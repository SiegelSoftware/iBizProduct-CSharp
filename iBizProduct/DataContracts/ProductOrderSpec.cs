// Copyright (c) iBizVision - 2014
// Author: Dan Siegel

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBizProduct.DataContracts
{
    public class ProductOrderSpec : Dictionary<string, string>
    {
        public string ProductOrderName
        {
            get
            {
                return this[ "productorder_name" ];
            }
            set
            {
                this.Add( "productorder_name", ProductOrderName );
            }
        }

        public string Cost
        {
            get
            {
                return this[ "my_cost" ];
            }
            set
            {
                this.Add( "my_cost", Cost );
            }
        }

        public string Setup
        {
            get
            {
                return this[ "my_setup" ];
            }
            set
            {
                this.Add( "my_setup", Setup );
            }
        }

        public ProductOrderStatus ProductOrderStatus
        {
            get
            {
                return (ProductOrderStatus) Enum.Parse( typeof( ProductOrderStatus ), this[ "productorder_status" ] );
            }
            set
            {
                this.Add( "productorder_status", ProductOrderStatus.ToString() );
            }
        }
    }
}
