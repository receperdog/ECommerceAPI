using ECommerceAPI.Persistence.Contexts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence
{
    public static class ServiceRegistration
    {
        //AddPersistenceServices
        public static void AddPersistenceServices(this IServiceCollection services) {
            //Burada oluşturduğumuz DbContext nesnesine options vererek, use komutu ile istediğimiz veritabanını seçebiliriz.
            //Ben burada postgreSql kullanacağım, bu yüzden nuget paketi içinde postgreSql ekledik.
            services.AddDbContext<ECommerceAPIDbContext>(options =>
                options.UseNpgsql("User ID=postgres;Password=123456;Host=localhost;Port=5432;Database=SeniorDesign_1;"));
        }
    }
}
