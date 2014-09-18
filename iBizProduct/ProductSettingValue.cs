using System.ComponentModel;
using iBizProduct.Security;

namespace iBizProduct
{
    public class ProductSettingValue
    {
        public string Value { get; set; }

        [DefaultValue( typeof( EncryptionType ), "None" )]
        public EncryptionType Encryption { get; set; }
    }
}
