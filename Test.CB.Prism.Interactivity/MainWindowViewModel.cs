using System.Windows.Input;
using CB.Model.Prism;
using CB.Prism.Interactivity;
using Microsoft.Practices.Prism.Commands;
using Prism.Interactivity.InteractionRequest;


namespace Test.CB.Prism.Interactivity
{
    public class MainWindowViewModel: PrismViewModelBase
    {
        #region Fields
        private string _saveFilePath;
        #endregion


        #region  Constructors & Destructor
        public MainWindowViewModel()
        {
            SaveCommand = new DelegateCommand(Save);
            ShowCommand = new DelegateCommand(Show);
        }
        #endregion


        #region  Properties & Indexers
        public ICommand SaveCommand { get; }

        public InteractionRequest<ISaveFileDialogInfo> SaveFileDialogRequest { get; } =
            new InteractionRequest<ISaveFileDialogInfo>();

        public string SaveFilePath
        {
            get { return _saveFilePath; }
            private set { SetProperty(ref _saveFilePath, value); }
        }

        public ICommand ShowCommand { get; }

        public InteractionRequest<ProgressViewModel> WindowRequest { get; } =
            new InteractionRequest<ProgressViewModel>();
        #endregion


        #region Methods
        public void Save()
            => SaveFileDialogRequest.Raise(new SaveFileDialogInfo { Title = "Get Save Path" },
                info => { SaveFilePath = info.Confirmed ? info.FileName : null; });

        public void Show()
            => WindowRequest.Raise(ProgressViewModel.StartNew());
        #endregion
    }
}