// iBizVision - System.Net.Http.Formatting.dll repack

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

namespace iBizProduct.Collections
{
    internal static class CollectionExtensions
    {
        public static T[] AppendAndReallocate<T>( this T[] array, T value )
        {
            int length = array.Length;
            T[] localArray = new T[ length + 1 ];
            array.CopyTo( localArray, 0 );
            localArray[ length ] = value;
            return localArray;
        }

        public static T[] AsArray<T>( this IEnumerable<T> values )
        {
            T[] localArray = values as T[];
            if( localArray == null )
            {
                localArray = values.ToArray<T>();
            }
            return localArray;
        }

        public static Collection<T> AsCollection<T>( this IEnumerable<T> enumerable )
        {
            Collection<T> collection = enumerable as Collection<T>;
            if( collection != null )
            {
                return collection;
            }
            IList<T> list = enumerable as IList<T>;
            if( list == null )
            {
                list = new List<T>( enumerable );
            }
            return new Collection<T>( list );
        }

        public static IList<T> AsIList<T>( this IEnumerable<T> enumerable )
        {
            IList<T> list = enumerable as IList<T>;
            if( list != null )
            {
                return list;
            }
            return new List<T>( enumerable );
        }

        public static List<T> AsList<T>( this IEnumerable<T> enumerable )
        {
            List<T> list = enumerable as List<T>;
            if( list != null )
            {
                return list;
            }
            ListWrapperCollection<T> wrappers = enumerable as ListWrapperCollection<T>;
            if( wrappers != null )
            {
                return wrappers.ItemsList;
            }
            return new List<T>( enumerable );
        }

        public static void RemoveFrom<T>( this List<T> list, int start )
        {
            list.RemoveRange( start, list.Count - start );
        }

        public static T SingleDefaultOrError<T, TArg1>( this IList<T> list, Action<TArg1> errorAction, TArg1 errorArg1 )
        {
            switch( list.Count )
            {
                case 0:
                    return default( T );

                case 1:
                    return list[ 0 ];
            }
            errorAction( errorArg1 );
            return default( T );
        }

        public static TMatch SingleOfTypeDefaultOrError<TInput, TMatch, TArg1>( this IList<TInput> list, Action<TArg1> errorAction, TArg1 errorArg1 ) where TMatch : class
        {
            TMatch local = default( TMatch );
            for( int i = 0; i < list.Count; i++ )
            {
                TMatch local2 = list[ i ] as TMatch;
                if( local2 != null )
                {
                    if( local != null )
                    {
                        errorAction( errorArg1 );
                        return default( TMatch );
                    }
                    local = local2;
                }
            }
            return local;
        }

        public static T[] ToArrayWithoutNulls<T>( this ICollection<T> collection ) where T : class
        {
            T[] sourceArray = new T[ collection.Count ];
            int index = 0;
            foreach( T local in collection )
            {
                if( local != null )
                {
                    sourceArray[ index ] = local;
                    index++;
                }
            }
            if( index == collection.Count )
            {
                return sourceArray;
            }
            T[] destinationArray = new T[ index ];
            Array.Copy( sourceArray, destinationArray, index );
            return destinationArray;
        }

        public static Dictionary<TKey, TValue> ToDictionaryFast<TKey, TValue>( this TValue[] array, Func<TValue, TKey> keySelector, IEqualityComparer<TKey> comparer )
        {
            Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>( array.Length, comparer );
            for( int i = 0; i < array.Length; i++ )
            {
                TValue local = array[ i ];
                dictionary.Add( keySelector( local ), local );
            }
            return dictionary;
        }

        public static Dictionary<TKey, TValue> ToDictionaryFast<TKey, TValue>( this IEnumerable<TValue> enumerable, Func<TValue, TKey> keySelector, IEqualityComparer<TKey> comparer )
        {
            TValue[] array = enumerable as TValue[];
            if( array != null )
            {
                return array.ToDictionaryFast<TKey, TValue>( keySelector, comparer );
            }
            IList<TValue> list = enumerable as IList<TValue>;
            if( list != null )
            {
                return ToDictionaryFastNoCheck<TKey, TValue>( list, keySelector, comparer );
            }
            Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>( comparer );
            foreach( TValue local in enumerable )
            {
                dictionary.Add( keySelector( local ), local );
            }
            return dictionary;
        }

        public static Dictionary<TKey, TValue> ToDictionaryFast<TKey, TValue>( this IList<TValue> list, Func<TValue, TKey> keySelector, IEqualityComparer<TKey> comparer )
        {
            TValue[] array = list as TValue[];
            if( array != null )
            {
                return array.ToDictionaryFast<TKey, TValue>( keySelector, comparer );
            }
            return ToDictionaryFastNoCheck<TKey, TValue>( list, keySelector, comparer );
        }

        private static Dictionary<TKey, TValue> ToDictionaryFastNoCheck<TKey, TValue>( IList<TValue> list, Func<TValue, TKey> keySelector, IEqualityComparer<TKey> comparer )
        {
            int count = list.Count;
            Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>( count, comparer );
            for( int i = 0; i < count; i++ )
            {
                TValue local = list[ i ];
                dictionary.Add( keySelector( local ), local );
            }
            return dictionary;
        }
    }
}
