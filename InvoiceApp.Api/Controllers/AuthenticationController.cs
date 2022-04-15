using Microsoft.AspNetCore.Mvc;
using InvoiceApp.Domain.Dtos.User;
using InvoiceApp.Infrastructure.Services;

namespace InvoiceApp.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authService;

        public AuthenticationController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpPost(nameof(Register))]
        public async Task Register(NewUserDto newUser)
            => await _authService.RegisterUser(newUser);
    }
}