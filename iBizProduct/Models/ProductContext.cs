using System;
using System.Data.Entity;

namespace iBizProduct.Models
{
    /// <summary>
    /// Creates a new ProductContext
    /// </summary>
    public class ProductContext : DbContext
    {
        /// <summary>
        /// Create a new instance of the ProductContext using the default Connection String
        /// </summary>
        /// <param name="Log">Optionally pass logging parameter i.e. Console.WriteLine</param>
        public ProductContext( Action<string> Log = null )
            : base( iBizProductSettings.ProductConnectionString )
        {
            if( Log != null ) this.Database.Log = Log;
        }

        /// <summary>
        /// Create a new instance of the ProductContext using a custom Connection String
        /// </summary>
        /// <param name="nameOrConnectionString">Custom Connection String</param>
        /// <param name="Log">Optionally pass logging parameter i.e. Console.WriteLine</param>
        public ProductContext( string nameOrConnectionString, Action<string> Log = null )
            : base( nameOrConnectionString )
        {
            if( Log != null ) this.Database.Log = Log;
        }

        /// <summary>
        /// Marketplace Product Instances
        /// </summary>
        public virtual DbSet<Product> Products { get; set; }

        /// <summary>
        /// Marketplace ProductSettings specific to a particular product
        /// </summary>
        public virtual DbSet<ProductSetting> ProductSettings { get; set; }
    }
}
