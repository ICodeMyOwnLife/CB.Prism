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
        private string _path;
        #endregion


        #region  Constructors & Destructor
        public MainWindowViewModel()
        {
            BrowseCommand = new DelegateCommand(Browse);
            OpenCommand = new DelegateCommand(Open);
            SaveCommand = new DelegateCommand(Save);
            ShowCommand = new DelegateCommand(Show);
        }
        #endregion


        #region  Properties & Indexers
        public ICommand BrowseCommand { get; }

        public InteractionRequest<IBrowseFolderDialogInfo> BrowseFolderDialogRequest { get; } =
            new InteractionRequest<IBrowseFolderDialogInfo>();

        public ICommand OpenCommand { get; }

        public InteractionRequest<IOpenFileDialogInfo> OpenFileDialogRequest { get; } =
            new InteractionRequest<IOpenFileDialogInfo>();

        public string Path
        {
            get { return _path; }
            private set { SetProperty(ref _path, value); }
        }

        public ICommand SaveCommand { get; }

        public InteractionRequest<ISaveFileDialogInfo> SaveFileDialogRequest { get; } =
            new InteractionRequest<ISaveFileDialogInfo>();

        public ICommand ShowCommand { get; }

        public InteractionRequest<ProgressViewModel> WindowRequest { get; } =
            new InteractionRequest<ProgressViewModel>();
        #endregion


        #region Methods
        public void Browse()
            => BrowseFolderDialogRequest.Raise(new BrowseFolderDialogInfo { Title = "Browse folder" },
                BrowseFolderInfoAction);

        public void Open()
            => OpenFileDialogRequest.Raise(new OpenFileDialogInfo { Title = "Get Save Path" },
                FileInfoAction);

        public void Save()
            => SaveFileDialogRequest.Raise(new SaveFileDialogInfo { Title = "Get Save Path" },
                FileInfoAction);

        public void Show()
            => WindowRequest.Raise(ProgressViewModel.StartNew());
        #endregion


        #region Implementation
        private void BrowseFolderInfoAction(IBrowseFolderDialogInfo info)
        {
            Path = info.Confirmed ? info.SelectedPath : null;
        }

        private void FileInfoAction(IFileDialogInfo info)
        {
            Path = info.Confirmed ? info.FileName : null;
        }
        #endregion
    }
}