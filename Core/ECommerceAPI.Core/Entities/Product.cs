using ECommerceAPI.Domain.Entities.Common;

namespace ECommerceAPI.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string? Name { get; set; }
        public int? Quantity { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }

        //Order ile Product arasında many to many ilişkisi vardır.
        public ICollection<Order>? Orders { get; set; }


        /*
          public int CategoryId { get; set; }
          public Category Category { get; set; }
         */

    }
}
