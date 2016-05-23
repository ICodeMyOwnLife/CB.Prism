using System;


namespace CB.Prism.Interactivity
{
    public class SaveFileDialogInfo: FileDialogInfo, ISaveFileDialogInfo
    {
        #region  Constructors & Destructor
        public SaveFileDialogInfo()
        {
            Title = "Save";
        }
        #endregion


        #region  Properties & Indexers
        public bool CreatePrompt { get; set; } = false;
        public bool OverwritePrompt { get; set; } = true;

        public override bool RestoreDirectory
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
        #endregion
    }
}