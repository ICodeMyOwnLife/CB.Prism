using System;
using System.Windows;
using System.Windows.Interop;
using CB.Xaml.AttachedProperties;
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
                    WindowServices.SetCloseToHide(window, false);
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
                case WindowRequestAction.OK:
                    if (ComponentDispatcher.IsThreadModal) window.DialogResult = true;
                    window.Close();
                    break;
                case WindowRequestAction.Cancel:
                    if (ComponentDispatcher.IsThreadModal) window.DialogResult = false;
                    window.Close();
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
}