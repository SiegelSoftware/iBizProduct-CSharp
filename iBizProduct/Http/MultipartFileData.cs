// iBizVision - System.Net.Http.Formatting.dll repack

using System.Net.Http.Headers;
using System.Web.Http;
using System.Net.Http;

namespace iBizProduct.Http
{
    public class MultipartFileData
    {
        public MultipartFileData( HttpContentHeaders headers, string localFileName )
        {
            if( headers == null )
            {
                throw Error.ArgumentNull( "headers" );
            }

            if( localFileName == null )
            {
                throw Error.ArgumentNull( "localFileName" );
            }

            Headers = headers;
            LocalFileName = localFileName;
        }

        public HttpContentHeaders Headers { get; private set; }

        public string LocalFileName { get; private set; }
    }
}
