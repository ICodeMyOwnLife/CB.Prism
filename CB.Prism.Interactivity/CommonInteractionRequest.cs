using System;


namespace CB.Prism.Interactivity
{
    public class CommonInteractionRequest: IInteractionContextRequest
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
        #endregion


        #region Implementation
        protected virtual void OnRaised(ContextRequestEventArgs e) => Raised?.Invoke(this, e);
        #endregion
    }
}