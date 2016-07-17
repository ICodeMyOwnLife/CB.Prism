using System.Windows;
using System.Windows.Interactivity;


namespace CB.Prism.Interactivity
{
    public class RoutedEventTrigger: EventTriggerBase<UIElement>
    {
        #region Fields
        private readonly RoutedEventHandler _handler;
        #endregion


        #region  Constructors & Destructor
        public RoutedEventTrigger()
        {
            _handler = (sender, args) => OnEvent(args);
        }
        #endregion


        #region  Properties & Indexers
        public string EventName { get; set; }
        public RoutedEvent RoutedEvent { get; set; }
        #endregion


        #region Override
        protected override string GetEventName()
        {
            return EventName;
        }

        protected override void OnAttached()
        {
            Source.AddHandler(RoutedEvent, _handler);
        }

        protected override void OnDetaching()
        {
            Source.RemoveHandler(RoutedEvent, _handler);
        }
        #endregion
    }
}