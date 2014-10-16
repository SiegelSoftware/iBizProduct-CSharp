// Copyright (c) iBizVision - 2014
// Author: Dan Siegel

using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using iBizProduct.DataContracts;

namespace iBizProduct.Models.Templates
{
    /// <summary>
    /// This provides a common framework to work with the Panel's FAQ featureset
    /// </summary>
    /// <typeparam name="T">Id type of the Keys</typeparam>
    public abstract class FAQBase<T>
    {
        /// <summary>
        /// FAQ Database Id
        /// </summary>
        [DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        public T FAQId { get; set; }

        /// <summary>
        /// FAQ ElementLanguage
        /// </summary>
        [DefaultValue( Language.EN )]
        public Language DefaultLanguage { get; set; }
    }
}
