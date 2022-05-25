using ECommerceAPI.Domain.Entities.Common;

namespace ECommerceAPI.Domain.Entities
{
    public class Order : BaseEntity
    {
        public string CustomerMessage { get; set; }
        public string Description { get; set; }
        public string? PaymentMethod { get; set; }
        public string? PaymentStatus { get; set; }
        public string? ShippingStatus { get; set; }
        public string? OrderStatus { get; set; }
        public string? OrderCode { get; set; }
        public string? OrderNote { get; set; }

        //Customer Class'ında adres belirlemedik çünkü adres sipariş temelli olsun istiyoruz.
        public string Address { get; set; }
        public string City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public string? Country { get; set; }

        //Order ile Product arasında ilişki many to many ile oluşturulur.
        public ICollection<Product> Products { get; set; }

        //Bir Siparişin bir tane müşterisi olabilir
        public Customer Customer { get; set; }

    }
}
