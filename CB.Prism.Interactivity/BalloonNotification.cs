namespace CB.Prism.Interactivity
{
    public class BalloonNotification: NotifyContext, IBalloonNotify

    {
        #region  Properties & Indexers
        public bool Loop { get; set; }
        public string SoundSource { get; set; }
        #endregion
    }
}