using AutoMapper;
using InvoiceApp.Common.Repositories;
using InvoiceApp.Domain.Dtos.User;
using InvoiceApp.Domain.Entities;

namespace InvoiceApp.Infrastructure.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IHashService _hashService;
    private readonly IMapper _mapper;
    private readonly IRepository<UserEntity> _userRepository;

    public AuthenticationService(
        IHashService hashService,
        IMapper mapper,
        IRepository<UserEntity> userRepository)
    {
        _hashService = hashService;
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task RegisterUser(NewUserDto newUser)
    {
        if (await _userRepository.Exists("WHERE Email=@Email", new { newUser.Email }))
            throw new Exception("User already exists");
        newUser.Password = _hashService.HashPassword(newUser.Password);
        await _userRepository.Insert(_mapper.Map<UserEntity>(newUser));
    }
}
