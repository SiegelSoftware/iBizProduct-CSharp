using System.Data.Entity;

namespace iBizProduct.Models
{
    public class ProductContext : DbContext
    {
        public ProductContext()
            : base( iBizProductSettings.ProductConnectionString )
        {

        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductSetting> ProductSettings { get; set; }
    }
}
