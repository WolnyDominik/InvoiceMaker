using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceApp.Common.Attributes;

public class TableWithAliasAttribute : TableAttribute
{
    public string Alias { get; set; }

    public TableWithAliasAttribute(string name, string alias) : base(name)
    {
        Alias = alias;
    }
}
