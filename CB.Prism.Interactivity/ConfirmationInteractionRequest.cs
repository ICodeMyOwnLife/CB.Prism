using System;
using System.Windows;
using EventTrigger = System.Windows.Interactivity.EventTrigger;


namespace CB.Prism.Interactivity
{
    public class WindowRequestTrigger: EventTrigger
    {
        #region Override
        protected override string GetEventName()
        {
            return "Raised";
        }

        protected override void OnEvent(EventArgs eventArgs)
        {
            var args = eventArgs as WindowRequestEventArgs;
            if (args == null) return;

            var window = GetAssociatedWindow();
            switch (args.RequestAction)
            {
                case WindowRequestAction.Close:
                    window.Close();
                    break;
                case WindowRequestAction.Show:
                    window.Show();
                    break;
                case WindowRequestAction.Hide:
                    window.Hide();
                    break;
                case WindowRequestAction.Minimize:
                    window.WindowState = WindowState.Minimized;
                    break;
                case WindowRequestAction.Maximize:
                    window.WindowState = WindowState.Maximized;
                    break;
                case WindowRequestAction.Restore:
                    window.WindowState = WindowState.Normal;
                    break;
                case WindowRequestAction.Activate:
                    window.Activate();
                    break;
                case WindowRequestAction.ShowDialog:
                    window.ShowDialog();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        #endregion


        #region Implementation
        private Window GetAssociatedWindow() => AssociatedObject as Window ?? Window.GetWindow(AssociatedObject);
        #endregion
    }

    public class WindowRequest
    {
        #region Events
        public event EventHandler<WindowRequestEventArgs> Raised;
        #endregion


        #region Methods
        public void Raise(WindowRequestAction requestAction) => OnRaised(new WindowRequestEventArgs(requestAction));
        #endregion


        #region Implementation
        protected virtual void OnRaised(WindowRequestEventArgs e)
        {
            Raised?.Invoke(this, e);
        }
        #endregion
    }

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

    public enum WindowRequestAction
    {
        Close,
        Show,
        Hide,
        Minimize,
        Maximize,
        Restore,
        Activate,
        ShowDialog
    }

    public class CommonInteractionRequest
    {
        #region Events
        public event EventHandler<RequestContextEventArgs> Raised;
        #endregion


        #region Methods
        public void Raise(IRequestContext context, Action<IRequestContext> callback = null)
            => OnRaised(new RequestContextEventArgs(context, () => callback?.Invoke(context)));
        #endregion


        #region Implementation
        protected virtual void OnRaised(RequestContextEventArgs e) => Raised?.Invoke(this, e);
        #endregion
    }

    /*public class GenericInteractionRequest<T> where T: IRequestContext
    {
        #region Events
        public event EventHandler<RequestEventArgs<T>> Raised;
        #endregion


        #region Methods
        public void Raise(T context, Action<T> callback = null)
            => OnRaised(new RequestEventArgs<T>(context, () => callback?.Invoke(context)));
        #endregion


        #region Implementation
        protected virtual void OnRaised(RequestEventArgs<T> e)
            => Raised?.Invoke(this, e);
        #endregion
    }*/

    /*public class ConfirmationInteractionRequest<T>: GenericInteractionRequest<T> where T: IConfirmation { }*/

    /*public class ConfirmationInteractionRequest<T>: IConfirmationRequest where T: IConfirmation
    {
        #region Events
        public event EventHandler<ConfirmationRequestEventArgs> Raised;
        #endregion


        #region Methods
        public void Raise(T context, Action<T> callback = null)
            => OnRaised(new ConfirmationRequestEventArgs(context, () => callback?.Invoke(context)));
        #endregion


        #region Implementation
        protected virtual void OnRaised(ConfirmationRequestEventArgs e)
            => Raised?.Invoke(this, e);
        #endregion
    }*/
}