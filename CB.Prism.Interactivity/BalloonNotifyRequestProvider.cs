using System;
using System.Drawing;


namespace CB.Prism.Interactivity
{
    public class BalloonNotifyRequestProvider: RequestProviderBase
    {
        #region Methods
        public virtual void Notify(string title, string message, Icon customIcon,
            bool largeIcon = false, string soundSource = null, bool loop = false,
            Action<ICustomIconBallonNotify> callback = null)
            => Request.Raise(
                new CustomIconBalloonNotification
                {
                    Title = title,
                    Content = message,
                    CustomIcon = customIcon,
                    LargeIcon = largeIcon,
                    SoundSource = soundSource,
                    Loop = loop
                }, callback);

        public virtual void Notify(string title, string message, BalloonIcon symbol = BalloonIcon.None,
            string soundSource = null, bool loop = false, Action<ISymbolBalloonNotify> callback = null)
            => Request.Raise(new SymbolBallonNotification
            {
                Title = title,
                Content = message,
                Symbol = symbol,
                SoundSource = soundSource,
                Loop = loop
            },
                callback);

        public virtual void NotifyError(string title, string message, string soundSource = null, bool loop = false,
            Action<ISymbolBalloonNotify> callback = null)
            => Notify(title, message, BalloonIcon.Error, soundSource, loop, callback);

        public virtual void NotifyErrorOnUiThread(string title, string message, string soundSource = null,
            bool loop = false, Action<ISymbolBalloonNotify> callback = null)
            => RunOnUiThread(() => NotifyError(title, message, soundSource, loop, callback));

        public virtual void NotifyInfo(string title, string message, string soundSource = null, bool loop = false,
            Action<ISymbolBalloonNotify> callback = null)
            => Notify(title, message, BalloonIcon.Info, soundSource, loop, callback);

        public virtual void NotifyInfoOnUiThread(string title, string message, string soundSource = null,
            bool loop = false, Action<ISymbolBalloonNotify> callback = null)
            => RunOnUiThread(() => NotifyInfo(title, message, soundSource, loop, callback));

        public virtual void NotifyOnUiThread(string title, string message, Icon customIcon, bool largeIcon = false,
            string soundSource = null, bool loop = false, Action<ICustomIconBallonNotify> callback = null)
            => RunOnUiThread(() => Notify(title, message, customIcon, largeIcon, soundSource, loop, callback));

        public virtual void NotifyOnUiThread(string title, string message, BalloonIcon symbol = BalloonIcon.None,
            string soundSource = null, bool loop = false, Action<ISymbolBalloonNotify> callback = null)
            => RunOnUiThread(() => Notify(title, message, symbol, soundSource, loop, callback));

        public virtual void NotifyWarning(string title, string message, string soundSource = null, bool loop = false,
            Action<ISymbolBalloonNotify> callback = null)
            => Notify(title, message, BalloonIcon.Warning, soundSource, loop, callback);

        public virtual void NotifyWarningOnUiThread(string title, string message, string soundSource = null,
            bool loop = false, Action<ISymbolBalloonNotify> callback = null)
            => RunOnUiThread(() => NotifyWarning(title, message, soundSource, loop, callback));
        #endregion
    }
}