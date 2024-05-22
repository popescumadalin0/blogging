using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using DataBaseLayout.DbContext;
using Microsoft.EntityFrameworkCore;

namespace BloggingServer.Repositories.Interfaces;
public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
{
    protected readonly Context Context;

    public RepositoryBase(IContext context)
    {
        Context = context as Context;
    }

    public void Add(TEntity model)
    {
        Context.Set<TEntity>().Add(model);
        Context.SaveChanges();
    }

    public void AddRange(IEnumerable<TEntity> model)
    {
        Context.Set<TEntity>().AddRange(model);
        Context.SaveChanges();
    }

    public TEntity GetId(int id)
    {
        return Context.Set<TEntity>().Find(id);
    }

    public async Task<TEntity> GetIdAsync(int id)
    {
        return await Context.Set<TEntity>().FindAsync(id);
    }

    public TEntity Get(Expression<Func<TEntity, bool>> predicate)
    {
        return Context.Set<TEntity>().FirstOrDefault(predicate);
    }

    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await Context.Set<TEntity>().FirstOrDefaultAsync(predicate);
    }

    public IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate)
    {
        return Context.Set<TEntity>().Where(predicate).ToList();
    }

    public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await Task.Run(() => Context.Set<TEntity>().Where(predicate));
    }

    public IEnumerable<TEntity> GetAll()
    {
        return Context.Set<TEntity>().ToList();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await Task.Run(() => Context.Set<TEntity>());
    }

    public int Count()
    {
        return Context.Set<TEntity>().Count();
    }

    public async Task<int> CountAsync()
    {
        return await Context.Set<TEntity>().CountAsync();
    }

    public void Update(TEntity objModel)
    {
        Context.Entry(objModel).State = EntityState.Modified;
        Context.SaveChanges();
    }

    public void Remove(TEntity objModel)
    {
        Context.Set<TEntity>().Remove(objModel);
        Context.SaveChanges();
    }

    public void Dispose()
    {
        Context.Dispose();
    }
}