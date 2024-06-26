using System.Net;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); // BadRequest -> 400

            try
            {
                IEnumerable<UserEntity> users = await _userService.GetAll();
                return Ok(await _userService.GetAll());
            }
            catch (ArgumentException e) // ArgumentException -> Erro de controller
            {
                // 500 -> Internal Server Error
                return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
            }
        }

        [HttpGet]
        [Route("{id}", Name = "GetById")]
        public async Task<ActionResult> GetById(Guid id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                return Ok(await _userService.GetById(id));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserEntity user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                UserEntity result = await _userService.Post(user);
                if (result != null)
                {
                    // 201 -> Created, indica que requisição foi bem sucedida e que um novo recurso foi criado.
                    return Created(new Uri(Url.Link("GetById", new { id = result.Id })), result); // Retorna o link e o objeto de result
                }
                return BadRequest();
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UserEntity user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                UserEntity result = await _userService.Put(user);

                if (result == null) return BadRequest();

                return Ok(result);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                return Ok(await _userService.Delete(id));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
            }
        }
    }
}
