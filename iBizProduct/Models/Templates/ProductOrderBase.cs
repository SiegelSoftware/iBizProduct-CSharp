// Copyright (c) iBizVision - 2014
// Author: Dan Siegel

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using iBizProduct.DataContracts;
namespace iBizProduct.Models.Templates
{
    /// <summary>
    /// This provides a base model for a ProductOrder. This may be applied to any model
    /// within the DbContext of your product which will be tracking the specific ProductOrder.
    /// </summary>
    public abstract class ProductOrderBase
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public ProductOrderBase() { }

        /// <summary>
        /// Allows you to construct a ProductOrdre based on a ProductOrderSpec
        /// </summary>
        /// <param name="OrderSpec"></param>
        public ProductOrderBase( ProductOrderSpec OrderSpec )
        {
            this.Cost = ( decimal )OrderSpec.Cost;
            this.Setup = ( decimal )OrderSpec.Setup;
            this.ProductOrderName = OrderSpec.ProductOrderName;
            this.ProductOrderStatus = OrderSpec.ProductOrderStatus;
            this.Notes = OrderSpec.Notes;
        }

        /// <summary>
        /// This is the Backend Product Order Id generated when you call the iBizAPIClient's ProductOrderAdd function.
        /// </summary>
        [Key]
        [DatabaseGenerated( DatabaseGeneratedOption.None )]
        public int ProductOrderId { get; set; }

        /// <summary>
        /// The recurring cost of the Product Order
        /// </summary>
        public decimal Cost { get; set; }

        /// <summary>
        /// The setup cost of the Product Order
        /// </summary>
        public decimal Setup { get; set; }

        /// <summary>
        /// This is the name that you should use when you create the Product Order with the Panel.
        /// </summary>
        public string ProductOrderName { get; set; }

        /// <summary>
        /// The current status of the Order. i.e. Incomplete, Complete, In Progress
        /// </summary>
        public ProductOrderStatus? ProductOrderStatus { get; set; }

        /// <summary>
        /// Enter any notes related to your ProductOrder
        /// </summary>
        public string Notes { get; set; }
    }
}
