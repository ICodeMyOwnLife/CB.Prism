using System;
using Prism.Interactivity.InteractionRequest;


namespace CB.Prism.Interactivity
{
    public interface IBrowseFolderDialogInfo: IConfirmContext
    {
        #region Abstract
        string Description { get; }
        Environment.SpecialFolder RootFolder { get; }
        string SelectedPath { get; set; }
        bool ShowNewFolderButton { get; }
        #endregion
    }
}