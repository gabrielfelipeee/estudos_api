using Api.Domain.Dtos;
using Api.Domain.Models;
using AutoMapper;

namespace Api.CrossCutting.Mappings
{
    public class DtoToModelProfile : Profile
    {
        public DtoToModelProfile()
        {
            CreateMap<UserModel, UserDto>()  // Mapeia UserModel para UserDto
                .ReverseMap(); // Mapeia também o inverso (UserDto para UserModel)
        }
    }
}
