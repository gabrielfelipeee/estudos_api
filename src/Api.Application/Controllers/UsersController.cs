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
    }
}
