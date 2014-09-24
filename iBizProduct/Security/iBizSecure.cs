// Copyright (c) iBizVision - 2014
// Author: William Cahill-Manley

using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;

namespace iBizProduct.Security
{
    public class iBizSecure
    {
        public string Key
        {
            get
            {
                return iBizProductSettings.ConfigKey;
            }
            set
            {
                ConfigurationManager.AppSettings.Add( name: "ConfigKey", value: value );
            }
        }





        /// -------------- Two Utility Methods (not used but may be useful) -----------
        /// Generates an encryption key.
        static public byte[] GenerateEncryptionKey()
        {
            //Generate a Key.
            RijndaelManaged rm = new RijndaelManaged();
            rm.GenerateKey();
            return rm.Key;
        }






        public string Encrypt( string textValue, EncryptionType EncryptionType)
        {
            switch(EncryptionType)
            {
                case EncryptionType.None:
                    return textValue;
                default:
                    IEncryptionInterface Encryptor = this.ConstructEncrypter( EncryptionType );
                    return Encryptor.Encrypt( textValue );

            }
        }

        public T Decrypt<T>( string encryptedValue, EncryptionType EncryptionType )
        {
            switch( EncryptionType )
            {
                case EncryptionType.None:
                    return ( T )Convert.ChangeType( encryptedValue, typeof( T ) );
                default:
                    IEncryptionInterface Decryptor = this.ConstructEncrypter( EncryptionType );
                    return Decryptor.Decrypt<T>( encryptedValue );
            }
        }

        public string Decrypt( string encryptedValue, EncryptionType EncryptionType )
        {
            return Decrypt<string>( encryptedValue, EncryptionType );
        }

        public string Hash ( string textValue, HashType HashType )
        {
            switch(HashType)
            {
                case HashType.None:
                default:
                    return textValue;
            }
        }

        // Todo: Replace this with Dependency Injection.
        private IEncryptionInterface ConstructEncrypter( EncryptionType Encryptor )
        {
            switch(Encryptor)
            {
                case EncryptionType.Base64:
                    return new Base64Encrypter();
                case EncryptionType.Laravel:
                    return new LaravelEncrypter( this.Key );
                case EncryptionType.None:
                    throw new iBizException( "Can not construct Encryptor of type None" );
                default:
                    throw new iBizException( String.Format( "EncryptionType '{0}' is not yet implemented.", Encryptor.ToString() ) );
            }


        }

    }
}
