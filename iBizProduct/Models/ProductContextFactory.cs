using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBizProduct.Models
{
    internal class ProductContextFactory : IDbContextFactory<ProductContext>
    {
        public ProductContext Create()
        {
            return new ProductContext( @"Data Source=iBizProduct.sdf" );
        }
    }
}
