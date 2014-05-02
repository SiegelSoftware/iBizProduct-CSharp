// iBizVision - System.Net.Http.Formatting.dll repack

using System;
using System.IO;

namespace iBizProduct.Http.Internal
{
    internal class NonClosingDelegatingStream : DelegatingStream
    {
        public NonClosingDelegatingStream( Stream innerStream )
            : base( innerStream )
        {
        }

        public override void Close()
        {
        }
    }
}
