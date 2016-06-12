namespace CB.Prism.Interactivity
{
    public interface INotifyContext: IContextRequestObject
    {
        #region Abstract
        object Content { get; set; }
        #endregion
    }
}