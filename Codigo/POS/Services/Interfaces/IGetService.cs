﻿namespace Services.Interfaces
{
    public interface IGetService<T>
    {
        abstract public List<T> GetAll();
    }
}
