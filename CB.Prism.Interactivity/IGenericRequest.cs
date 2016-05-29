using System;


namespace CB.Prism.Interactivity
{
    public interface IGenericRequest
    {
        #region Abstract
        event EventHandler<ConfirmationRequestEventArgs> Raised;
        #endregion
    }
}