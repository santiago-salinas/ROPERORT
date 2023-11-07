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
    private const int _zero = 0;
    private const string _promoDirectoryPath = "C:\\Users\\Federico Rodriguez\\source\\repos\\IngSoft-DA2-2023-2\\266628-255981-271568\\Codigo\\POS\\Services\\AvailablePromos";
    private List<IPromo> _promos;
    List<IPromo> Promos { get => _promos; }
    public PromoService()
    {
        _promos = new List<IPromo>();
    }

    public List<IPromo> GetAll()
    {
        List<IPromo> availablePromos = new List<IPromo>();

        foreach (string file in Directory.GetFiles(".\\promotions", "*_Promo.dll"))
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