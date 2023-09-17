﻿using Models;
using System.Xml.Linq;

namespace Rest_Api.Interfaces
{
    public interface ICRUDService<T>
    {

        abstract public List<T> GetAll();

        abstract public T? Get(int id);

        abstract public void Add(T entity);

        abstract public void Delete(int id);

        abstract public void Update(T entity);
    }
}
