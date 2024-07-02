using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.User;
using Api.Domain.Repository;

namespace Api.Service.Services
{

    public class LoginService : ILoginService
    {
        private IUserRepository _userRepository;
        public LoginService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<object> FindUserByLogin(UserEntity user)
        {
            try
            {
                if (user != null && !string.IsNullOrWhiteSpace(user.Email))
                {
                    return await _userRepository.FindByLogin(user.Email);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
