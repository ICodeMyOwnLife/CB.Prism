using System;


namespace CB.Prism.Interactivity
{
    public class WindowRequestEventArgs: EventArgs
    {
        #region  Constructors & Destructor
        public WindowRequestEventArgs() { }

        public WindowRequestEventArgs(WindowRequestAction requestAction)
        {
            RequestAction = requestAction;
        }
        #endregion


        #region  Properties & Indexers
        public WindowRequestAction RequestAction { get; set; }
        #endregion
    }
}