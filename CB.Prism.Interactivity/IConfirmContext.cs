namespace CB.Prism.Interactivity
{
    public interface IConfirmContext: IRequestContext
    {
        #region Abstract
        bool Confirmed { get; set; }
        #endregion
    }
}