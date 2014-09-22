// Copyright (c) iBizVision - 2014
// Author: Dan Siegel

using System;
using System.Collections.Generic;

namespace iBizProduct.Ultilities
{
    /// <summary>
    /// Abstracts out the EventLog Collection
    /// </summary>
    [Serializable]
    public class EventLogCollection : Dictionary<string, string>
    {
        public EventLogCollection()
            : base()
        {
            this.Add( "iBizProduct", "Operational" );
        }
        /// <summary>
        /// Add a new Event Log to Create
        /// </summary>
        /// <param name="ApplicationName">Name of the Product or Application</param>
        /// <param name="LogName">Specific name of the Application. DEFAULT: Operational</param>
        public void Add( string ApplicationName, string LogName = "Operational" )
        {
            base.Add( ApplicationName, LogName );
        }
    }
}
