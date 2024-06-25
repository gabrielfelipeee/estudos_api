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
        [HttpGet]
        public async Task<ActionResult> GetAll([FromServices] IUserService service)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); // BadRequest -> 400

            try
            {
                IEnumerable<UserEntity> users = await service.GetAll();
                return Ok(await service.GetAll());
            }
            catch (ArgumentException e) // ArgumentException -> Erro de controller
            {
                // 500 -> Internal Server Error
                return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
            }
        }
    }
}
