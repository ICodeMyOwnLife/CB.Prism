using System;


namespace CB.Prism.Interactivity
{
    public interface ICommonInteractionRequest: IInteractionContextRequest
    {
        #region Abstract
        void Raise(IContextRequestObject context, Action<IContextRequestObject> callback = null);

        void Raise<TRequest>(TRequest requestObject, Action<TRequest> callback = null)
            where TRequest: IContextRequestObject;
        #endregion
    }
}