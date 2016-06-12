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
        #endregion


        #region Implementation
        protected virtual void OnRaised(ContextRequestEventArgs e) => Raised?.Invoke(this, e);
        #endregion
    }
}