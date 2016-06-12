namespace CB.Prism.Interactivity
{
    public enum WindowRequestAction
    {
        Close,
        Show,
        Hide,
        Minimize,
        Maximize,
        Restore,
        Activate,
        ShowDialog
    }

    /*public class GenericInteractionRequest<T> where T: IRequestContext
    {
        #region Events
        public event EventHandler<RequestEventArgs<T>> Raised;
        #endregion


        #region Methods
        public void Raise(T context, Action<T> callback = null)
            => OnRaised(new RequestEventArgs<T>(context, () => callback?.Invoke(context)));
        #endregion


        #region Implementation
        protected virtual void OnRaised(RequestEventArgs<T> e)
            => Raised?.Invoke(this, e);
        #endregion
    }*/

    /*public class ConfirmationInteractionRequest<T>: GenericInteractionRequest<T> where T: IConfirmation { }*/

    /*public class ConfirmationInteractionRequest<T>: IConfirmationRequest where T: IConfirmation
    {
        #region Events
        public event EventHandler<ConfirmationRequestEventArgs> Raised;
        #endregion


        #region Methods
        public void Raise(T context, Action<T> callback = null)
            => OnRaised(new ConfirmationRequestEventArgs(context, () => callback?.Invoke(context)));
        #endregion


        #region Implementation
        protected virtual void OnRaised(ConfirmationRequestEventArgs e)
            => Raised?.Invoke(this, e);
        #endregion
    }*/
}