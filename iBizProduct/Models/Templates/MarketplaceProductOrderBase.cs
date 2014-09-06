// Copyright (c) iBizVision - 2014
// Author: Dan Siegel

namespace iBizProduct.Models.Templates
{
    public abstract class MarketplaceProductOrderBase : ProductOrderBase
    {
        /// <summary>
        /// ProductId of the ProductOrder. This allows for Marketplace Implementation.
        /// </summary>
        public int ProductId { get; set; }
    }
}
