using ECommerceAPI.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        //Repository dp to be implemented by all repositories
        //Some of the methods will be common to all repositories
        //Burada repository işlemlerinin hepsinin en geneli tanımlanır.

        //Generic bir yapıda olmalı ve her repo kullanılabilmeli
        DbSet<T> Table { get; }
        //Task<T> GetByIdAsync(int id);

    }
}
