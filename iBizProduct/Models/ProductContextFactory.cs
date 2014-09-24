using System.Data.Entity.Infrastructure;

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
