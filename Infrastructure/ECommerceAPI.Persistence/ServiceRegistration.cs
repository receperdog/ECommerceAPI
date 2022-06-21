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
                options.UseNpgsql(Configuration.ConnectionString));


            //Repositorylerimizin çalışması için gerekli olan nesneleri ekliyoruz.
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
            services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            //services.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();
            //services.AddScoped<ICategoryReadRepository, CategoryReadRepository>();



        }
    }
}
