using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using InvoiceApp.Common.Enums;

namespace InvoiceApp.Common.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class JoinAttribute : Attribute
{
    private readonly Type[] _seekedAttributes = new Type[] { typeof(TableWithAliasAttribute), typeof(TableAttribute) };
    public JoinType JoinType { get; }
    public Type ReferencedTable { get; }
    public string SourceAlias { get; }
    public string ReferenceAlias { get; }

    public JoinAttribute(JoinType joinType, Type referencedTable, string? sourceAlias = null, string? referenceAlias = null)
    {
        JoinType = joinType;
        ReferencedTable = referencedTable;
        SourceAlias = sourceAlias;
        ReferenceAlias = referenceAlias ?? RetrieveAliasFromTableType();
    }

    private string RetrieveAliasFromTableType()
    {
        if (ReferencedTable.GetCustomAttributes<TableWithAliasAttribute>().FirstOrDefault() is TableWithAliasAttribute tableWithAlias)
            return tableWithAlias.Alias;
        if (ReferencedTable.GetCustomAttributes<TableWithAliasAttribute>().FirstOrDefault() is TableAttribute table)
            return string.Concat(table.Name.Where(c => char.IsUpper(c)));
        return string.Concat(ReferencedTable.Name.Where(c => char.IsUpper(c)));
    }
}