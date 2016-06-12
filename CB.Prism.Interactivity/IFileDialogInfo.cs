using System.Collections.Generic;
using Microsoft.Win32;
using Prism.Interactivity.InteractionRequest;


namespace CB.Prism.Interactivity
{
    public interface IFileDialogInfo: IConfirmContext
    {
        #region Abstract
        bool AddExtension { get; }
        bool CheckFileExists { get; }
        bool CheckPathExists { get; }
        IList<FileDialogCustomPlace> CustomPlaces { get; }
        string DefaultExt { get; }
        bool DerefenenceLinks { get; }
        string FileName { get; set; }
        string[] FileNames { get; set; }
        string Filter { get; }
        int FilterIndex { get; }
        string InitialDirectory { get; }
        bool RestoreDirectory { get; }
        bool ValidateNames { get; }
        #endregion
    }
}