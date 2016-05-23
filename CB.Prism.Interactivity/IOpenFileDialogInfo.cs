namespace CB.Prism.Interactivity
{
    public interface IOpenFileDialogInfo: IFileDialogInfo
    {
        #region Abstract
        bool MultiSelect { get; }
        bool ReadOnlyChecked { get; }
        bool ShowReadOnly { get; }
        #endregion
    }
}