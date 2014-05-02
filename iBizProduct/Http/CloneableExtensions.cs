﻿// iBizVision - System.Net.Http.Formatting.dll repack

using System;

namespace iBizProduct.Http
{
    internal static class CloneableExtensions
    {
        /// <summary>
        /// Convenience method for cloning objects that implement <see cref="ICloneable"/> explicitly.
        /// </summary>
        /// <typeparam name="T">The type of the cloneable object.</typeparam>
        /// <param name="value">The cloneable object.</param>
        /// <returns>The result of cloning the <paramref name="value"/>.</returns>
        internal static T Clone<T>( this T value ) where T : ICloneable
        {
            return ( T )value.Clone();
        }
    }
}
