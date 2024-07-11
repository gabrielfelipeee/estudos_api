using Api.Domain.Dtos;
using Api.Domain.Dtos.User;

namespace Api.Domain.Interfaces.Services.User
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAll();
        Task<UserDto> GetById (Guid id);
        Task<UserDtoCreateResult> Post(UserDto user);
        Task<UserDtoUpdateResult> Put (UserDto user);
        Task<bool> Delete (Guid id);
    }
}
