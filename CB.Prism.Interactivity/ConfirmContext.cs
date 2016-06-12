namespace CB.Prism.Interactivity
{
    public class ConfirmContext: RequestContext, IConfirmContext
    {
        #region  Properties & Indexers
        public bool Confirmed { get; set; }
        #endregion
    }
}