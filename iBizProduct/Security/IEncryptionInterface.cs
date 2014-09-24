// Copyright (c) iBizVision - 2014
// Author: William Cahill-Manley

namespace iBizProduct.Security
{
    public interface IEncryptionInterface
    {

        string Encrypt( object toEncrypt );

        T Decrypt<T>( string toDecrypt );
        object Decrypt( string toDecrypt );

    }
}
