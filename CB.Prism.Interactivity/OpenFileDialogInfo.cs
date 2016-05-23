using System;


namespace CB.Prism.Interactivity
{
    public class OpenFileDialogInfo: FileDialogInfo, IOpenFileDialogInfo
    {
        #region  Constructors & Destructor
        public OpenFileDialogInfo()
        {
            CheckFileExists = true;
            Title = "Open";
        }
        #endregion


        #region  Properties & Indexers
        public bool MultiSelect { get; set; } = false;
        public bool ReadOnlyChecked { get; set; } = false;

        public override bool RestoreDirectory
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public bool ShowReadOnly { get; set; } = false;
        #endregion
    }
}