using System.Drawing;


namespace CB.Prism.Interactivity
{
    public interface ICustomIconBallonNotify: IBalloonNotify
    {
        #region Abstract
        Icon CustomIcon { get; set; }
        bool LargeIcon { get; set; }
        #endregion
    }
}