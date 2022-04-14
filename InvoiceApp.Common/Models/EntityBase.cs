using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceApp.Common.Models;

public class EntityBase
{
    public long Id { get; set; }
    public long CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public long UpdatedBy { get; set; }
    public DateTime UpdatedOn { get; set; }
}
