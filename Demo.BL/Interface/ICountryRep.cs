﻿using Demo.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BL.Interface
{
    public interface ICountryRep
    {
        Task<IEnumerable<Country>> GetAsync(Expression<Func<Country, bool>> filter = null);
    }
}
