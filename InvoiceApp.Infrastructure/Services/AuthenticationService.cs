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

    public async Task<bool> Login(LoginUserDto loginUser)
    {
        var dbUser = await _userRepository.GetSingle("WHERE Email=@Email", new { loginUser.Email });
        if (dbUser is null)
            return false;
        (var validated, var needsRehash) = _hashService.VerifyHashedPassword(dbUser.Password, loginUser.Password);
        if (needsRehash)
            dbUser.Password = _hashService.HashPassword(loginUser.Password);
        return validated;
    }
}
