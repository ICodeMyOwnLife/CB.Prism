using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IList<Window> _windows = new List<Window>();
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

        public static readonly DependencyProperty WindowTypeProperty = DependencyProperty.Register(
            nameof(WindowType), typeof(Type), typeof(WindowTriggerAction), new PropertyMetadata(default(Type)));

        public Type WindowType
        {
            get { return (Type)GetValue(WindowTypeProperty); }
            set { SetValue(WindowTypeProperty, value); }
        }
        #endregion


        #region Override
        protected override void Invoke(object parameter)
        {
            var args = parameter as ContextRequestEventArgs;
            if (args == null)
            {
                return;
            }

            if (WindowType == null)
            {
                OpenDefaultWindow(args);
                return;
            }

            var window = InitializeWindow(args);

            if (IsModal)
            {
                window.ShowDialog();
            }
            else
            {
                window.Show();
            }
        }

        protected override void OnAttached()
        {
            var hostedWindow = AssociatedObject as Window ?? Window.GetWindow(AssociatedObject);
            if (hostedWindow != null)
            {
                hostedWindow.Closed += delegate
                {
                    foreach (var window in _windows.ToArray())
                    {
                        window.Close();
                    }
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

        private Window InitializeWindow(ContextRequestEventArgs args)
        {
            var window = (Window)Activator.CreateInstance(WindowType);
            window.DataContext = args.Context;
            window.Title = args.Context.Title;
            var callback = args.Callback;
            EventHandler handler = null;

            handler = delegate
            {
                _windows.Remove(window);
                window.Closed -= handler;

                var confirmationContext = args.Context as IConfirmContext;
                if (confirmationContext != null) confirmationContext.Confirmed = window.DialogResult == true;

                callback?.Invoke();
            };
            window.Closed += handler;
            _windows.Add(window);

            if (!CenterOverAssociatedObject || AssociatedObject == null) return window;

            SizeChangedEventHandler sizeHandler = null;
            sizeHandler = delegate
            {
                window.SizeChanged -= sizeHandler;

                var view = AssociatedObject;
                var position = view.PointToScreen(new Point(0, 0));

                window.Top = position.Y + (view.ActualHeight - window.ActualHeight) / 2;
                window.Left = position.X + (view.ActualWidth - window.ActualWidth) / 2;
            };
            window.SizeChanged += sizeHandler;
            return window;
        }

        private static void OpenDefaultWindow(ContextRequestEventArgs args)
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
//