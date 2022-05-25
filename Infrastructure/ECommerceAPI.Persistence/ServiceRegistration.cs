using ECommerceAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceAPI.Persistence
{
    public static class ServiceRegistration
    {


        //AddPersistenceServices
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            //Burada oluşturduğumuz DbContext nesnesine options vererek, use komutu ile istediğimiz veritabanını seçebiliriz.
            //Ben burada postgreSql kullanacağım, bu yüzden nuget paketi içinde postgreSql ekledik.
            services.AddDbContext<ECommerceAPIDbContext>(options =>
                options.UseNpgsql(Configuration.ConnectionString));
        }
    }
}
