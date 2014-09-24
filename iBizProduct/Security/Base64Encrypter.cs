// Copyright (c) iBizVision - 2014
// Author: William Cahill-Manley

using System;
using System.Text;

namespace iBizProduct.Security
{
    public class Base64Encrypter : IEncryptionInterface
    {
        public string Encrypt( object toEncrypt )
        {
            return Convert.ToBase64String( Encoding.Default.GetBytes( toEncrypt.ToString() ) );
        }

        public object Decrypt( string toDecrypt )
        {
            return Encoding.Default.GetString( Convert.FromBase64String( toDecrypt ) );
        }

        public T Decrypt<T>( string toDecrypt )
        {
            var decoded = this.Decrypt( toDecrypt );
            return ( T )Convert.ChangeType( decoded, typeof( T ) );
        }
    }
}
