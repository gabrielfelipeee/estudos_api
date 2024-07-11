using System.Net;
using Api.Domain.Dtos;
using Api.Domain.Dtos.User;
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
                return Ok(users);
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
                UserDto user = await _userService.GetById(id);
                return Ok(user);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserDtoCreate user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                UserDtoCreateResult result = await _userService.Post(user);
                if (result != null)
                {
                    
                    // Cria uma URI baseada na rota GET "GetById" + o parâmetro id
                    // Uri nesse vaso seria -> 'api/Users/id'
                    var location = new Uri(Url.Link("GetById", new { id = result.Id }));

                    // 201 -> Created, indica que requisição foi bem sucedida e que um novo recurso foi criado.
                    return Created(location, result); // Retorna a Uri e o user
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
        public async Task<ActionResult> Put([FromBody] UserDtoUpdate user)
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
                var result = await _userService.Delete(id);
                return Ok(result);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
            }
        }
    }
}
