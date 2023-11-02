using Services.Interfaces;
using Services.Models;
using Services.Exceptions;
using System.Reflection;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;


namespace Services;

public class PromoService : IPromoService
{
    private const int _zero = 0;
    private const string _promoDirectoryPath = "C:\\Users\\Federico Rodriguez\\source\\repos\\IngSoft-DA2-2023-2\\266628-255981-271568\\Codigo\\POS\\Services\\AvailablePromos";

    List<IPromo> Promos { get; }
    public PromoService()
    {
        Promos = new List<IPromo>
        {
            /*new FidelityPromo(),
            new ThreeForTwoPromo(),
            new TwentyPercentOff(),
            new TotalLookPromo(),*/
        };
    }

    public List<IPromo> GetAll() => Promos;
    public List<IPromo> GetAvailablePromosFromDirectory()
    {
        List<IPromo> availablePromos = new List<IPromo>();

        foreach (string file in Directory.GetFiles(_promoDirectoryPath,"*_Promo.dll"))
        {
            try
            {
                Assembly assembly = Assembly.LoadFrom(file);

                var TYPES= assembly.GetTypes();

                var type = TYPES.First(t => typeof(IPromo).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);
                
                IPromo promo = (IPromo)Activator.CreateInstance(type);
                availablePromos.Add(promo);                
            }
            catch (Exception)
            {
                throw new Service_PromosHandlingException("An exception ocurred while trying to fetch available promos");
            }
        }

        return availablePromos;
    }


    public IPromo? Get(string name) => Promos.FirstOrDefault(p => p.Name == name);
}