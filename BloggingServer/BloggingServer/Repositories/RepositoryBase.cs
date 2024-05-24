using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using DataBaseLayout.DbContext;
using Microsoft.EntityFrameworkCore;
using BloggingServer.Repositories.Interfaces;

namespace BloggingServer.Repositories;
public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
{
    protected readonly Context Context;

    public RepositoryBase(IContext context)
    {
        Context = context as Context;
    }

    /// <inheritdoc />
    public void Add(TEntity model)
    {
        Context.Set<TEntity>().Add(model);
        Context.SaveChanges();
    }

    /// <inheritdoc />
    public void AddRange(IEnumerable<TEntity> model)
    {
        Context.Set<TEntity>().AddRange(model);
        Context.SaveChanges();
    }

    /// <inheritdoc />
    public TEntity Get(Expression<Func<TEntity, bool>> predicate)
    {
        return Context.Set<TEntity>().FirstOrDefault(predicate);
    }

    /// <inheritdoc />
    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await Context.Set<TEntity>().FirstOrDefaultAsync(predicate);
    }
    
    /// <inheritdoc />
    public IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate)
    {
        return Context.Set<TEntity>().Where(predicate).ToList();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await Task.Run(() => Context.Set<TEntity>().Where(predicate));
    }

    /// <inheritdoc />
    public IEnumerable<TEntity> GetAll()
    {
        return Context.Set<TEntity>().ToList();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await Task.Run(() => Context.Set<TEntity>());
    }

    /// <inheritdoc />
    public int Count()
    {
        return Context.Set<TEntity>().Count();
    }

    /// <inheritdoc />
    public async Task<int> CountAsync()
    {
        return await Context.Set<TEntity>().CountAsync();
    }

    /// <inheritdoc />
    public void Update(TEntity objModel)
    {
        Context.Entry(objModel).State = EntityState.Modified;
        Context.SaveChanges();
    }

    /// <inheritdoc />
    public void Remove(TEntity objModel)
    {
        Context.Set<TEntity>().Remove(objModel);
        Context.SaveChanges();
    }

    /// <inheritdoc />
    public void Dispose()
    {
        Context.Dispose();
    }
}