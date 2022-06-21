using ECommerceAPI.Domain.Entities.Common;

namespace ECommerceAPI.Domain.Entities
{
    public class Order : BaseEntity
    {
        public string? CustomerMessage { get; set; }
        public string? Description { get; set; }
     

        //Customer Class'ında adres belirlemedik çünkü adres sipariş temelli olsun istiyoruz.
        public string? Address { get; set; }
    
        //Order ile Product arasında ilişki many to many ile oluşturulur.
        public ICollection<Product> Products { get; set; }

        //Bir Siparişin bir tane müşterisi olabilir
        public Customer Customer { get; set; }

    }
}
