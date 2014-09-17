using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iBizProduct.Models
{
    /// <summary>
    /// Marketplace Products
    /// </summary>
    public class Product
    {
        /// <summary>
        /// iBizProduct instance
        /// </summary>
        public Product()
        {
            this.ProductSettings = new HashSet<ProductSetting>();
        }

        /// <summary>
        /// This is the Backend Product Order Id generated when you call the iBizAPIClient's ProductOrderAdd function.
        /// </summary>
        [Key]
        [DatabaseGenerated( DatabaseGeneratedOption.None )]
        public int ProductId { get; set; }

        /// <summary>
        /// External Key associated with this ProductId
        /// </summary>
        public string ExternalKey { get; set; }

        /// <summary>
        /// Product Order Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The name used in the Uri: {hostname}/iBiz/Product/{UriName}
        /// </summary>
        public string UriName { get; set; }

        /// <summary>
        /// Associated ProductSettings
        /// </summary>
        public ICollection<ProductSetting> ProductSettings { get; set; }
    }
}
