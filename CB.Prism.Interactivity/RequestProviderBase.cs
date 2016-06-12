using System;
using System.Threading;


namespace CB.Prism.Interactivity
{
    public abstract class RequestProviderBase<T> where T: IContextRequestObject
    {
        #region Fields
        private readonly SynchronizationContext _synchronizationContext = SynchronizationContext.Current;
        #endregion


        #region Abstract
        public abstract InteractionContextRequest<T> Request { get; }
        #endregion


        #region Implementation
        protected virtual void RunOnUiThread(Action action)
            => _synchronizationContext.Send(_ => action(), null);
        #endregion
    }
}