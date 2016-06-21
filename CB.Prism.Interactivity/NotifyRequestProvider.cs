using System;
using Prism.Interactivity.InteractionRequest;


namespace CB.Prism.Interactivity
{
    public class NotifyRequestProvider: RequestProviderBase<INotifyContext>
    {
        #region Fields
        public static string CommonErrorTitle = "Error";
        public static string CommonWarningTitle = "Warning";
        #endregion


        #region  Properties & Indexers
        public virtual string ErrorTitle { get; set; }

        public override InteractionContextRequest<INotifyContext> Request { get; } =
            new InteractionContextRequest<INotifyContext>();

        public virtual string WarningTitle { get; set; }
        #endregion


        #region Methods
        public virtual void Notify(string title, object content, Action<INotifyContext> callback = null)
            => Request.Raise(new NotifyContext { Title = title, Content = content }, callback);

        public virtual void NotifyError(string content, Action<INotifyContext> callback = null)
            => Notify(ErrorTitle ?? CommonErrorTitle, content, callback);

        public virtual void NotifyErrorOnUiThread(string content, Action<INotifyContext> callback = null)
            => RunOnUiThread(() => NotifyError(content, callback));

        public virtual void NotifyOnUiThread(string title, object content, Action<INotifyContext> callback = null)
            => RunOnUiThread(() => Notify(title, content, callback));

        public virtual void NotifyWarning(string content, Action<INotifyContext> callback = null)
            => Notify(WarningTitle ?? CommonWarningTitle, content, callback);

        public virtual void NotifyWarningOnUiThread(string content, Action<INotifyContext> callback = null)
            => RunOnUiThread(() => NotifyWarning(content, callback));
        #endregion
    }
}