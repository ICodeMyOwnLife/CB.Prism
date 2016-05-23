using Prism.Interactivity.InteractionRequest;


namespace CB.Prism.Interactivity
{
    public interface IReportProgress<out T>: INotification
    {
        #region Abstract
        T Progress { get; }
        #endregion
    }
}