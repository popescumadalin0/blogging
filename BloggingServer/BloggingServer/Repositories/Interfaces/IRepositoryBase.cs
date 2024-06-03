using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BloggingServer.Repositories.Interfaces;
public interface IRepositoryBase<T> where T : class
{
    /// <summary/>
    void Add(T objModel);

    /// <summary/>
    void AddRange(IEnumerable<T> objModel);

    /// <summary/>
    T Get(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);

    /// <summary/>
    Task<T> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);

    /// <summary/>
    IEnumerable<T> GetList(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);

    /// <summary/>
    Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);

    /// <summary/>
    IEnumerable<T> GetAll(Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);

    /// <summary/>
    Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);

    /// <summary/>
    int Count();

    /// <summary/>
    Task<int> CountAsync();

    /// <summary/>
    void Update(T objModel);

    /// <summary/>
    void Remove(T objModel);

    /// <summary/>
    void Dispose();
}