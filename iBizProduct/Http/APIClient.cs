// Copyright (c) iBizVision - 2014
// Author: Dan Siegel

using System.Net.Http;

namespace iBizProduct.Http
{
    public class APIClient : HttpClient
    {
        public APIClient() : base() { }
        public APIClient( HttpMessageHandler handler ) : base( handler ) { }
        public APIClient( HttpMessageHandler handler, bool disposeHandler ) : base( handler, disposeHandler ) { }
    }
}
