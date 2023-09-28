﻿using Services.Interfaces;
using Services.Models;
using Services.Models.Promos;


namespace Services;

public class PromoService : IPromoService
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