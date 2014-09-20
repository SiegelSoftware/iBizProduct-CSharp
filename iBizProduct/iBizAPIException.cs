using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBizProduct
{
    public class iBizBackendException : iBizException
    {
        public int BackendErrorId { get; set; }

        public iBizBackendException( string Message )
        {

        }

        private string HandleBackendErrorString( string Message )
        {

            var temp = "Error processing request: 10003: The object for the passed ID does not exist. [ERROR_DATA: ProductOrderEvent, 1359374, (0)] [ERROR_PATH:  (/opt/ibizd/lib/Functions.pm,402) (/opt/ibizd/lib/CommerceManager/ProductManager.pm,2502) (/opt/ibizd/lib/CommerceManager/ProductManager/ProductOrder/Event.pm,119)]\n";

//$pattern = '/Error processing (?:request|response): (\d+): ([^\[]+)\s*(?:\[ERROR_DATA: ([^\]]+)\])?\s*\[ERROR_PATH: ([^\]]+)\]/';

//preg_match($pattern, trim($message), $matches);
//if ($matches)
//{
//$this->error_code = $matches[1];
//return trim($matches[2]) .' '. trim($matches[3] ? " ({$matches[3]})": '');
//}


            return Message;
        }
    }
}
