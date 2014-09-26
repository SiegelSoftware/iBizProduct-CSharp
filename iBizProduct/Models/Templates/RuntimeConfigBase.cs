// Copyright (c) iBizVision - 2014
// Author: Dan Siegel

using System.ComponentModel.DataAnnotations;
using iBizProduct.Security;

namespace iBizProduct.Models.Templates
{
    public class RuntimeConfigBase
    {
        [Key]
        public string Key { get; set; }
        public string Value { get; set; }
        public EncryptionType EncryptionType { get; set; }
    }
}
