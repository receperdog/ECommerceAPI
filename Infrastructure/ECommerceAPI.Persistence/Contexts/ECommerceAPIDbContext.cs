using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Persistence.Contexts
{
    //Dbcontext nesnesidir, EFCore kullanmamız için bu nesne oluşturulmalıdır.
    public class ECommerceAPIDbContext : DbContext
    {
        //DbContext kullanırken belirli optionlara ulaşabilmemiz için bu ayarların constructor içinde yapılması gerekir.
        //Bu constructor içinde DbContextOptionsBuilder nesnesi oluşturulur.
        //Bu constructor IoC container içerisinde doldurulur. Eğer bunu koymazsak süreçte hata alırız.
        ////Bu Class'ı IoC container içerisinde doldurmak için Startup.cs dosyasındaki ConfigureServices metodunu override ederiz.
        //IoC Container içerisine eklemek lazım çünkü gerektiği zaman erişilebilmeli.
        public ECommerceAPIDbContext(DbContextOptions<ECommerceAPIDbContext> options) : base(options)
        {

        }
        //Veri tabanına tablo oluşturmak için bu property oluşturulur.
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        //public DbSet<OrderItem> OrderItems { get; set; }
        //public DbSet<Category> Categories { get; set; }
        //public DbSet<User> Users { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //db classtan geliyor. Entityler üzerinden yapılan değişikliklerin ya da yeni eklenen verinin yakalanmasını sağlayan propertydir.
            //Update operasyonlarında track edilen veriyi yakalayıp elde edliyoruz ve düzenleme yapabiliyoruz
            ChangeTracker.Entries<BaseEntity>().ToList().ForEach(x =>
            {
                if (x.State == EntityState.Added)
                {
                    x.Property("CreatedAt").CurrentValue = DateTime.UtcNow;
                }
                if (x.State == EntityState.Modified)
                {
                    x.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;
                }
            });
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
