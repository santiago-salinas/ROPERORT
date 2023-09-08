using DataAccess.Entities;
using DataAccess.Expcetions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DatabaseServices
{
    public class EFColourService
    {
        public EFColourService() { }

        public List<ColourEntity> GetAll()
        {
            using (EFContext context = new EFContext())
            {
                try
                {
                    List<ColourEntity> colours = context.ColourEntities.ToList();

                    return colours;

                }
                catch
                {
                    throw new DatabaseException("Error while getting all colours from database");
                }
            }

        }

        public ColourEntity? Get(string name)
        {
            try
            {
                using(EFContext context = new EFContext())
                {
                    ColourEntity colour = context.ColourEntities.First(p => p.Name == name);

                    return colour;
                }

            }catch 
            {
                throw new DatabaseException("Error while trying to get colour " + name);
            }
        }
    }
}
