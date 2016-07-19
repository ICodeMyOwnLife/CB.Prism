namespace CB.Prism.Interactivity
{
    public interface ISymbolBalloonNotify: IBalloonNotify
    {
        #region Abstract
        BalloonIcon Symbol { get; set; }
        #endregion
    }
}