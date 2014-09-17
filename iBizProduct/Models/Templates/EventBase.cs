// Copyright (c) iBizVision - 2014
// Author: Dan Siegel

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using iBizProduct.DataContracts;

namespace iBizProduct.Models.Templates
{
    /// <summary>
    /// This provides a base class for storing Event History
    /// </summary>
    public abstract class EventBase
    {
        /// <summary>
        /// The Event Id sent by the Backend iBizAPI
        /// </summary>
        [Key]
        [DatabaseGenerated( DatabaseGeneratedOption.None )]
        public int EventId { get; set; }

        /// <summary>
        /// Associated Product Order Id
        /// </summary>
        public int ProductOrderId { get; set; }

        /// <summary>
        /// When the Event was first requested.
        /// </summary>
        public DateTime EventRequested { get; set; }

        /// <summary>
        /// How many API Requests were recieved.
        /// </summary>
        public int APIRequests { get; set; }

        /// <summary>
        /// When the Event completed processing.
        /// </summary>
        public Nullable<DateTime> EventCompleted { get; set; }

        /// <summary>
        /// The type of Event Requested.
        /// </summary>
        public EventActions Type { get; set; }

        /// <summary>
        /// Indicates whether the event is currently in the queue or not.
        /// </summary>
        public bool IsQueued { get; set; }
    }
}
