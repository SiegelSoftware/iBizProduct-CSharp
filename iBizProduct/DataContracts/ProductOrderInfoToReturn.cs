// Copyright (c) iBizVision - 2014
// Author: Dan Siegel

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBizProduct.DataContracts
{
    /// <summary>
    /// This provides the correct InfoToReturn Object when attempting to View a Product Order.
    /// You can use this to tailor your return to your specific needs.
    /// </summary>
    public class ProductOrderInfoToReturn : InfoToReturn
    {
        /// <summary>
        /// A productorder -> View on the parentorder_id.
        /// </summary>
        public InfoToReturn PARENTORDER_VIEW { get; set; }

        /// <summary>
        /// If TRUE, returns the productorder data.
        /// </summary>
        public bool PRODUCTORDER_ATTRIBUTE { get; set; }

        /// <summary>
        /// If TRUE, add inventory data for this order.
        /// </summary>
        public bool PRODUCTORDER_INVENTORY { get; set; }

        /// <summary>
        /// Return the product data. If this is passed as a hash, it will be passed to the Product->View
        /// function as InfoToReturn.
        /// </summary>
        public InfoToReturn PRODUCT_DATA { get; set; }

        /// <summary>
        /// If TRUE, add the provisioning data.
        /// </summary>
        public bool PROVISIONING_DATA { get; set; }

        /// <summary>
        /// If TRUE, add the List of purchases using this order.
        /// </summary>
        public bool PURCHASEORDER_LIST { get; set; }
    }
}
