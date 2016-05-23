namespace CB.Prism.Interactivity
{
    public interface ISaveFileDialogInfo: IFileDialogInfo
    {
        #region Abstract
        bool CreatePrompt { get; }
        bool OverwritePrompt { get; }
        #endregion
    }
}