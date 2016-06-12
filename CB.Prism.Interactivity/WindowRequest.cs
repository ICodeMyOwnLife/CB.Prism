using System;


namespace CB.Prism.Interactivity
{
    public class WindowRequest
    {
        #region Events
        public event EventHandler<WindowRequestEventArgs> Raised;
        #endregion


        #region Methods
        public void Raise(WindowRequestAction requestAction) => OnRaised(new WindowRequestEventArgs(requestAction));
        #endregion


        #region Implementation
        protected virtual void OnRaised(WindowRequestEventArgs e)
        {
            Raised?.Invoke(this, e);
        }
        #endregion
    }
}