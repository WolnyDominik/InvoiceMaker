using InvoiceApp.Common.Models;
using System.Data.Common;

namespace InvoiceApp.Common.Repositories;

public interface IRepository<TEntity> where TEntity : EntityBase
{
    Task<long> Insert(TEntity entity, DbConnection? connection = null, DbTransaction? transaction = null);
    IEnumerable<long> BatchInsert(IEnumerable<TEntity> entities, DbConnection? connection = null, DbTransaction? transaction = null);
    Task Update(TEntity entity, long id, DbConnection? connection = null, DbTransaction? transaction = null);
    Task Delete(long Id, DbConnection? connection = null, DbTransaction? transaction = null);
    Task<bool> Exists(string queryString = "", DbConnection? connection = null, DbTransaction? transaction = null);
    Task<bool> Exists(string queryString = "", object param = null, DbConnection? connection = null, DbTransaction? transaction = null);
    Task<long> Count(string queryString = "", DbConnection? connection = null, DbTransaction? transaction = null);
    Task<long> Count(string queryString = "", object param = null, DbConnection? connection = null, DbTransaction? transaction = null);
    Task<TEntity> GetSingle(string queryString = "", object param = null, DbConnection? connection = null, DbTransaction? transaction = null);
    Task<IEnumerable<TEntity>> GetMany(string queryString = "", object param = null, DbConnection? connection = null, DbTransaction? transaction = null);
}
