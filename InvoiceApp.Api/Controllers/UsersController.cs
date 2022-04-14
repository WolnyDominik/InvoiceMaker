using Microsoft.AspNetCore.Mvc;
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

    public UsersController(ILogger<UsersController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult GetMany()
        => users.Any()
            ? Ok(users)
            : NoContent();
    
    [HttpGet("{id}")]
    public IActionResult GetSingle(long id)
        => users.Where(u => u.Id == id).FirstOrDefault() is UserEntity user
            ? Ok(user)
            : NotFound(id);
}
