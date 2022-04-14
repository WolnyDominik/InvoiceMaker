using InvoiceApp.Common.Models;

namespace InvoiceApp.Domain.Entities
{
    public class UserEntity : EntityBase
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
    }
}