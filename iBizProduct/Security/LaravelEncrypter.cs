// Copyright (c) iBizVision - 2014
// Author: William Cahill-Manley

using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace iBizProduct.Security
{
    public class LaravelEncrypter : IEncryptionInterface
    {
        protected string Key { get; set; }

        protected string Cipher { get; set; }

        protected CipherMode CipherMode { get; set; }

        protected int BlockSize { get; set; }

        protected RijndaelManaged Manager { get; set; }

        public LaravelEncrypter( string Key )
        {
            this.Key = Key;
            this.CipherMode = CipherMode.CBC;
            this.BlockSize = 128;

            this.Manager = new RijndaelManaged()
            {
                Key = Encoding.UTF8.GetBytes( Key ),
                Mode = this.CipherMode,
                BlockSize = this.BlockSize,
                Padding = PaddingMode.PKCS7
            };

        }

        public string Encrypt( object toEncrypt )
        {
            ICryptoTransform Encryptor = this.Manager.CreateEncryptor( this.Manager.Key, this.Manager.IV );

            byte[] encrypted;

            PHPSerializer phpSerializer = new PHPSerializer();
            string cereal = phpSerializer.Serialize( toEncrypt );

            using( MemoryStream msEncrypt = new MemoryStream() )
            {
                using( CryptoStream csEncrypt = new CryptoStream( msEncrypt, Encryptor, CryptoStreamMode.Write ) )
                {
                    using( StreamWriter swEncrypt = new StreamWriter( csEncrypt ) )
                    {
                        swEncrypt.Write( cereal );
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }

            Dictionary<string, string> Items = new Dictionary<string, string>();
            Items.Add( "iv", Convert.ToBase64String( this.Manager.IV ) );
            Items.Add( "value", Convert.ToBase64String( encrypted ) );
            Items.Add( "mac", this.Hash<string>( Items[ "iv" ], Items[ "value" ] ) );

            string json = JsonConvert.SerializeObject( Items );
            byte[] jsonBits = Encoding.Default.GetBytes( json );
            string encoded = Convert.ToBase64String( jsonBits );

            return encoded;
        }

        public T Decrypt<T>( string toDecrypt )
        {
            var decrypted = this.Decrypt( toDecrypt );
            return ( T )Convert.ChangeType( decrypted, typeof( T ) );
        }

        public object Decrypt( string toDecrypt )
        {

            byte[] jsonBits = Convert.FromBase64String( toDecrypt );
            string json = Encoding.Default.GetString( jsonBits );
            Dictionary<string, string> Items = JsonConvert.DeserializeObject<Dictionary<string, string>>( json );
            this.ValidateMac( Items );
            this.Manager.IV = Convert.FromBase64String( Items[ "iv" ] );
            byte[] encrypted = Convert.FromBase64String( Items[ "value" ] );

            ICryptoTransform Decryptor = this.Manager.CreateDecryptor( this.Manager.Key, this.Manager.IV );

            string cereal;

            using( MemoryStream msDecrypt = new MemoryStream( encrypted ) )
            {
                using( CryptoStream csDecrypt = new CryptoStream( msDecrypt, Decryptor, CryptoStreamMode.Read ) )
                {
                    using( StreamReader srDecrypt = new StreamReader( csDecrypt ) )
                    {
                        cereal = srDecrypt.ReadToEnd();
                    }
                }
            }

            PHPSerializer phpSerializer = new PHPSerializer();

            return phpSerializer.Deserialize( cereal );
        }

        private T Hash<T>( string iv, string value )
        {
            return this.Hash<T>( iv + value );
        }

        private T Hash<T>( string value )
        {
            return this.Hash<T>( Encoding.Default.GetBytes( value ) );
        }

        private T Hash<T>( byte[] value )
        {
            using( var hasher = new HMACSHA256( this.Manager.Key ) )
            {
                hasher.ComputeHash( value );

                if( typeof( T ) == typeof( string ) )
                {
                    string sbinary = "";
                    for( int i = 0; i < hasher.Hash.Length; i++ )
                        sbinary += hasher.Hash[ i ].ToString( "X2" );
                    return ( T )Convert.ChangeType( sbinary.ToLower(), typeof( T ) );
                }

                return ( T )Convert.ChangeType( hasher.Hash, typeof( T ) );
            }
        }

        private void ValidateMac( Dictionary<string, string> Items )
        {
            Random Randomizer = new Random();
            byte[] bytes = new Byte[ 16 ];
            Randomizer.NextBytes( bytes );

            string mac = this.Hash<string>( Items[ "iv" ], Items[ "value" ] );

            byte[] calcMac = this.Hash<byte[]>( mac );
            byte[] verifyMac = this.Hash<byte[]>( Items[ "mac" ] );

            if( calcMac != verifyMac )
            {
                // throw iBizException
            }

        }


    }
}
