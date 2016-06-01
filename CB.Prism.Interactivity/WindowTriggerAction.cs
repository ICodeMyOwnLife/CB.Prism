using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interactivity;
using FileDialog = Microsoft.Win32.FileDialog;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;


namespace CB.Prism.Interactivity
{
    public class WindowTriggerAction: TriggerAction<FrameworkElement>
    {
        #region Fields
        private static readonly IList<Window> _closedWindows = new List<Window>();
        #endregion


        #region Dependency Properties
        public static readonly DependencyProperty CenterOverAssociatedObjectProperty =
            DependencyProperty.Register("CenterOverAssociatedObject", typeof(bool), typeof(WindowTriggerAction),
                new PropertyMetadata(null));

        public bool CenterOverAssociatedObject
        {
            get { return (bool)GetValue(CenterOverAssociatedObjectProperty); }
            set { SetValue(CenterOverAssociatedObjectProperty, value); }
        }

        public static readonly DependencyProperty IsModalProperty = DependencyProperty.Register("IsModal", typeof(bool),
            typeof(WindowTriggerAction), new PropertyMetadata(null));

        public bool IsModal
        {
            get { return (bool)GetValue(IsModalProperty); }
            set { SetValue(IsModalProperty, value); }
        }

        public static readonly DependencyProperty WindowContentProperty =
            DependencyProperty.Register("Window", typeof(Window), typeof(WindowTriggerAction),
                new PropertyMetadata(null));

        public Window Window
        {
            get { return (Window)GetValue(WindowContentProperty); }
            set { SetValue(WindowContentProperty, value); }
        }
        #endregion


        #region Override
        protected override void Invoke(object parameter)
        {
            var args = parameter as ConfirmationRequestEventArgs;
            if (args == null)
            {
                return;
            }

            if (Window == null)
            {
                OpenDefaultWindow(args);
                return;
            }

            if (_closedWindows.Contains(Window))
            {
                _closedWindows.Remove(Window);
                Window = (Window)Activator.CreateInstance(Window.GetType());
            }

            Window.DataContext = args.Context;
            var callback = args.Callback;
            EventHandler handler = null;
            handler =
                (o, e) =>
                {
                    _closedWindows.Add(Window);
                    Window.Closed -= handler;
                    args.Context.Confirmed = Window.DialogResult == true;
                    callback?.Invoke();
                };
            Window.Closed += handler;

            if (CenterOverAssociatedObject && AssociatedObject != null)
            {
                SizeChangedEventHandler sizeHandler = null;
                sizeHandler =
                    (o, e) =>
                    {
                        Window.SizeChanged -= sizeHandler;

                        var view = AssociatedObject;
                        var position = view.PointToScreen(new Point(0, 0));

                        Window.Top = position.Y + ((view.ActualHeight - Window.ActualHeight) / 2);
                        Window.Left = position.X + ((view.ActualWidth - Window.ActualWidth) / 2);
                    };
                Window.SizeChanged += sizeHandler;
            }

            if (IsModal)
            {
                Window.ShowDialog();
            }
            else
            {
                Window.Show();
            }
        }

        protected override void OnAttached()
        {
            var hostedWindow = AssociatedObject as Window ?? Window.GetWindow(AssociatedObject);
            if (hostedWindow != null)
            {
                hostedWindow.Closed += delegate
                {
                    Window?.Close();
                };
            }
        }
        #endregion


        #region Implementation
        private static void HandleFileDialog(IFileDialogInfo fileDialogInfo, FileDialog fileDialog)
        {
            fileDialog.AddExtension = fileDialogInfo.AddExtension;
            fileDialog.CheckFileExists = fileDialogInfo.CheckFileExists;
            fileDialog.CheckPathExists = fileDialogInfo.CheckPathExists;
            fileDialog.CustomPlaces = fileDialogInfo.CustomPlaces;
            fileDialog.DefaultExt = fileDialogInfo.DefaultExt;
            fileDialog.DereferenceLinks = fileDialogInfo.DerefenenceLinks;
            fileDialog.Filter = fileDialogInfo.Filter;
            fileDialog.FilterIndex = fileDialogInfo.FilterIndex;
            fileDialog.InitialDirectory = fileDialogInfo.InitialDirectory;
            fileDialog.Title = fileDialogInfo.Title;
            fileDialog.ValidateNames = fileDialogInfo.ValidateNames;

            fileDialogInfo.Confirmed = fileDialog.ShowDialog() == true;

            if (!fileDialogInfo.Confirmed) return;

            fileDialogInfo.FileName = fileDialog.FileName;
            fileDialogInfo.FileNames = fileDialog.FileNames;
        }

        private static void OpenDefaultWindow(ConfirmationRequestEventArgs args)
        {
            var saveFileDialogInfo = args.Context as ISaveFileDialogInfo;
            if (saveFileDialogInfo != null)
            {
                var saveFileDialog = new SaveFileDialog
                {
                    CreatePrompt = saveFileDialogInfo.CreatePrompt,
                    OverwritePrompt = saveFileDialogInfo.OverwritePrompt
                };

                HandleFileDialog(saveFileDialogInfo, saveFileDialog);
                args.Callback?.Invoke();
                return;
            }

            var openFileDialogInfo = args.Context as IOpenFileDialogInfo;
            if (openFileDialogInfo != null)
            {
                var openFileDialog = new OpenFileDialog
                {
                    Multiselect = openFileDialogInfo.MultiSelect,
                    ReadOnlyChecked = openFileDialogInfo.ReadOnlyChecked,
                    ShowReadOnly = openFileDialogInfo.ShowReadOnly
                };

                HandleFileDialog(openFileDialogInfo, openFileDialog);
                args.Callback?.Invoke();
                return;
            }

            var browseFolderDialogInfo = args.Context as IBrowseFolderDialogInfo;
            if (browseFolderDialogInfo != null)
            {
                var browseFolderDialog = new FolderBrowserDialog();
                OpenFolderDialog(browseFolderDialogInfo, browseFolderDialog);
                args.Callback?.Invoke();
            }
        }

        private static void OpenFolderDialog(IBrowseFolderDialogInfo browseFolderDialogInfo,
            FolderBrowserDialog browseFolderDialog)
        {
            browseFolderDialog.Description = browseFolderDialogInfo.Description;
            browseFolderDialog.RootFolder = browseFolderDialogInfo.RootFolder;
            browseFolderDialog.ShowNewFolderButton = browseFolderDialogInfo.ShowNewFolderButton;

            browseFolderDialogInfo.Confirmed = browseFolderDialog.ShowDialog() == DialogResult.OK;

            if (browseFolderDialogInfo.Confirmed)
            {
                browseFolderDialogInfo.SelectedPath = browseFolderDialog.SelectedPath;
            }
        }
        #endregion
    }
}


// TODO: Window is disposed: how to create new Window each time we call - not done - don't know how to create new window which is defined in Xaml