using Demo.BL.Interface;
using Demo.DAL.Database;
using Demo.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BL.Repository
{
    public class CityRep : ICityRep
    {
        private readonly ApplicationContext db;

        public CityRep(ApplicationContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<City>> GetAsync(Expression<Func<City, bool>> filter = null)
        {
            if (filter == null)
                return await db.City.ToListAsync();
            else
                return await db.City.Where(filter).ToListAsync();
        }
    }
}
