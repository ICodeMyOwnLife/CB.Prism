using System;
using Prism.Interactivity.InteractionRequest;


namespace CB.Prism.Interactivity
{
    public class ConfirmationInteractionRequest<T>: IConfirmationRequest where T: IConfirmation
    {
        #region Events
        public event EventHandler<ConfirmationRequestEventArgs> Raised;
        #endregion


        #region Methods
        public void Raise(T context, Action<T> callback = null)
            => OnTriggered(new ConfirmationRequestEventArgs(context, () => callback?.Invoke(context)));
        #endregion


        #region Implementation
        protected virtual void OnTriggered(ConfirmationRequestEventArgs e)
            => Raised?.Invoke(this, e);
        #endregion
    }
}