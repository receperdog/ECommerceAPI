using ECommerceAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Repositories
{
    public interface IReadRepository<T> : IRepository<T> where T:BaseEntity
    {
        //get data from database
        //Sorgular ile çalışacağım için IQueryable kullanıyorum. Eğer Inmemory çalışsaydım IEnumarable kullanacaktım.
        IQueryable<T> GetAll();

        //Şartlı veri sorgulamak için kullanılır
        IQueryable<T> GetWhere(System.Linq.Expressions.Expression<Func<T, bool>> predicate);

        //Belirlenen şarta uyan Tek bir değeri vermesi için kullanılır
        Task<T> GetValueAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate);

        Task<T> GetByIdAsync(string id);
        
        
        
    }
}
