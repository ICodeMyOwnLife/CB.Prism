namespace CB.Prism.Interactivity
{
    public interface IConfirmContext: IContextRequestObject
    {
        #region Abstract
        bool Confirmed { get; set; }
        #endregion
    }
}