using System;


namespace CB.Prism.Interactivity
{
    public class RequestContextEventArgs: EventArgs
    {
        #region  Constructors & Destructor
        public RequestContextEventArgs() { }

        public RequestContextEventArgs(IRequestContext context, Action callback = null)
        {
            Context = context;
            Callback = callback;
        }
        #endregion


        #region  Properties & Indexers
        public Action Callback { get; set; }
        public IRequestContext Context { get; set; }
        #endregion
    }

    /*public class RequestEventArgs<TContext>: EventArgs where TContext: IRequestContext
    {
        #region  Constructors & Destructor
        public RequestEventArgs() { }

        public RequestEventArgs(TContext context, Action callback = null)
        {
            Context = context;
            Callback = callback;
        }
        #endregion


        #region  Properties & Indexers
        public Action Callback { get; set; }
        public TContext Context { get; set; }
        #endregion
    }*/

    /*public class ConfirmationRequestEventArgs: RequestEventArgs<IConfirmation>
    {
        #region  Constructors & Destructor
        public ConfirmationRequestEventArgs() { }

        public ConfirmationRequestEventArgs(IConfirmation context, Action callback = null): base(Context, callback) { }
        #endregion


        /*#region  Constructors & Destructor
        public ConfirmationRequestEventArgs() { }

        public ConfirmationRequestEventArgs(IConfirmation context, Action callback = null)
        {
            Context = Context;
            Callback = callback; 
        }
        #endregion


        #region  Properties & Indexers
        public Action Callback { get; set; }
        public IConfirmation Context { get; set; }
        #endregion#1#
    }*/
}