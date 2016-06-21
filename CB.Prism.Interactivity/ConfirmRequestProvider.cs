using System;


namespace CB.Prism.Interactivity
{
    public class ConfirmRequestProvider: RequestProviderBase<IConfirmContext>
    {
        #region Fields
        public static string CommonConfirmTitle = "Confirm";
        #endregion


        #region  Properties & Indexers
        public string ConfirmTitle { get; set; }

        public override InteractionContextRequest<IConfirmContext> Request { get; } =
            new InteractionContextRequest<IConfirmContext>();
        #endregion


        #region Methods
        public virtual void Confirm(string title, object content, Action<IConfirmContext> callback)
            => Request.Raise(new ConfirmContext { Title = title, Content = content }, callback);

        public virtual void Confirm(object content, Action<IConfirmContext> callback)
            => Confirm(ConfirmTitle ?? CommonConfirmTitle, content, callback);

        public virtual void Confirm(string title, object content, Action confirmedCallback)
            => Confirm(title, content, context => { if (context.Confirmed) confirmedCallback(); });

        public virtual void Confirm(object content, Action confirmedCallback)
            => Confirm(content, context => { if (context.Confirmed) confirmedCallback(); });

        public virtual void ConfirmOnUiThread(string title, object content, Action<IConfirmContext> callback)
            => RunOnUiThread(() => Confirm(title, content, callback));

        public virtual void ConfirmOnUiThread(string title, object content, Action confirmedCallback)
            => RunOnUiThread(() => Confirm(title, content, confirmedCallback));

        public virtual void ConfirmOnUiThread(object content, Action confirmedCallback)
            => RunOnUiThread(() => Confirm(content, confirmedCallback));

        public virtual void ConfirmOnUiThread(object content, Action<IConfirmContext> callback)
            => RunOnUiThread(() => Confirm(content, callback));
        #endregion
    }
}