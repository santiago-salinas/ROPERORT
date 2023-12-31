﻿using Services.Exceptions;
using Services.Interfaces;
using Services.Models;

namespace Services;
public class CategoryService : IGetService<Category>
{
    private readonly IGetRepository<Category> _repository;
    public CategoryService(IGetRepository<Category> repository)
    {
        _repository = repository;
    }

    public List<Category> GetAll()
    {
        try { return _repository.GetAll(); }
        catch (DatabaseException ex)
        {
            throw new Service_ObjectHandlingException("Exception catched from the repository: " + ex.Message);
        };
    }
}