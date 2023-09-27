using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IPromoService
    {
        public List<Promo> GetAll();

        public Promo? Get(string name);
    }
}
