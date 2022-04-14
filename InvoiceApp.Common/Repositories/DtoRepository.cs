using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Reflection;
using System.Text;
using Dapper;
using InvoiceApp.Common.Attributes;
using InvoiceApp.Common.Models;
using Npgsql;

namespace InvoiceApp.Common.Repositories;

public class DtoRepository<TEntity> where TEntity : DtoBase
{
    private string _selectQuery
    {
        get => $"SELECT {string.Join(", ", _propColNames.Values)} FROM {_tableName}";
    }

    private string _countQuery
    {
        get => $"SELECT COUNT({_propColNames[nameof(EntityBase.Id)]}) FROM {_tableName}";
    }

    private string _deleteQuery
    {
        get => $"DELETE FROM {_tableName}";
    }

    private string _updateQuery
    {
        get
        {
            var querySb = new StringBuilder($"UPDATE {_tableName} SET ");
            var queryProps = new List<string>();
            foreach (var prop in _propColNames)
                queryProps.Add($"{prop.Value}=(@{prop.Key}) ");
            return querySb.Append(string.Join(", ", queryProps)).ToString();
        }
    }

    private string _insertQuery
    {
        get => $"INSERT INTO {_tableName} ({string.Join(", ", _propColNames.Values)}) VALUES ({string.Join(", ", _propColNames.Keys.Select(k => $"@{k}"))}) RETURNING {_propColNames[nameof(EntityBase.Id)]}";
    }

    private readonly Dictionary<string, string> _propColNames = new();
    private readonly string _tableName;
    private readonly string _connString;

    public DtoRepository()
    {
        foreach (var prop in typeof(TEntity).GetProperties())
            _propColNames[prop.Name] = prop.GetCustomAttributes<ColumnAttribute>()?.FirstOrDefault()?.Name ?? prop.Name;
        _tableName = typeof(TEntity).GetCustomAttributes<TableAttribute>()?.FirstOrDefault()?.Name ?? typeof(TEntity).Name;
        var _attr = typeof(TEntity).GetCustomAttributes<JoinAttribute>();
    }

    //public async Task<long> Insert(TEntity entity, DbConnection? connection = null, DbTransaction? transaction = null)
    //{
    //    var query = _selectQuery;
    //    await using var conn = connection ?? await OpenConnectionAsync();
    //    return await conn.QuerySingleAsync<long>(query, entity, transaction);
    //}

    //public IEnumerable<long> BatchInsert(IEnumerable<TEntity> entities, DbConnection? connection = null, DbTransaction? transaction = null)
    //    => entities.Select(async e => await Insert(e, connection, transaction)).Select(t => t.Result);

    //public async Task Update(TEntity entity, long id, DbConnection? connection = null, DbTransaction? transaction = null)
    //{
    //    var query = $"{_updateQuery} WHERE {_propColNames[nameof(EntityBase.Id)]}=@{nameof(entity.Id)}";
    //    await using var conn = connection ?? await OpenConnectionAsync();
    //    await conn.ExecuteAsync(query, entity, transaction);
    //}

    //public async Task Delete(long id, DbConnection? connection = null, DbTransaction? transaction = null)
    //{
    //    var query = $"{_deleteQuery} WHERE {_propColNames[nameof(EntityBase.Id)]}=@Id";
    //    await using var conn = connection ?? await OpenConnectionAsync();
    //    await conn.ExecuteAsync(query, new { Id = id }, transaction);
    //}

    //public async Task<bool> Exists(string queryString = "", DbConnection? connection = null, DbTransaction? transaction = null)
    //    => await Count(queryString, connection, transaction) > 0;

    //public async Task<bool> Exists(string queryString = "", object param = null, DbConnection? connection = null, DbTransaction? transaction = null)
    //    => await Count(queryString, param, connection, transaction) > 0;

    //public async Task<long> Count(string queryString = "", DbConnection? connection = null, DbTransaction? transaction = null)
    //    => await Count(queryString, null, connection, transaction);

    //public async Task<long> Count(string queryString = "", object param = null, DbConnection? connection = null, DbTransaction? transaction = null)
    //{
    //    string query = $"{_countQuery} {queryString}";
    //    await using var conn = connection ?? await OpenConnectionAsync();
    //    return await conn.QueryFirstAsync<long>(query, param, transaction);
    //}

    public async Task<TEntity> GetSingle(string queryString = "", DbConnection? connection = null, DbTransaction? transaction = null)
        => await GetSingle(queryString, null, connection, transaction);

    public async Task<TEntity> GetSingle(string queryString = "", object param = null, DbConnection? connection = null, DbTransaction? transaction = null)
    {
        string query = $"{_selectQuery} {queryString}";
        await using var conn = connection ?? await OpenConnectionAsync();
        return await conn.QueryFirstAsync<TEntity>(query, param, transaction);
    }

    public async Task<IEnumerable<TEntity>> GetMany(string queryString = "", DbConnection? connection = null, DbTransaction? transaction = null)
        => await GetMany(queryString, null, connection, transaction);

    public async Task<IEnumerable<TEntity>> GetMany(string queryString = "", object param = null, DbConnection? connection = null, DbTransaction? transaction = null)
    {
        string query = $"{_selectQuery} {queryString}";
        await using var conn = connection ?? await OpenConnectionAsync();
        return await conn.QueryAsync<TEntity>(query, param, transaction);
    }

    private async Task<DbConnection> OpenConnectionAsync()
    {
        var connection = new NpgsqlConnection(_connString);
        await connection.OpenAsync();
        return connection;
    }
}