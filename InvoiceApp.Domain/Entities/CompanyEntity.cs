using InvoiceApp.Common.Models;

namespace InvoiceApp.Domain.Entities
{
    public class CompanyEntity : EntityBase
    {
        public string Name { get; set; } = null!;
        public string TaxId { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string CountryShort { get; set; } = null!;
    }
}