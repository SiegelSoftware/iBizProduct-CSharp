// Copyright (c) iBizVision - 2014
// Author: Dan Siegel

using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;

namespace iBizProduct.Security
{
    public class iBizSecure
    {
        // Change these keys
        private byte[] Key;
        private byte[] Vector;
        private int Salt;

        private ICryptoTransform EncryptorTransform, DecryptorTransform;
        private System.Text.UTF8Encoding UTFEncoder;

        public iBizSecure()
        {
            LoadKeys();
            //This is our encryption method
            RijndaelManaged rm = new RijndaelManaged();

            //Create an encryptor and a decryptor using our encryption method, key, and vector.
            EncryptorTransform = rm.CreateEncryptor( this.Key, this.Vector );
            DecryptorTransform = rm.CreateDecryptor( this.Key, this.Vector );

            //Used to translate bytes to text and vice versa
            UTFEncoder = new System.Text.UTF8Encoding();
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

        /// Generates a unique encryption vector
        static public byte[] GenerateEncryptionVector()
        {
            //Generate a Vector
            RijndaelManaged rm = new RijndaelManaged();
            rm.GenerateIV();
            return rm.IV;
        }


        /// ----------- The commonly used methods ------------------------------   
        /// Encrypt some text and return a string suitable for passing in a URL.
        public string EncryptString( string TextValue )
        {
            return ( TextValue != "" ) ? Convert.ToBase64String( Encrypt( TextValue ) ) : "";
        }

        /// Encrypt some text and return an encrypted byte array.
        public byte[] Encrypt( string TextValue, int userDefineSalt = 0 )
        {
            //Translates our text value into a byte array.
            Byte[] pepper = UTFEncoder.GetBytes( TextValue );

            if( userDefineSalt != 0 )
                Salt = userDefineSalt;

            Byte[] salt = new byte[ Salt ];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes( salt );
            Byte[] bytes = new byte[ 2 * Salt + pepper.Length ];
            System.Buffer.BlockCopy( salt, 0, bytes, 0, Salt );
            System.Buffer.BlockCopy( pepper, 0, bytes, Salt, pepper.Length );
            crypto.GetNonZeroBytes( salt );
            System.Buffer.BlockCopy( salt, 0, bytes, Salt + pepper.Length, Salt );

            //Used to stream the data in and out of the CryptoStream.
            MemoryStream memoryStream = new MemoryStream();

            /*
             * We will have to write the unencrypted bytes to the stream,
             * then read the encrypted result back from the stream.
             */
            #region Write the decrypted value to the encryption stream
            CryptoStream cs = new CryptoStream( memoryStream, EncryptorTransform, CryptoStreamMode.Write );
            cs.Write( bytes, 0, bytes.Length );
            cs.FlushFinalBlock();
            #endregion

            #region Read encrypted value back out of the stream
            memoryStream.Position = 0;
            byte[] encrypted = new byte[ memoryStream.Length ];
            memoryStream.Read( encrypted, 0, encrypted.Length );
            #endregion

            //Clean up.
            cs.Close();
            memoryStream.Close();

            return encrypted;
        }

        /// The other side: Decryption methods
        public string DecryptString( string EncryptedString )
        {
            return ( EncryptedString != "" ) ? Decrypt( Convert.FromBase64String( EncryptedString ) ) : "";
        }

        public string Decrypt( string EncryptedValue, string userDefinedSalt = "0" )
        {
            return Decrypt( GetBytes( EncryptedValue ), int.Parse( userDefinedSalt ) );
        }

        /// Decryption when working with byte arrays.    
        public string Decrypt( byte[] EncryptedValue, int userDefineSalt = 0 )
        {
            if( userDefineSalt != 0 )
                Salt = userDefineSalt;

            #region Write the encrypted value to the decryption stream
            MemoryStream encryptedStream = new MemoryStream();
            CryptoStream decryptStream = new CryptoStream( encryptedStream, DecryptorTransform, CryptoStreamMode.Write );
            decryptStream.Write( EncryptedValue, 0, EncryptedValue.Length );
            decryptStream.FlushFinalBlock();
            #endregion

            #region Read the decrypted value from the stream.
            encryptedStream.Position = 0;
            Byte[] decryptedBytes = new Byte[ encryptedStream.Length ];
            encryptedStream.Read( decryptedBytes, 0, decryptedBytes.Length );
            encryptedStream.Close();
            #endregion
            // remove salt
            int len = decryptedBytes.Length - 2 * Salt;
            Byte[] pepper = new Byte[ len ];
            System.Buffer.BlockCopy( decryptedBytes, Salt, pepper, 0, len );
            return UTFEncoder.GetString( pepper );
        }

        public int GenerateSalt()
        {
            Random rnd = new Random();
            return rnd.Next( 1000000, 2147483647 );
        }

        private void LoadKeys()
        {
            try
            {
                this.Key = Convert.FromBase64String( iBizProductSettings.ConfigKey );
                this.Vector = Convert.FromBase64String( iBizProductSettings.ConfigVector );
                this.Salt = BitConverter.ToInt32( Convert.FromBase64String( iBizProductSettings.ConfigSalt ), 0 );
            }
            catch( iBizException )
            {
                // If any of the values above did not exist they will throw an iBizException. We will generate new values for the Config Manager
                NewKeys();

                // Thought about doing this recursively... but the possibility for an infinite loop didn't seem worth it.
                this.Key = Convert.FromBase64String( iBizProductSettings.ConfigKey );
                this.Vector = Convert.FromBase64String( iBizProductSettings.ConfigVector );
                this.Salt = BitConverter.ToInt32( Convert.FromBase64String( iBizProductSettings.ConfigSalt ), 0 );
            }
        }

        /// <summary>
        /// Use this function to generate new encryption keys for your application.
        /// You can find the generated settings in your web.config.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool NewKeys( string name = "" )
        {
            bool wasSuccessful = false;
            if( name == "" || name == "ConfigKey" )
            {
                var NewGuid = Guid.NewGuid().ToByteArray();

                ConfigurationManager.AppSettings.Add( name: "ConfigKey", value: Convert.ToBase64String( NewGuid ) );
            }

            if( name == "" || name == "ConfigVector" )
            {
                var NewGuid = Guid.NewGuid().ToByteArray();

                ConfigurationManager.AppSettings.Add( name: "ConfigVector", value: Convert.ToBase64String( NewGuid ) );
            }

            if( name == "" || name == "ConfigSalt" )
            {
                Random random = new Random();
                ConfigurationManager.AppSettings.Add( name: "ConfigSalt", value: Convert.ToBase64String( BitConverter.GetBytes( random.Next( 2048, 4096 ) ) ) );
            }

            return wasSuccessful;
        }

        private byte[] GetBytes( string str )
        {
            byte[] bytes = new byte[ str.Length * sizeof( char ) ];
            System.Buffer.BlockCopy( str.ToCharArray(), 0, bytes, 0, bytes.Length );
            return bytes;
        }
    }
}
