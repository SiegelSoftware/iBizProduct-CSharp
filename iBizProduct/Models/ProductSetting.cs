using System;

namespace iBizProduct.Models
{
    public class ProductSetting
    {
        public Guid ProductSettingId { get; set; }

        public int ProductId { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }

        public virtual Product Product { get; set; }
    }
}
