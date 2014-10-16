// Copyright (c) iBizVision - 2014
// Author: Dan Siegel

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using iBizProduct.Security;

namespace iBizProduct.Models.Templates
{
    public class RuntimeConfigBase
    {
        /// <summary>
        /// The descriptive Key for the setting being stored
        /// </summary>
        [Key]
        public string Key { get; set; }

        /// <summary>
        /// The Value of the Runtime Configuration
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// The encryption type used on the value. 
        /// </summary>
        [DefaultValue( EncryptionType.None )]
        public EncryptionType EncryptionType { get; set; }
    }
}
