// Copyright (c) iBizVision - 2014
// Author: Dan Siegel

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;

namespace iBizProduct.DataContracts
{
    public class ProductOrderSpec
    {
        private Dictionary<string, string> POSpec = new Dictionary<string, string>();

        public string ProductOrderName { get; set; }

        /// <summary>
        /// You only want to set this if you want to bill a client a set charge every payment period.
        /// </summary>
        /// <remarks>
        /// If you want dynamic billing, you may not want to use this or set it to 0
        /// </remarks>
        public decimal Cost { get; set; }

        /// <summary>
        /// This will create a one time charge for setting up your product.
        /// </summary>
        public decimal Setup { get; set; }

        /// <summary>
        /// The current status of the Order. i.e. Incomplete, Complete, In Progress
        /// </summary>
        public ProductOrderStatus ProductOrderStatus { get; set; }

        /// <summary>
        /// Enter any notes related to your productorder
        /// </summary>
        public string Notes { get; set; }
        

        /// <summary>
        /// The Order Specifications that have been defined.
        /// </summary>
        /// <returns>Dictionary<string,string> of the specifications</returns>
        public Dictionary<string,string> OrderSpec()
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
