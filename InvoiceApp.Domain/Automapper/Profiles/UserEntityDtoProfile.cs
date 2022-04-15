using AutoMapper;
using InvoiceApp.Domain.Dtos.User;
using InvoiceApp.Domain.Entities;

namespace InvoiceApp.Domain.Automapper.Profiles;

public class UserEntityDtoProfile : Profile
{
    public UserEntityDtoProfile()
    {
        CreateMap<NewUserDto, UserEntity>();
    }
}