// iBizVision - System.Net.Http.Formatting.dll repack

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace iBizProduct.Collections
{
    internal sealed class ListWrapperCollection<T> : Collection<T>
    {
        private readonly List<T> _items;

        internal ListWrapperCollection()
            : this( new List<T>() )
        {
        }

        internal ListWrapperCollection( List<T> list )
            : base( list )
        {
            this._items = list;
        }

        internal List<T> ItemsList
        {
            get
            {
                return this._items;
            }
        }
    }
}
