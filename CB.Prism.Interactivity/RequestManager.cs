namespace CB.Prism.Interactivity
{
    public class RequestManager
    {
        #region Fields
        private ConfirmRequestProvider _confirmRequestProvider;
        private FileRequestProvider _fileRequestProvider;
        private NotifyRequestProvider _notifyRequestProvider;
        private WindowRequestProvider _windowRequestProvider;
        #endregion


        #region  Properties & Indexers
        public ConfirmRequestProvider ConfirmRequestProvider
            => _confirmRequestProvider ?? (_confirmRequestProvider = new ConfirmRequestProvider());

        public FileRequestProvider FileRequestProvider => _fileRequestProvider ?? (_fileRequestProvider = new FileRequestProvider());

        public NotifyRequestProvider NotifyRequestProvider
            => _notifyRequestProvider ?? (_notifyRequestProvider = new NotifyRequestProvider());

        public WindowRequestProvider WindowRequestProvider
            => _windowRequestProvider ?? (_windowRequestProvider = new WindowRequestProvider());
        #endregion
    }
}