using Api.Domain.Dtos;
using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Services.User;
using Api.Domain.Models;
using AutoMapper;


// Regras de negócios de user
namespace Api.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<UserEntity> _repository;
        private readonly IMapper _mapper;
        public UserService(IRepository<UserEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            var entities = await _repository.SelectAllAsync(); // Lista de UserEntity
            var usersDto = _mapper.Map<IEnumerable<UserDto>>(entities); // Lista de UserDto
            /*
            List<UserDto> dtos = new List<UserDto>();
            foreach (var entity in entities)
            {
                var dto = new UserDto
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Email = entity.Email,
                    CreateAt = entity.CreateAt ?? DateTime.MinValue
                };
                dtos.Add(dto);
            }
            */
            return usersDto;
        }
        public async Task<UserDto> GetById(Guid id)
        {
            var entity = await _repository.SelectByIdAsync(id); // UserEntity
            var userDto = _mapper.Map<UserDto>(entity); // Mapeia UserEntity para UserDto
            /*
            var result = new UserDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Email = entity.Email,
                CreateAt = entity.CreateAt ?? DateTime.MinValue
            };
            */
            return userDto;
        }
        public async Task<UserDtoCreateResult> Post(UserDtoCreate user)
        {
            var model = _mapper.Map<UserModel>(user);
            var entity = _mapper.Map<UserEntity>(model);
            var result = await _repository.InsertAsync(entity); // Adiciona e retorna uma UserEntity

            var dto = _mapper.Map<UserDtoCreateResult>(result);
            return dto;
        }
        public async Task<UserDtoUpdateResult> Put(UserDtoUpdate user)
        {
            var model = _mapper.Map<UserModel>(user);
            var entity = _mapper.Map<UserEntity>(model);
            var result = await _repository.UpdateAsync(entity); // Atualiza e retorna uma UserEntity

            var dto = _mapper.Map<UserDtoUpdateResult>(result);
            return dto;
        }
        public async Task<bool> Delete(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
