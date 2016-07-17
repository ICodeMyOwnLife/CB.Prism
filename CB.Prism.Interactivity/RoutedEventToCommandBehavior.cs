using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;


namespace CB.Prism.Interactivity
{
    public class RoutedEventToCommandBehavior: Behavior<UIElement>
    {
        #region Fields
        private readonly RoutedEventHandler _handler;
        #endregion


        #region  Constructors & Destructor
        public RoutedEventToCommandBehavior()
        {
            _handler = (sender, args) => GetCommand(sender, args)?.Execute(GetCommandParameter(args));
        }
        #endregion


        #region Dependency Properties
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(nameof(Command),
            typeof(ICommand), typeof(RoutedEventToCommandBehavior), new PropertyMetadata(default(ICommand)));

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandPathProperty = DependencyProperty.Register(
            nameof(CommandPath), typeof(string), typeof(RoutedEventToCommandBehavior),
            new PropertyMetadata(default(string)));

        public string CommandPath
        {
            get { return (string)GetValue(CommandPathProperty); }
            set { SetValue(CommandPathProperty, value); }
        }

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register(nameof(CommandParameter), typeof(object), typeof(RoutedEventToCommandBehavior),
                new PropertyMetadata(default(object)));

        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public static readonly DependencyProperty CommandParameterOptionProperty =
            DependencyProperty.Register(nameof(CommandParameterOption), typeof(CommandParameterOption),
                typeof(RoutedEventToCommandBehavior), new PropertyMetadata(default(CommandParameterOption)));

        public CommandParameterOption CommandParameterOption
        {
            get { return (CommandParameterOption)GetValue(CommandParameterOptionProperty); }
            set { SetValue(CommandParameterOptionProperty, value); }
        }

        public static readonly DependencyProperty FindCommandInProperty = DependencyProperty.Register(
            nameof(FindCommandIn), typeof(FindCommandOption), typeof(RoutedEventToCommandBehavior),
            new PropertyMetadata(FindCommandOption.AssociatedObject));

        public FindCommandOption FindCommandIn
        {
            get { return (FindCommandOption)GetValue(FindCommandInProperty); }
            set { SetValue(FindCommandInProperty, value); }
        }

        public static readonly DependencyProperty RoutedEventProperty = DependencyProperty.Register(
            nameof(RoutedEvent), typeof(RoutedEvent), typeof(RoutedEventToCommandBehavior),
            new PropertyMetadata(default(RoutedEvent)));

        public RoutedEvent RoutedEvent
        {
            get { return (RoutedEvent)GetValue(RoutedEventProperty); }
            set { SetValue(RoutedEventProperty, value); }
        }

        public static readonly DependencyProperty TriggerParameterPathProperty = DependencyProperty.Register(
            nameof(TriggerParameterPath), typeof(string), typeof(RoutedEventToCommandBehavior),
            new PropertyMetadata(default(string)));

        public string TriggerParameterPath
        {
            get { return (string)GetValue(TriggerParameterPathProperty); }
            set { SetValue(TriggerParameterPathProperty, value); }
        }
        #endregion


        #region Override
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.AddHandler(RoutedEvent, _handler);
        }
        #endregion


        #region Implementation
        private ICommand GetCommand(object sender, EventArgs args)
        {
            if (Command != null) return Command;
            if (string.IsNullOrEmpty(CommandPath)) return null;

            object commandSource;
            switch (FindCommandIn)
            {
                case FindCommandOption.Sender:
                    commandSource = sender;
                    break;
                case FindCommandOption.AssociatedObject:
                    commandSource = AssociatedObject;
                    break;
                case FindCommandOption.EventArgs:
                    commandSource = args;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return ReflectionHelper.GetPropertyValue(commandSource, CommandPath) as ICommand;
        }

        private object GetCommandParameter(RoutedEventArgs args)
            => CommandParameter ?? (!string.IsNullOrEmpty(TriggerParameterPath) ? ReflectionHelper.GetPropertyValue(args, TriggerParameterPath) : null);
        #endregion
    }
}