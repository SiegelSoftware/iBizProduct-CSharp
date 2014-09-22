// Copyright (c) iBizVision - 2014
// Author: Dan Siegel

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace iBizProduct
{
    [Serializable]
    public sealed class iBizBackendException : iBizException
    {
        private const string Pattern = @"/Error processing (?:request|response): (\d+): ([^\[]+)\s*(?:\[ERROR_DATA: ([^\]]+)\])?\s*\[ERROR_PATH: ([^\]]+)\]/";
        public int BackendErrorId { get; private set; }
        public string ErrorData { get; private set; }
        public string ErrorPath { get; private set; }
        public string Parameters { get; private set; }
        public string Endpoint { get; private set; }

        public iBizBackendException( string Message )
            : base( GetBackendMessage( Message ) )
        {
            HandleBackendErrorString( Message );
        }

        public iBizBackendException( string Message, string Endpoint, IDictionary<string, object> Params )
            : base( GetBackendMessage( Message ) )
        {
            this.Endpoint = Endpoint;
            this.Parameters = JsonConvert.SerializeObject( Params, Formatting.Indented );
            HandleBackendErrorString( Message );
        }

        private static string GetBackendMessage( string Message )
        {
            var matches = Regex.Split( Message, Pattern );
            return matches[ 2 ];
        }

        private void HandleBackendErrorString( string Message )
        {
            var matches = Regex.Split( Message, Pattern );
            this.BackendErrorId = Convert.ToInt32( matches[ 1 ] );
            this.ErrorData = matches[ 3 ];
#if DEBUG
            this.ErrorPath = matches[4];
#else
            this.ErrorPath = "";
#endif
            //var temp = "Error processing request: 10003: The object for the passed ID does not exist. [ERROR_DATA: ProductOrderEvent, 1359374, (0)] [ERROR_PATH:  (/opt/ibizd/lib/Functions.pm,402) (/opt/ibizd/lib/CommerceManager/ProductManager.pm,2502) (/opt/ibizd/lib/CommerceManager/ProductManager/ProductOrder/Event.pm,119)]\n";

            //$pattern = '/Error processing (?:request|response): (\d+): ([^\[]+)\s*(?:\[ERROR_DATA: ([^\]]+)\])?\s*\[ERROR_PATH: ([^\]]+)\]/';
            //preg_match($pattern, trim($message), $matches);
            //if ($matches)
            //{
            //    $this->error_code = $matches[1];
            //    return trim($matches[2]) .' '. trim($matches[3] ? " ({$matches[3]})": '');
            //}
        }
    }
}
