using System.Data.Entity;

namespace iBizProduct.Models
{
    public class ProductContext : DbContext
    {
        public ProductContext()
            : base( "iBizProductSettings.GetProductConnectionString()" )
        {

        }

        DbSet<Product> Products { get; set; }
    }
}
