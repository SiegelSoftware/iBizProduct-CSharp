// iBizVision - System.Net.Http.Formatting.dll repack

using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace iBizProduct.Http
{
    internal static class TypeExtensions
    {
        public static Type ExtractGenericInterface( this Type queryType, Type interfaceType )
        {
            Func<Type, bool> predicate = t => t.IsGenericType() && ( t.GetGenericTypeDefinition() == interfaceType );
            if( !predicate( queryType ) )
            {
                return queryType.GetInterfaces().FirstOrDefault<Type>( predicate );
            }
            return queryType;
        }

        public static bool IsGenericType( this Type type )
        {
            return type.IsGenericType;
        }

        public static bool IsInterface( this Type type )
        {
            return type.IsInterface;
        }

        public static bool IsValueType( this Type type )
        {
            return type.IsValueType;
        }
    }
}
