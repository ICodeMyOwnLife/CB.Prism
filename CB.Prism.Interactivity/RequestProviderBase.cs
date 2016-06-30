using System;
using System.Threading;


namespace CB.Prism.Interactivity
{
    /*public abstract class RequestProviderBase<T> where T: IContextRequestObject
    {
        #region Fields
        private InteractionContextRequest<T> _request;
        private readonly SynchronizationContext _synchronizationContext = SynchronizationContext.Current;
        #endregion


        #region  Properties & Indexers
        public virtual InteractionContextRequest<T> Request
            => _request ?? (_request = new InteractionContextRequest<T>());
        #endregion


        #region Implementation
        protected virtual void RunOnUiThread(Action action)
            => _synchronizationContext.Send(_ => action(), null);
        #endregion
    }*/

    public abstract class RequestProviderBase
    {
        private ICommonInteractionRequest _request;
        private readonly SynchronizationContext _synchronizationContext = SynchronizationContext.Current;

        public virtual ICommonInteractionRequest Request
            => _request ?? (_request = new CommonInteractionRequest());

        #region Implementation
        protected virtual void RunOnUiThread(Action action)
            => _synchronizationContext.Send(_ => action(), null);
        #endregion
    }
}