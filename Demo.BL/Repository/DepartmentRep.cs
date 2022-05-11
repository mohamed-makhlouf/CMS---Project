using Demo.BL.Interface;
using Demo.BL.Models;
using Demo.DAL.Database;
using Demo.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BL.Repository
{
    public class DepartmentRep : IDepartment
    {
        private readonly ApplicationContext db;

        public DepartmentRep(ApplicationContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<Department>> GetAsync()
        {
            var data = await db.Department.ToListAsync();
            return data;
        }

        public async Task<Department> GetByIdAsync(int id)
        {
            var data = await db.Department.Where(a => a.Id == id).FirstOrDefaultAsync();
            return data;
        }


        public async Task CreateAsync(Department obj)
        {
            await db.Department.AddAsync(obj);
            await db.SaveChangesAsync();
        }


        public async Task UpdateAsync(Department obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var oldData = await db.Department.FindAsync(id);

            db.Department.Remove(oldData);
            await db.SaveChangesAsync();
        }
    }
}
