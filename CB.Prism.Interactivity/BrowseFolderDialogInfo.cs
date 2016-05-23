using System;
using Prism.Interactivity.InteractionRequest;


namespace CB.Prism.Interactivity
{
    public class BrowseFolderDialogInfo: Confirmation, IBrowseFolderDialogInfo
    {
        #region  Constructors & Destructor
        public BrowseFolderDialogInfo()
        {
            Title = "Browse";
        }
        #endregion


        #region  Properties & Indexers
        public string Description { get; set; } = "";
        public Environment.SpecialFolder RootFolder { get; set; } = Environment.SpecialFolder.Desktop;
        public string SelectedPath { get; set; } = "";
        public bool ShowNewFolderButton { get; set; } = true;
        #endregion
    }
}