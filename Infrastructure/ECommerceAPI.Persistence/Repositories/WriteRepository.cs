using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities.Common;
using ECommerceAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        private readonly ECommerceAPIDbContext _context;
        //constructor
        public WriteRepository(ECommerceAPIDbContext context)
        {
            _context = context;
        }


        public DbSet<T> Table => _context.Set<T>();
        
        public async Task<bool> addAsync(T entity)
        {
            //entityEntry has some enum values we will use this values for returning value
            EntityEntry<T> entityEntry = await Table.AddAsync(entity);
            return entityEntry.State == EntityState.Added;
        }

        public async Task<bool> addAsync(List<T> entity)
        {
            await Table.AddRangeAsync(entity);
            if (entity == null)
            {
                return false;
            }
            return true;
        }

        public bool delete(T entity)
        {
            EntityEntry<T> entityEntry = Table.Remove(entity);
            return entityEntry.State == EntityState.Deleted;
        }

        public bool delete(List<T> entity)
        {
            Table.RemoveRange(entity);
            if (entity == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> deleteByIdAsync(string id)
        {
            T data = await Table.FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));
            return delete(data);

        }

        public bool update(T entity)
        {
            EntityEntry entityEntry = Table.Update(entity);
            return entityEntry.State == EntityState.Modified;
        }

        public async Task<bool> saveAsync() 
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
