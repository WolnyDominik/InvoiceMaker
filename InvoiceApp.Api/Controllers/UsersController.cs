using Microsoft.AspNetCore.Mvc;
using InvoiceApp.Common.Repositories;
using InvoiceApp.Domain.Entities;

namespace InvoiceApp.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private static readonly UserEntity[] users = new[]
    {
        new UserEntity
        {
            Id = 0,
            Email = "wolny.dominik@outlook.com",
            Name = "Dominik",
            Surname = "Wolny"
        },
        new UserEntity
        {
            Id = 1,
            Email = "milkiewicz.daniel@outlook.com",
            Name = "Daniel",
            Surname = "Milkiewicz"
        }
    };

    private readonly ILogger<UsersController> _logger;
    private readonly IRepository<UserEntity> _userRepository;

    public UsersController(
        ILogger<UsersController> logger,
        IRepository<UserEntity> userRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetMany()
        => Ok(await _userRepository.GetMany());
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSingle(long id)
        => await _userRepository.GetSingle("WHERE Id=@Id", new {Id = id}) is UserEntity user
            ? Ok(user)
            : NotFound(id);
}
