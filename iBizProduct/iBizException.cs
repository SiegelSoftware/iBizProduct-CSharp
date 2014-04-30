﻿using System;

namespace iBizProduct
{
    public class iBizException : Exception
    {
        public iBizException() { }

        public iBizException( string message ) : base( message ) { LogError( message );  }

        public iBizException( string message, Exception inner ) : base( message, inner ) { LogError( message ); }

        private void LogError( string message = "An iBizException has been thrown" )
        {
            Console.WriteLine( message );
        }
    }
}
