using System.Net;
using Api.Domain.Dtos;
using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); // BadRequest -> 400

            try
            {
                IEnumerable<UserDto> users = await _userService.GetAll();
                return Ok(await _userService.GetAll());
            }
            catch (ArgumentException e) // ArgumentException -> Erro de controller
            {
                // 500 -> Internal Server Error
                return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
            }
        }

        [Authorize]
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

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserDto user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                UserDtoCreateResult result = await _userService.Post(user);
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

        [Authorize]
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UserDto user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                UserDtoUpdateResult result = await _userService.Put(user);

                if (result == null) return BadRequest();

                return Ok(result);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
            }
        }

        [Authorize]
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
