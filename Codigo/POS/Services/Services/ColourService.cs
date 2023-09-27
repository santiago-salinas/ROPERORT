﻿using Services.Interfaces;
using Services.Models;

namespace Services;

public class ColourService : IGetService<Colour>
{
    private readonly IGetRepository<Colour> _repository;
    public ColourService(IGetRepository<Colour> repository)
    {
        _repository = repository;
    }

    public List<Colour> GetAll() => _repository.GetAll();

    public Colour? Get(string name) => _repository.Get(name);
}