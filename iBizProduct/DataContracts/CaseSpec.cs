using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace iBizProduct.DataContracts
{
    public class CaseSpec
    {
        /// <summary>
        /// True or False for automatically close the case [Default: True]
        /// </summary>
        [DefaultValue( true )]
        public bool AutoClose { get; set; }

        /// <summary>
        /// [ REQUIRED ] The few word description of the case. (Max 255 characters)
        /// </summary> 
        [StringLength( 255 )]
        public string Description { get; set; }

        /// <summary>
        /// [ CONDITIONALLY REQUIRED ] Case Details
        /// </summary>
        public string Detail { get; set; }

        /// <summary>
        /// [ CONDITIONALLY REQUIRED ] The internal notes are visible only for worker company, not customers
        /// </summary>
        public string InternalNotes { get; set; }

        /// <summary>
        /// True or False if the case is resolved [Default: False]
        /// </summary>
        [DefaultValue( false )]
        public bool IsResolved { get; set; }

        /// <summary>
        /// [ REQUIRED ] The priority of the case ("LOW", "MEDIUM", "HIGH"). 
        /// </summary>
        public Priority Priority { get; set; }

        /// <summary>
        /// [ CONDITIONALLY REQUIRED ] Set the case back to PENDING status in the specified number of hours, essentially returning 
        /// it to the one working the case. This must be specified if AutoClose is False.
        /// </summary>
        public int ReturnHours { get; set; }

        /// <summary>
        /// [ REQUIRED ] The status of the case ("NEW", "CLOSED", "RESPONSE", "UPSTREAM", "CUSTOMER", "PARTNER", "PENDING")
        /// </summary>
        public CaseStatus Status { get; set; }

        /// <summary>
        /// [ REQUIRED ] The type of case ("QUESTION", "PROBLEM", "COMMENT", "INFO", "FOLLOWUP", "PRIVATE")
        /// </summary>
        public CaseType Type { get; set; }

        /// <summary>
        /// Gets the spec to pass to the backend API
        /// </summary>
        /// <returns>Dictionary spec based on how the object has been built.</returns>
        public Dictionary<string, object> GetSpec()
        {
            if( !( Detail.Length > 0 || InternalNotes.Length > 0 ) ) throw new iBizException( "You must specificy either a case detail or internal notes." );
            if( !AutoClose && ReturnHours < 1 ) throw new iBizException( "If AutoClose is disabled you must specify the number of hours in which to have the case returned." );

            var spec = new Dictionary<string, object>()
            {
                { "auto_close", ( AutoClose ) ? "YES" : "NO" },
                { "description", Description },
                { "is_resolved", ( IsResolved ) ? "YES" : "NO" },
                { "priority", Priority },
                { "status", Status },
                { "type", Type }
            };

            if( Detail.Length > 0 ) spec.Add( "detail", Detail );

            if( InternalNotes.Length > 0 ) spec.Add( "internal_notes", InternalNotes );

            if( ReturnHours > 0 ) spec.Add( "return_hours", ReturnHours );

            return spec;
        }
    }
}
