using System.ComponentModel;
using System.Windows.Input;
using Prism.Commands;


namespace CB.Prism.Interactivity
{
    public class WindowRequestProvider
    {
        #region  Constructors & Destructor
        public WindowRequestProvider()
        {
            RaiseCommand = new DelegateCommand<object>(Raise);
        }
        #endregion


        #region  Commands
        public ICommand RaiseCommand { get; }
        #endregion


        #region  Properties & Indexers
        public WindowRequest Request { get; } = new WindowRequest();
        #endregion


        #region Methods
        public void Raise(object parameter)
        {
            WindowRequestAction action;
            if (parameter is WindowRequestAction)
            {
                action = (WindowRequestAction)parameter;
            }
            else
            {
                var enumConverter = new EnumConverter(typeof(WindowRequestAction));
                var value = enumConverter.ConvertFrom(parameter);
                if (value == null) return;

                action = (WindowRequestAction)value;
            }
            Request.Raise(action);
        }
        #endregion
    }
}