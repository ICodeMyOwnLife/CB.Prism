using System;
using System.Threading.Tasks;


namespace CB.Prism.Interactivity
{
    internal static class CallbackHelper
    {
        #region Methods
        public static Task<TResult> AwaitCallbackResult<TResult>(Action<Action<TResult>> f)
        {
            var tcs = new TaskCompletionSource<TResult>();
            f(n => tcs.SetResult(n));
            return tcs.Task;
        }
        #endregion
    }
}