using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Persistence.Contexts;
using ECommerceAPI.Persistence.Repositories;
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
                options.UseNpgsql(Configuration.ConnectionString), ServiceLifetime.Singleton);


            //Repositorylerimizin çalışması için gerekli olan nesneleri ekliyoruz.
            services.AddSingleton<IProductWriteRepository, ProductWriteRepository>();
            services.AddSingleton<IProductReadRepository, ProductReadRepository>();
            services.AddSingleton<IOrderWriteRepository, OrderWriteRepository>();
            services.AddSingleton<IOrderReadRepository, OrderReadRepository>();
            services.AddSingleton<ICustomerWriteRepository, CustomerWriteRepository>();
            services.AddSingleton<ICustomerReadRepository, CustomerReadRepository>();
            //services.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();
            //services.AddScoped<ICategoryReadRepository, CategoryReadRepository>();



        }
    }
}
