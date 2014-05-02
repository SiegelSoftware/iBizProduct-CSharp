// iBizVision - System.Net.Http.Formatting.dll repack

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;

namespace iBizProduct.Http.Properties
{
    [DebuggerNonUserCode, CompilerGenerated, GeneratedCode( "System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0" )]
    internal class CommonWebApiResources
    {
        private static CultureInfo resourceCulture;
        private static System.Resources.ResourceManager resourceMan;

        internal CommonWebApiResources()
        {
        }

        internal static string ArgumentInvalidAbsoluteUri
        {
            get
            {
                return ResourceManager.GetString( "ArgumentInvalidAbsoluteUri", resourceCulture );
            }
        }

        internal static string ArgumentInvalidHttpUriScheme
        {
            get
            {
                return ResourceManager.GetString( "ArgumentInvalidHttpUriScheme", resourceCulture );
            }
        }

        internal static string ArgumentMustBeGreaterThanOrEqualTo
        {
            get
            {
                return ResourceManager.GetString( "ArgumentMustBeGreaterThanOrEqualTo", resourceCulture );
            }
        }

        internal static string ArgumentMustBeLessThanOrEqualTo
        {
            get
            {
                return ResourceManager.GetString( "ArgumentMustBeLessThanOrEqualTo", resourceCulture );
            }
        }

        internal static string ArgumentNullOrEmpty
        {
            get
            {
                return ResourceManager.GetString( "ArgumentNullOrEmpty", resourceCulture );
            }
        }

        internal static string ArgumentUriHasQueryOrFragment
        {
            get
            {
                return ResourceManager.GetString( "ArgumentUriHasQueryOrFragment", resourceCulture );
            }
        }

        [EditorBrowsable( EditorBrowsableState.Advanced )]
        internal static CultureInfo Culture
        {
            get
            {
                return resourceCulture;
            }
            set
            {
                resourceCulture = value;
            }
        }

        internal static string InvalidEnumArgument
        {
            get
            {
                return ResourceManager.GetString( "InvalidEnumArgument", resourceCulture );
            }
        }

        [EditorBrowsable( EditorBrowsableState.Advanced )]
        internal static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if( object.ReferenceEquals( resourceMan, null ) )
                {
                    Assembly assembly = typeof( CommonWebApiResources ).Assembly;
                    string str = ( from s in assembly.GetManifestResourceNames()
                                   where s.EndsWith( "CommonWebApiResources.resources", StringComparison.OrdinalIgnoreCase )
                                   select s ).Single<string>();
                    System.Resources.ResourceManager manager = new System.Resources.ResourceManager( str.Substring( 0, str.Length - 10 ), assembly );
                    resourceMan = manager;
                }
                return resourceMan;
            }
        }
    }
}
