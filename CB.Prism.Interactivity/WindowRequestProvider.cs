using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;


namespace CB.Prism.Interactivity
{
    public class WindowRequestProvider
    {
        #region  Constructors & Destructor
        public WindowRequestProvider()
        {
            CloseWindowCommand = new DelegateCommand(CloseWindow);
            HideWindowCommand = new DelegateCommand(HideWindow);

            //RaiseCommand = new DelegateCommand<WindowRequestAction>(Raise);
            ShowWindowCommand = new DelegateCommand(ShowWindow);
        }
        #endregion


        #region  Commands
        public ICommand CloseWindowCommand { get; }
        public ICommand HideWindowCommand { get; }

        //public ICommand RaiseCommand { get; }
        public ICommand ShowWindowCommand { get; }
        #endregion


        #region  Properties & Indexers
        public WindowRequest Request { get; } = new WindowRequest();
        #endregion


        #region Methods
        public void CloseWindow()
            => Raise(WindowRequestAction.Close);

        public void HideWindow()
            => Raise(WindowRequestAction.Hide);

        public void Raise(WindowRequestAction requestAction)
            => Request.Raise(requestAction);

        public void ShowWindow()
            => Raise(WindowRequestAction.Show);
        #endregion
    }
}


// TODO: Complete WindowRequestProvider