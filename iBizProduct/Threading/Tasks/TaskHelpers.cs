using System;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace iBizProduct.Threading.Tasks
{
    internal static class TaskHelpers
    {
        private static readonly Task<object> _completedTaskReturningNull = Task.FromResult<object>( null );
        private static readonly Task _defaultCompleted = Task.FromResult<AsyncVoid>( new AsyncVoid() );

        internal static Task Canceled()
        {
            return CancelCache<AsyncVoid>.Canceled;
        }

        internal static Task<TResult> Canceled<TResult>()
        {
            return CancelCache<TResult>.Canceled;
        }

        internal static Task Completed()
        {
            return _defaultCompleted;
        }

        internal static Task FromError( Exception exception )
        {
            return FromError<AsyncVoid>( exception );
        }

        internal static Task<TResult> FromError<TResult>( Exception exception )
        {
            TaskCompletionSource<TResult> source = new TaskCompletionSource<TResult>();
            source.SetException( exception );
            return source.Task;
        }

        internal static Task<object> NullResult()
        {
            return _completedTaskReturningNull;
        }

        [StructLayout( LayoutKind.Sequential, Size = 1 )]
        private struct AsyncVoid
        {
        }

        private static class CancelCache<TResult>
        {
            public static readonly Task<TResult> Canceled;

            static CancelCache()
            {
                TaskHelpers.CancelCache<TResult>.Canceled = TaskHelpers.CancelCache<TResult>.GetCancelledTask();
            }

            private static Task<TResult> GetCancelledTask()
            {
                TaskCompletionSource<TResult> source = new TaskCompletionSource<TResult>();
                source.SetCanceled();
                return source.Task;
            }
        }
    }
}
