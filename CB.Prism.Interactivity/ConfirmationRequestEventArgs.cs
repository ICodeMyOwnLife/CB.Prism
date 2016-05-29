using System;
using Prism.Interactivity.InteractionRequest;


namespace CB.Prism.Interactivity
{
    public class ConfirmationRequestEventArgs: EventArgs
    {
        #region  Constructors & Destructor
        public ConfirmationRequestEventArgs() { }

        public ConfirmationRequestEventArgs(IConfirmation context, Action callback = null)
        {
            Context = context;
            Callback = callback;
        }
        #endregion


        #region  Properties & Indexers
        public Action Callback { get; set; }
        public IConfirmation Context { get; set; }
        #endregion
    }
}