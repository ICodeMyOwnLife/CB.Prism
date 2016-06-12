using System;


namespace CB.Prism.Interactivity
{
    public interface IInteractionContextRequest
    {
        #region Abstract
        event EventHandler<ContextRequestEventArgs> Raised;
        #endregion
    }
}