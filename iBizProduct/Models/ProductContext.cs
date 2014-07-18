using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
