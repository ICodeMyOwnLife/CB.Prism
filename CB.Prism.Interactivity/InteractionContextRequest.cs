using System;
using System.Threading.Tasks;


namespace CB.Prism.Interactivity
{
    public class InteractionContextRequest<T>: IInteractionContextRequest where T: IContextRequestObject
    {
        #region Events
        public event EventHandler<ContextRequestEventArgs> Raised;
        #endregion


        #region Methods
        public void Raise(T context)
            => Raise(context, c => { });

        public void Raise(T context, Action<T> callback)
            => Raised?.Invoke(this, new ContextRequestEventArgs(context, () => callback?.Invoke(context)));

        public Task<T> RaiseAsync(T context)
            => CallbackHelper.AwaitCallbackResult<T>(callback => Raise(context, callback));
        #endregion
    }
}

// TODO: Test InteractionContextRequest (NotifyRequestProvider)
// TODO: Implement ConfirmRequestProvider???