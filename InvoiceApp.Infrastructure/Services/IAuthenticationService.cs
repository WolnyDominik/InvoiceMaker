using InvoiceApp.Domain.Dtos.User;

namespace InvoiceApp.Infrastructure.Services;

public interface IAuthenticationService
{
    Task RegisterUser(NewUserDto newUser);
}
