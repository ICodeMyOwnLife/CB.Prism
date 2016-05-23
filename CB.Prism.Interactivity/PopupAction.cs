using System.Windows.Forms;
using Prism.Interactivity;
using Prism.Interactivity.InteractionRequest;
using FileDialog = Microsoft.Win32.FileDialog;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;


namespace CB.Prism.Interactivity
{
    public class PopupAction: PopupWindowAction
    {
        #region Override
        protected override void Invoke(object parameter)
        {
            var args = parameter as InteractionRequestedEventArgs;
            if (args == null)
            {
                return;
            }

            if (WindowContent?.Parent != null)
            {
                return;
            }

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
                return;
            }

            base.Invoke(parameter);
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