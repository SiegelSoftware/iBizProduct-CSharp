using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace iBizProduct
{
    class APIClient : HttpClient
    {
        public APIClient() : base() { }
        public APIClient( HttpMessageHandler handler ) : base( handler ) { }
        public APIClient( HttpMessageHandler handler, Boolean disposeHandler ) : base( handler, disposeHandler ) { }

        public async Task<HttpResponseMessage> PostAsJsonAsync( Uri requestUri, Dictionary<string, object> value )
        {
            var address = new Uri( BaseAddress.ToString() + requestUri.ToString() );
            return await PostAsync( address, new StringContent( JsonConvert.SerializeObject( value ) ) );
        }

        public async Task<HttpResponseMessage> PostAsJsonAsync( string requestUri, Dictionary<string, object> value )
        {
            var address = new Uri( BaseAddress.ToString() + requestUri.ToString() );
            return await PostAsync( address, new StringContent( JsonConvert.SerializeObject( value ) ) );
        }
    }
}
