// Copyright (c) iBizVision - 2014
// Author: Dan Siegel

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using iBizProduct.DataContracts;

namespace iBizProduct.Models.Templates
{
    /// <summary>
    /// Provides a context for storing information on a FAQ Item in multiple languages.
    /// </summary>
    /// <typeparam name="T">Id Type ( int or Guid )</typeparam>
    public abstract class FAQMessageBase<T>
    {
        /// <summary>
        /// FAQMessage Id
        /// </summary>
        [DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        public T FAQMessageId { get; set; }

        /// <summary>
        /// FAQ Database Id
        /// </summary>
        public T FAQId { get; set; }

        /// <summary>
        /// FAQ ElementLanguage
        /// </summary>
        public Language Language { get; set; }

        /// <summary>
        /// FAQ Title
        /// </summary>
        [MaxLength( 255 )]
        public string Title { get; set; }

        /// <summary>
        /// FAQ Content
        /// </summary>
        public string Content { get; set; }
    }
}
