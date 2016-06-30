using System;
using System.Threading.Tasks;


namespace CB.Prism.Interactivity
{
    public class CommonInteractionRequest: ICommonInteractionRequest
    {
        #region Events
        public event EventHandler<ContextRequestEventArgs> Raised;
        #endregion


        #region Methods
        public void Raise(IContextRequestObject context, Action<IContextRequestObject> callback = null)
            => OnRaised(new ContextRequestEventArgs(context, () => callback?.Invoke(context)));

        public void Raise<TRequest>(TRequest requestObject, Action<TRequest> callback = null)
            where TRequest: IContextRequestObject
            => OnRaised(new ContextRequestEventArgs(requestObject, () => callback?.Invoke(requestObject)));

        public Task<IContextRequestObject> RaiseAsync(IContextRequestObject context)
            => CallbackHelper.AwaitCallbackResult<IContextRequestObject>(callback => Raise(context, callback));

        public Task<TRequest> RaiseAsync<TRequest>(TRequest requestObject) where TRequest: IContextRequestObject
            => CallbackHelper.AwaitCallbackResult<TRequest>(callback => Raise(requestObject, callback));
        #endregion


        #region Implementation
        protected virtual void OnRaised(ContextRequestEventArgs e) => Raised?.Invoke(this, e);
        #endregion
    }
}