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
    public class EmployeeRep : IEmployeeRep
    {
        private readonly ApplicationContext db;

        public EmployeeRep(ApplicationContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<Employee>> GetAsync(Expression<Func<Employee, bool>> filter = null)
        {
            if(filter == null)
                return await db.Employee.Include("Department").Include("District").ToListAsync();
            else
                return await db.Employee.Include("Department").Include("District").Where(filter).ToListAsync();
        }

        public async Task<Employee> GetByIdAsync(Expression<Func<Employee, bool>> filter)
        {
            var data = await db.Employee.Where(filter).Include("Department").Include("District").FirstOrDefaultAsync();
            return data;
        }

        public async Task<Employee> CreateAsync(Employee obj)
        {
            await db.Employee.AddAsync(obj);
            await db.SaveChangesAsync();

            return db.Employee.OrderByDescending(a => a.Id).FirstOrDefault();
        }


        public async Task UpdateAsync(Employee obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var oldData = await db.Employee.FindAsync(id);

            db.Employee.Remove(oldData);
            await db.SaveChangesAsync();
        }
    }
}
