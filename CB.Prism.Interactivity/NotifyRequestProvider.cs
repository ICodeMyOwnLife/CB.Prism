using System;


namespace CB.Prism.Interactivity
{
    public class NotifyRequestProvider: RequestProviderBase
    {
        #region Fields
        public static string CommonErrorTitle = "Error";
        public static string CommonInfoTitle = "Info";
        public static string CommonWarningTitle = "Warning";
        #endregion


        #region  Properties & Indexers
        public virtual string ErrorTitle { get; set; }
        public virtual string InfoTitle { get; set; }
        public virtual string WarningTitle { get; set; }
        #endregion


        #region Methods
        public virtual void Notify(string title, object content, Action<INotifyContext> callback = null)
            => Request.Raise(new NotifyContext { Title = title, Content = content }, callback);

        public virtual void NotifyError(object content, Action<INotifyContext> callback = null)
            => Notify(ErrorTitle ?? CommonErrorTitle, content, callback);

        public virtual void NotifyErrorOnUiThread(object content, Action<INotifyContext> callback = null)
            => RunOnUiThread(() => NotifyError(content, callback));

        public virtual void NotifyInfo(object content, Action<INotifyContext> callback = null)
            => Notify(InfoTitle ?? CommonInfoTitle, content, callback);

        public virtual void NotifyInfoOnUiThread(object content, Action<INotifyContext> callback)
            => RunOnUiThread(() => NotifyInfo(content, callback));

        public virtual void NotifyOnUiThread(string title, object content, Action<INotifyContext> callback = null)
            => RunOnUiThread(() => Notify(title, content, callback));

        public virtual void NotifyWarning(object content, Action<INotifyContext> callback = null)
            => Notify(WarningTitle ?? CommonWarningTitle, content, callback);

        public virtual void NotifyWarningOnUiThread(object content, Action<INotifyContext> callback = null)
            => RunOnUiThread(() => NotifyWarning(content, callback));
        #endregion
    }
}