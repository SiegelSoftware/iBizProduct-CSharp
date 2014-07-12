// Copyright (c) iBizVision - 2014
// Author: Dan Siegel

using System.Collections.Generic;

namespace iBizProduct.DataContracts
{
    /// <summary>
    /// The ProductOrderSpec is used to define different attributes about a ProductOrder.
    /// </summary>
    public class ProductOrderSpec
    {
        private Dictionary<string, object> POSpec = new Dictionary<string, object>();

        /// <summary>
        /// Instatiate a new ProductOrderSpec. You will need to set the attributes you wish to use. 
        /// </summary>
        public ProductOrderSpec() { }

        /// <summary>
        /// The ProductOrder Name. This will be shown in the Panel.
        /// </summary>
        public string ProductOrderName { get; set; }

        /// <summary>
        /// You only want to set this if you want to bill a client a set charge every payment period.
        /// </summary>
        /// <remarks>
        /// If you want dynamic billing, you may not want to use this or set it to 0
        /// </remarks>
        public decimal? Cost { get; set; }

        /// <summary>
        /// This will create a one time charge for setting up your product.
        /// </summary>
        public decimal? Setup { get; set; }

        /// <summary>
        /// The current status of the Order. i.e. Incomplete, Complete, In Progress
        /// </summary>
        public ProductOrderStatus? ProductOrderStatus { get; set; }

        /// <summary>
        /// Enter any notes related to your productorder
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// This provides functionality to add custom attributes to the Associative Array
        /// that is built to pass to the Backend API. 
        /// </summary>
        /// <remarks>
        /// You should only use this if you know what you are doing.
        /// </remarks>
        /// <param name="key">Key of the Associative Array to be passed to the Backend API</param>
        /// <param name="value">Value of the Associative Array to be passed to the Backend API</param>
        /// <returns></returns>
        public bool AddCustomAttribute( string key, object value )
        {
            POSpec.Add( key, value );
            return true;
        }

        /// <summary>
        /// The Order Specifications that have been defined.
        /// </summary>
        /// <returns>Dictionary&lt;string,string> of the specifications</returns>
        public Dictionary<string,object> OrderSpec()
        {
            if( ProductOrderName != null )
                POSpec.Add( "productorder_name", ProductOrderName );
            if( Cost != null )
                POSpec.Add( "my_cost", Cost.ToString() );
            if( Setup != null )
                POSpec.Add( "my_setup", Setup.ToString() );
            if( ProductOrderStatus != null )
                POSpec.Add( "productorder_status", ProductOrderStatus.ToString() );
            if( Notes != null )
                POSpec.Add( "notes", Notes );

            return this.POSpec;
        }
    }
}
