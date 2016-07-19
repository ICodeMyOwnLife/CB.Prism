namespace CB.Prism.Interactivity
{
    public interface IBalloonNotify: INotifyContext
    {
        #region Abstract
        string SoundSource { get; set; }
        bool Loop { get; set; }
        #endregion
    }
}