using InvoiceApp.Common.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InvoiceApp.Common.Query;

public class QueryBuilderOLD_DONOTUSE
{
    private List<string> columnNames = new();
    private string tableName;
    private Dictionary<string, string> tables = new();

    public string Query { get; set; }

    public static QueryBuilderOLD_DONOTUSE Create<TEntity>() where TEntity : EntityBase
    {
        var current = new QueryBuilderOLD_DONOTUSE();
        foreach (var property in typeof(TEntity).GetProperties())
            current.columnNames.Add(property.GetCustomAttributes(typeof(ColumnAttribute), false).Cast<ColumnAttribute>().FirstOrDefault()?.Name ?? property.Name);
        var tableName = typeof(TEntity).GetCustomAttributes(typeof(TableAttribute), false).Cast<TableAttribute>().FirstOrDefault()?.Name ?? typeof(TEntity).Name;
        current.tables[tableName] = "";

        return current;
    }

    public QueryBuilderOLD_DONOTUSE Select()
    {
        Query = $"SELECT {string.Join(", ", columnNames)} FROM {GetTable()}";
        return this;
    }

    public QueryBuilderOLD_DONOTUSE AddWhere()
    {
        Query = $"{Query}";
        return this;
    }

    private string GetTable()
    {
        StringBuilder sb = new("");
        foreach (var table in tables)
            sb.Append($"{table.Key} {table.Value}");
        return sb.ToString().TrimEnd();
    }
}
