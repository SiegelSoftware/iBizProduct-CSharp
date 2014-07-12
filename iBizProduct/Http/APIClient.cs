// Copyright (c) iBizVision - 2014
// Author: Dan Siegel

using System.Net.Http;

namespace iBizProduct.Http
{
    /// <summary>
    /// Provides a base class for sending web requests to a specified API and receiving responses.
    /// </summary>
    public class APIClient : HttpClient
    {
        /// <summary>
        /// Initializes a new instance of the APIClient class. Adds a default Handler to ignore SSL Errors
        /// </summary>
        public APIClient() : base( new WebRequestHandler() { ServerCertificateValidationCallback = ( sender, cert, chain, sslPolicyErrors ) => true }, true ) { }

        /// <summary>
        /// Initializes a new instance of the APIClient class, with a specified handler.
        /// </summary>
        /// <param name="handler">The HTTP handler stack to use for sending requests.</param>
        public APIClient( HttpMessageHandler handler ) : base( handler, true ) { }

        /// <summary>
        /// Initializes a new instance of the APIClient class, with a specified handler and boolean indicating whether this object
        /// is responsible for disposing of the specified handler.
        /// </summary>
        /// <param name="handler">The HTTP handler stack to use for sending requests.</param>
        /// <param name="disposeHandler">true if the inner handler should be disposed of by the Dispose method, 
        /// false if you intend to reuse the inner handler.</param>
        public APIClient( HttpMessageHandler handler, bool disposeHandler ) : base( handler, disposeHandler ) { }
    }
}
