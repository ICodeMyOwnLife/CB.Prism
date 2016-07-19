namespace CB.Prism.Interactivity
{
    public class SymbolBallonNotification: BalloonNotification, ISymbolBalloonNotify
    {
        #region  Properties & Indexers
        public BalloonIcon Symbol { get; set; }
        #endregion
    }
}