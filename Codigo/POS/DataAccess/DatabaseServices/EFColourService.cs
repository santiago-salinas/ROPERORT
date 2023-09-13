using DataAccess.Entities;
using DataAccess.Expcetions;
using Microsoft.EntityFrameworkCore;
using Rest_Api.Models;
using Rest_Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DatabaseServices
{
    public class EFColourService : IGetService<Colour>
    {
        public EFColourService() { }

        public List<Colour> GetAll()
        {
            using (EFContext context = new EFContext())
            {
                try
                {
                    List<ColourEntity> entities = context.ColourEntities.ToList();
                    List<Colour> colours = entities.Select(c => ColourEntity.FromEntity(c)).ToList();

                    return colours;

                }
                catch
                {
                    throw new DatabaseException("Error while getting all colours from database");
                }
            }

        }

        public Colour? Get(string name)
        {
            try
            {
                using(EFContext context = new EFContext())
                {
                    ColourEntity entity = context.ColourEntities.First(p => p.Name == name);
                    
                    return ColourEntity.FromEntity(entity);
                }

            }catch 
            {
                throw new DatabaseException("Error while trying to get colour " + name);
            }
        }
    }
}
