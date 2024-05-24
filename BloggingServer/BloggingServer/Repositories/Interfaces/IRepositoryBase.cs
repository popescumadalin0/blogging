using System;
using System.Collections.Generic;
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
    T Get(Expression<Func<T, bool>> predicate);

    /// <summary/>
    Task<T> GetAsync(Expression<Func<T, bool>> predicate);

    /// <summary/>
    IEnumerable<T> GetList(Expression<Func<T, bool>> predicate);

    /// <summary/>
    Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> predicate);

    /// <summary/>
    IEnumerable<T> GetAll();

    /// <summary/>
    Task<IEnumerable<T>> GetAllAsync();

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