using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iBizProduct;
using iBizProduct.DataContracts;
using iBizProduct.Ultilities;

namespace Debugger
{
    class Program
    {
        static void Main( string[] args )
        {
            EventLogCollection customlogs = new EventLogCollection();
            customlogs.Add( "BillingService", "AzureProductR2" );
            customlogs.Add( "BudgetChangeService", "AzureProductR2" );
            customlogs.Add( "FailedPaymentService", "AzureProductR2" );
            customlogs.Add( "EventsService", "AzureProductR2" );
            EventLogger.SetupLogs(customlogs);
        }
    }
}
