// Copyright (c) iBizVision - 2014
// Author: Dan Siegel

using System.ComponentModel;

namespace iBizProduct.DataContracts
{
    /// <summary>
    /// Available Panel Languages
    /// </summary>
    public enum Language
    {
        /// <summary>
        /// English
        /// </summary>
        [Description( "English" )]
        EN,

        /// <summary>
        /// Español (Spanish)
        /// </summary>
        [Description( "Español (Spanish)" )]
        ES,

        /// <summary>
        /// Deutsch (German)
        /// </summary>
        [Description( "Deutsch (German)" )]
        DE,

        /// <summary>
        /// Greek
        /// </summary>
        [Description( "Greek" )]
        EL,

        /// <summary>
        /// French
        /// </summary>
        [Description( "French" )]
        FR,

        /// <summary>
        /// Italian
        /// </summary>
        [Description( "Italian" )]
        IT,

        /// <summary>
        /// Portuguese
        /// </summary>
        [Description( "Portuguese" )]
        PT,

        /// <summary>
        /// Russian
        /// </summary>
        [Description( "Russian" )]
        RU,

        /// <summary>
        /// Turkish
        /// </summary>
        [Description( "Turkish" )]
        TR,

        /// <summary>
        /// Vietnamese
        /// </summary>
        [Description( "Vietnamese" )]
        VI
    }
}
