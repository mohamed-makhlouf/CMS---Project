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
    public class CountryRep : ICountryRep
    {

        private readonly ApplicationContext db;

        public CountryRep(ApplicationContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<Country>> GetAsync(Expression<Func<Country, bool>> filter = null)
        {
            if (filter == null)
                return await db.Country.ToListAsync();
            else
                return await db.Country.Where(filter).ToListAsync();
        }
    }
}
