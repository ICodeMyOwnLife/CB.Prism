namespace CB.Prism.Interactivity
{
    public class NotifyContext: RequestContext, INotifyContext
    {
        #region  Properties & Indexers
        public object Content { get; set; }
        #endregion
    }
}