using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace iBizProduct.DataContracts.Templates
{
    /// <summary>
    /// This provides a base class for storing Event History
    /// </summary>
    /// <typeparam name="T">Should be Guid or Int depending on the standard of your Product DbContext</typeparam>
    public abstract class EventBase<T>
    {
        /// <summary>
        /// The Product's Event Id
        /// </summary>
        [DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        public T EventId { get; set; }

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
        public EventActions EventType { get; set; }

        /// <summary>
        /// The Event Id sent by the Backend iBizAPI
        /// </summary>
        [Index( IsUnique = true )]
        public int PurchaseOrderEventId { get; set; }
    }
}
