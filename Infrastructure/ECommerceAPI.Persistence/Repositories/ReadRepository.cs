using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities.Common;
using ECommerceAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        //Dependency Injection from IoC container
        //Read Repository is a generic repository that can be used for any entity
        //Bu sınıfı IoC container ile inject edebiliriz.
        //Bu sınıfı ıoc container'a kayıt etmemiz gerek ki istediğimizde context'i versin
        private readonly ECommerceAPIDbContext _context;

        public ReadRepository(ECommerceAPIDbContext context)
        {
            _context = context;
        }

        //EFcore burada contextten dönecek nesnesini tipini belirlememiz için bize Set metodunu veriyor.
        public DbSet<T> Table => _context.Set<T>();

        //Burada Table nesnesi bize veri tabanındaki verileri getirir.
        public IQueryable<T> GetAll(bool tracking = true) {

            return tracking ? Table.AsQueryable() : Table.AsQueryable().AsNoTracking();

        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> predicate, bool tracking = true)
        {

            return tracking ? Table.Where(predicate).AsQueryable() : Table.Where(predicate).AsQueryable().AsNoTracking();

        }

        public async Task<T> GetValueAsync(Expression<Func<T, bool>> predicate, bool tracking = true) {

            var query = tracking ? Table.Where(predicate).AsQueryable() : Table.Where(predicate).AsQueryable().AsNoTracking();
            return await query.FirstOrDefaultAsync(predicate);

        }
        //Base entity marker pattern'i oluyor.
        public async Task<T> GetByIdAsync(string id, bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (tracking)
            {
                query = query.AsTracking();
            }
            else
            {
                query = query.AsNoTracking();
            }
            return await query.FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));

        }


    }
}
