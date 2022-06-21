using ECommerceAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Repositories
{
    public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
    {
        Task<bool> addAsync(T entity);
        //Task<bool> addAsync(List<T> entity);
        bool update(T entity);
        bool delete(T entity);
        bool delete(List<T> entity);
        Task<bool> deleteByIdAsync(string id);
        Task<bool> saveAsync();

    }
}
