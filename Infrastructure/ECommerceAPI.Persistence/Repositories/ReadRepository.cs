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
        public IQueryable<T> GetAll() => Table;

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> predicate) => Table.Where(predicate);
        public async Task<T> GetValueAsync(Expression<Func<T, bool>> predicate) => await Table.FirstOrDefaultAsync(predicate);

        //Base entity marker pattern'i oluyor.
        public async Task<T> GetByIdAsync(string id) => await Table.FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));
    }
}
