using System;
using System.Windows;
using CB.Xaml.Interactivity;


namespace CB.Prism.Interactivity
{
    public class ShowNotificationAction: TargetTriggerAction<FrameworkElement, IShowBalloonTip>
    {
        #region Override
        protected override void Invoke(object parameter)
        {
            var notifier = this.GetTarget();
            var args = parameter as ContextRequestEventArgs;
            if (notifier == null || args == null) return;

            var symbolBalloonNotify = args.Context as ISymbolBalloonNotify;
            if (symbolBalloonNotify != null)
            {
                HookCallback(args, notifier);
                notifier.ShowBalloonTip(symbolBalloonNotify.Title, symbolBalloonNotify.Content.ToString(),
                    symbolBalloonNotify.Symbol, symbolBalloonNotify.SoundSource, symbolBalloonNotify.Loop);
                return;
            }

            var customIconBalloonNotify = args.Context as ICustomIconBallonNotify;
            if (customIconBalloonNotify != null)
            {
                HookCallback(args, notifier);
                notifier.ShowBalloonTip(customIconBalloonNotify.Title, customIconBalloonNotify.Content.ToString(),
                    customIconBalloonNotify.CustomIcon, customIconBalloonNotify.LargeIcon,
                    customIconBalloonNotify.SoundSource, customIconBalloonNotify.Loop);
            }
        }
        #endregion


        #region Implementation
        private static void HookCallback(ContextRequestEventArgs args, IShowBalloonTip notifier)
        {
            EventHandler handler = null;

            handler = (sender, eventArgs) =>
            {
                args.Callback?.Invoke();
                notifier.BalloonTipClosed -= handler;
            };
            notifier.BalloonTipClosed += handler;
        }
        #endregion
    }
}