using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace iBizProduct.DataContracts.Templates
{
    /// <summary>
    /// This provides a base class for Queuing Events
    /// </summary>
    /// <typeparam name="T">Should be Guid or Int depending on the standard of your Product DbContext</typeparam>
    public abstract class EventQueueBase<T>
    {
        /// <summary>
        /// The Event Queue Id
        /// </summary>
        [DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        public T EventQueueId { get; set; }

        /// <summary>
        /// The Associated Event Id - This is a 1 - 1 relationship
        /// </summary>
        public T EventId { get; set; }

        /// <summary>
        /// This is the Event Id as sent by the Backend iBizAPI
        /// </summary>
        [Index( IsUnique = true )]
        public int PurchaseOrderEventId { get; set; }

        /// <summary>
        /// This notes the type of event to perform
        /// </summary>
        public EventActions EventType { get; set; }

        /// <summary>
        /// This notes the time the event was first queued
        /// </summary>
        public DateTime TimeRecieved { get; set; }
    }
}
