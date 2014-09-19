
using System;
namespace iBizProduct
{
    public class NullSettingValueException : NullReferenceException
    {
        private const string baseMessage = "No setting was for for {0}. Please check your environmental configurations.";
        private const string baseMessageForProduct = "Product {0} could not find a value for {1}. Please check your environmental configurations.";

        public NullSettingValueException( string SettingName ) 
            : base( string.Format( baseMessage, SettingName ) ) 
        {

        }

        public NullSettingValueException( string SettingName, int ProductId ) 
            : base( string.Format( baseMessageForProduct, ProductId, SettingName ) )
        {

        }

        public NullSettingValueException( string SettingName, Exception InnerException ) 
            : base( string.Format( baseMessage, SettingName), InnerException )
        {

        }

        public NullSettingValueException( string SettingName, int ProductId, Exception InnerException )
            : base( string.Format( baseMessageForProduct, ProductId, SettingName ), InnerException )
        {

        }
    }
}
