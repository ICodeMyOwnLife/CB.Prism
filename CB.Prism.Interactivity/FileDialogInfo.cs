using System.Collections.Generic;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;


namespace CB.Prism.Interactivity
{
    public abstract class FileDialogInfo: ConfirmContext, IFileDialogInfo
    {
        #region  Properties & Indexers
        public bool AddExtension { get; set; } = true;
        public bool CheckFileExists { get; set; } = false;
        public bool CheckPathExists { get; set; } = true;
        public IList<FileDialogCustomPlace> CustomPlaces { get; set; }
        public string DefaultExt { get; set; } = "";
        public bool DerefenenceLinks { get; set; } = false;
        public string FileName { get; set; } = "";
        public string[] FileNames { get; set; } = new string[0];
        public string Filter { get; set; } = "";
        public int FilterIndex { get; set; } = 1;
        public string InitialDirectory { get; set; } = "";
        public virtual bool RestoreDirectory { get; set; } = false;
        public bool ValidateNames { get; set; } = false;
        #endregion
    }
}


// TODO: Set default values