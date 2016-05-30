using System;


namespace CB.Prism.Interactivity
{
    public interface IConfirmationRequest
    {
        #region Abstract
        event EventHandler<ConfirmationRequestEventArgs> Raised;
        #endregion
    }
}