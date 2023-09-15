﻿using Rest_Api.Models;
using Rest_Api.Models.Promos;
using System.Drawing;

namespace Rest_Api.Services;

public class PromoService : IGetService<Promo>
{
    private const int _zero = 0;

    List<Promo> Promos { get; }
    public PromoService() 
    {
        Promos = new List<Promo>
        {
            new FidelityPromo(),
            new ThreeForTwoPromo(),
            new TwentyPercentOff(),
            new TotalLookPromo()
        };
    }


    public List<Promo> GetAll() => Promos;
    public Promo? Get(string name) => Promos.FirstOrDefault(p => p.Name == name);

}