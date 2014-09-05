using System.Collections.Generic;

namespace iBizProduct.Models
{
    public class Product
    {
        public Product()
        {
            this.ProductSettings = new HashSet<ProductSetting>();
        }

        public int ProductId { get; set; }

        public string ExternalKey { get; set; }

        public string Name { get; set; }

        public ICollection<ProductSetting> ProductSettings { get; set; }
    }
}
