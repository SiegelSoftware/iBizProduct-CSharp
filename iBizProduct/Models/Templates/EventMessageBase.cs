using System;
using System.ComponentModel.DataAnnotations.Schema;
using iBizProduct.DataContracts;

namespace iBizProduct.Models.Templates
{
    /// <summary>
    /// This provides a base class for working with Event Messages.
    /// </summary>
    /// <typeparam name="T">Should be Guid or Int depending on the standard of your Product DbContext</typeparam>
    public abstract class EventMessageBase<T>
    {
        /// <summary>
        /// The Event Message Id
        /// </summary>
        [DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        public T EventMessageId { get; }

        /// <summary>
        /// The associated Event Id
        /// </summary>
        public int EventId { get; set; }

        /// <summary>
        /// The Status of the event as of this message
        /// </summary>
        public EventStatus Status { get; set; } 

        /// <summary>
        /// The message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The time of the message.
        /// </summary>
        [DatabaseGenerated( DatabaseGeneratedOption.Computed )]
        public DateTime Time { get; }
    }
}
