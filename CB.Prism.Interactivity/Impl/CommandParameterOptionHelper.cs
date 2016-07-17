using System;
using System.Windows;


namespace CB.Prism.Interactivity
{
    internal class CommandParameterOptionHelper
    {
        internal static object GetCommandParameter(RoutedEventArgs args, CommandParameterOption commandParameterOption)
        {
            switch (commandParameterOption)
            {
                case CommandParameterOption.DataContext:
                    return (args.OriginalSource as FrameworkElement)?.DataContext;
                case CommandParameterOption.Source:
                    return args.Source;
                case CommandParameterOption.OriginalSource:
                    return args.OriginalSource;
                case CommandParameterOption.EventArgs:
                    return args;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}