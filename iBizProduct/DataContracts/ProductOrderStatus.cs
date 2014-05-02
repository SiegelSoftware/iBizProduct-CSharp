// Copyright (c) iBizVision - 2014
// Author: Dan Siegel

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBizProduct.DataContracts
{
    // TODO: John Please complete the a summary for each enum status
    public enum ProductOrderStatus
    {
        /// <summary>
        /// 
        /// </summary>
        INCOMPLETE,

        /// <summary>
        /// 
        /// </summary>
        COMPLETE,

        /// <summary>
        /// 
        /// </summary>
        ACTIVE,

        /// <summary>
        /// 
        /// </summary>
        SUSPENDED,

        /// <summary>
        /// 
        /// </summary>
        IN_PROGRESS_INV,

        /// <summary>
        /// 
        /// </summary>
        IN_PROGRESS_ADD,

        /// <summary>
        /// 
        /// </summary>
        IN_PROGRESS_EDIT,

        /// <summary>
        /// 
        /// </summary>
        IN_PROGRESS_SUS,

        /// <summary>
        /// 
        /// </summary>
        IN_PROGRESS_DELETE,

        /// <summary>
        /// 
        /// </summary>
        IN_PROGRESS_UNSUS
    }
}
