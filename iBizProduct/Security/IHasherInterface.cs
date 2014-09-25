// Copyright (c) iBizVision - 2014
// Author: William Cahill-Manley

namespace iBizProduct.Security
{
    public interface IHasherInterface
    {
        string Hash( string toHash );
        byte[] Hash( byte[] toHash );

    }
}
