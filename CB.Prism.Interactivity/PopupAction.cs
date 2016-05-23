using System.Windows.Forms;
using Prism.Interactivity;
using Prism.Interactivity.InteractionRequest;
using FileDialog = Microsoft.Win32.FileDialog;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;


namespace CB.Prism.Interactivity
{
    public interface IFileDialogInfo: IConfirmation
    {
        #region Abstract
        string FileName { get; set; }
        string[] FileNames { get; set; }
        #endregion


        // TODO: Add properties for SaveFileDialog
    }

    public interface ISaveFileDialogInfo: IFileDialogInfo { }

    public interface IOpenFileDialogInfo: IFileDialogInfo { }

    public interface IBrowseFolderDialogInfo: IConfirmation
    {
        #region Abstract
        string SelectedPath { get; set; }
        #endregion
    }

    public abstract class FileDialogInfo: Confirmation, IFileDialogInfo
    {
        #region  Properties & Indexers
        public string FileName { get; set; }
        public string[] FileNames { get; set; }
        #endregion
    }

    public class BrowseFolderDialogInfo: Confirmation, IBrowseFolderDialogInfo
    {
        #region  Properties & Indexers
        public string SelectedPath { get; set; }
        #endregion
    }

    public class SaveFileDialogInfo: FileDialogInfo, ISaveFileDialogInfo { }

    public class OpenFileDialogInfo: FileDialogInfo, IOpenFileDialogInfo { }

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

            // If the WindowContent shouldn't be part of another visual tree.
            if (WindowContent != null && WindowContent.Parent != null)
            {
                return;
            }

            var saveFileDialogInfo = args.Context as ISaveFileDialogInfo;
            if (saveFileDialogInfo != null)
            {
                var saveFileDialog = new SaveFileDialog();
                OpenFileDialog(saveFileDialogInfo, saveFileDialog);
                args.Callback?.Invoke();
                return;
            }

            var openFileDialogInfo = args.Context as IOpenFileDialogInfo;
            if (openFileDialogInfo != null)
            {
                var openFileDialog = new OpenFileDialog();
                OpenFileDialog(openFileDialogInfo, openFileDialog);
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
        private static void OpenFileDialog(IFileDialogInfo saveFileDialogInfo, FileDialog saveFileDialog)
        {
            saveFileDialogInfo.Confirmed = saveFileDialog.ShowDialog() == true;

            if (!saveFileDialogInfo.Confirmed) return;

            saveFileDialogInfo.FileName = saveFileDialog.FileName;
            saveFileDialogInfo.FileNames = saveFileDialog.FileNames;
        }

        private static void OpenFolderDialog(IBrowseFolderDialogInfo browseFolderDialogInfo,
            FolderBrowserDialog browseFolderDialog)
        {
            browseFolderDialogInfo.Confirmed = browseFolderDialog.ShowDialog() == DialogResult.OK;

            if (browseFolderDialogInfo.Confirmed)
            {
                browseFolderDialogInfo.SelectedPath = browseFolderDialog.SelectedPath;
            }
        }
        #endregion
    }

    public interface IReportProgress<out T>: INotification
    {
        #region Abstract
        T Progress { get; }
        #endregion
    }
}