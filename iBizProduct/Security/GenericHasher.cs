using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace iBizProduct.Security
{
    class GenericHasher : IHasherInterface
    {
        private HashAlgorithm Manager { get; set; }

        public GenericHasher(HashAlgorithm manager)
        {
            this.Manager = manager;
        }

        public byte[] Hash ( byte[] toHash )
        {
            return this.Manager.ComputeHash( toHash );
        }

        public string Hash ( string toHash )
        {
            byte[] hash = this.Hash( Encoding.UTF8.GetBytes( toHash ) );

            string hexHash = "";

            foreach(byte x in hash)
            {
                hexHash += String.Format( "{0:x2}", x );
            }

            return hexHash;
        }
    }
}
