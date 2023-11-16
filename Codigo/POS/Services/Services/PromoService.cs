using Services.Models;
using Services.Exceptions;
using System.Reflection;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Services.Interfaces;


namespace Services;

public class PromoService : IPromoService
{
    private List<IPromo> _promos;
    List<IPromo> Promos { get => _promos; }
    public PromoService()
    {
        _promos = new List<IPromo>();
    }

    public List<IPromo> GetAll()
    {
        List<IPromo> availablePromos = new List<IPromo>();

        foreach (string file in Directory.GetFiles(".\\AvailablePromotions", "*_Promo.dll"))
        {
            try
            {
                Assembly assembly = Assembly.LoadFrom(file);

                foreach (var type in assembly.GetTypes())
                {
                    if (type != null && typeof(IPromo).IsAssignableFrom(type))
                    {
                        IPromo promo = (IPromo)Activator.CreateInstance(type);
                        availablePromos.Add(promo);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Service_PromosHandlingException("An exception occurred while trying to fetch available promos: " + ex.Message);
            }
        }

        _promos = availablePromos;

        return availablePromos;
    }



    public IPromo? Get(string name) => Promos.FirstOrDefault(p => p.Name == name);
}