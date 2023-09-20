using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Services.Interfaces;
using DataAccess;
/*
class BaseRepository<T> : IRepository<T>
{
    internal EFContext context;
    internal DbSet<T> dbSet;

    public BaseRepository(EFContext context)
    {
        this.context = context;
        this.dbSet = context.Set<T>();
    }

    public virtual T? Get(int id)
    {
        dbSet.FirstOrDefault().Where(t => t.Id == id);
    }

    public virtual T GetByID(object id)
    {
        return dbSet.Find(id);
    }

    public virtual void Insert(T entity)
    {
        dbSet.Add(entity);
    }

    public virtual void Delete(object id)
    {
        T entityToDelete = dbSet.Find(id);
        Delete(entityToDelete);
    }

    public virtual void Delete(T entityToDelete)
    {
        if (context.Entry(entityToDelete).State == EntityState.Detached)
        {
            dbSet.Attach(entityToDelete);
        }
        dbSet.Remove(entityToDelete);
    }

    public virtual void Update(T entityToUpdate)
    {
        dbSet.Attach(entityToUpdate);
        context.Entry(entityToUpdate).State = EntityState.Modified;
    }
}
*/