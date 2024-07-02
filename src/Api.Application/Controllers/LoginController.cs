using System.Net;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public async Task<object> Login([FromBody] UserEntity userEntity, [FromServices] ILoginService service)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (userEntity == null) return BadRequest();
            try
            {
                var result = await service.FindUserByLogin(userEntity);
                if (result != null) 
                {
                    return Ok(result);
                }
                return NotFound();
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
            }
        }
    }
}
