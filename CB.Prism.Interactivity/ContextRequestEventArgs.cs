using System;


namespace CB.Prism.Interactivity
{
    public class ContextRequestEventArgs: EventArgs
    {
        #region  Constructors & Destructor
        public ContextRequestEventArgs() { }

        public ContextRequestEventArgs(IContextRequestObject context, Action callback = null)
        {
            Context = context;
            Callback = callback;
        }
        #endregion


        #region  Properties & Indexers
        public Action Callback { get; set; }
        public IContextRequestObject Context { get; set; }
        #endregion
    }
}