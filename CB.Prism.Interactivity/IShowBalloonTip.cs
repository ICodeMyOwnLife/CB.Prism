using System.Drawing;


namespace CB.Prism.Interactivity
{
    public interface IShowBalloonTip
    {
        #region Abstract
        void ShowBalloonTip(string title, string message, BalloonIcon symbol = BalloonIcon.None, string soundSource = null, bool loop = false);
        void ShowBalloonTip(string title, string message, Icon customIcon = null, bool largeIcon = false, string soundSource = null, bool loop = false);
        #endregion
    }
}