using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using CB.Model.Common;
using CB.Model.Prism;
using CB.Prism.Interactivity;
using Microsoft.Practices.Prism.Commands;


namespace Test.CB.Prism.Interactivity
{
    public class MainWindowViewModel: PrismViewModelBase
    {
        #region Fields
        private string _path;
        private WindowRequestAction? _selectedRequestAction;
        #endregion


        #region  Constructors & Destructor
        public MainWindowViewModel()
        {
            BrowseCommand = new DelegateCommand(Browse);
            ExecuteRequestCommand = new DelegateCommand(ExecuteRequest);
            OpenCommand = new DelegateCommand(Open);
            SaveCommand = new DelegateCommand(Save);
            ShowCommand = new DelegateCommand(Progress);
        }
        #endregion


        #region  Commands
        public ICommand BrowseCommand { get; }

        public ICommand ExecuteRequestCommand { get; }

        public ICommand OpenCommand { get; }

        public ICommand SaveCommand { get; }

        public ICommand ShowCommand { get; }
        #endregion


        #region  Properties & Indexers
        public bool CanExecuteRequest => SelectedRequestAction.HasValue;

        public CommonInteractionRequest IORequest { get; } = new CommonInteractionRequest();

        public string Path
        {
            get { return _path; }
            private set { SetProperty(ref _path, value); }
        }

        /*public GenericInteractionRequest<IBrowseFolderDialogInfo> BrowseFolderDialogRequest { get; } =
            new GenericInteractionRequest<IBrowseFolderDialogInfo>();*/

        /*public GenericInteractionRequest<IOpenFileDialogInfo> OpenFileDialogRequest { get; } =
            new GenericInteractionRequest<IOpenFileDialogInfo>();*/

        public CommonInteractionRequest ProgressRequest { get; } = new CommonInteractionRequest();

        public WindowRequestAction? SelectedRequestAction
        {
            get { return _selectedRequestAction; }
            set { SetProperty(ref _selectedRequestAction, value); }
        }

        public WindowRequest WindowRequest { get; } = new WindowRequest();
        #endregion


        #region Methods
        /*public void Browse()
            => BrowseFolderDialogRequest.Raise(new BrowseFolderDialogInfo { Title = "Browse folder" },
                BrowseFolderInfoAction);*/

        public void Browse()
        {
            var browseInfo = new BrowseFolderDialogInfo { Title = "Browse folder" };
            IORequest.Raise(browseInfo, _ => BrowseFolderInfoAction(browseInfo));
        }

        public void ExecuteRequest()
        {
            if (!CanExecuteRequest) return;

            WindowRequest.Raise(SelectedRequestAction.Value);
        }

        /*public void Open()
            => OpenFileDialogRequest.Raise(new OpenFileDialogInfo { Title = "Get Save Path" },
                FileInfoAction);*/

        public void Open()
        {
            var openInfo = new OpenFileDialogInfo { Title = "Get Save Path" };
            IORequest.Raise(openInfo, _ => FileInfoAction(openInfo));
        }

        /*public void Progress()
        {
            var fileProgressReporter = CreateFileProgressReporter();
            WindowRequest.Raise(new FileTransferProgressViewModel
            {
                Title = "Progress",
                ProgressReporter = fileProgressReporter
            });
        }*/

        public void Progress()
        {
            /*var fileProgressReporter = CreateFileProgressReporter();
            ProgressRequest.Raise(new FileTransferProgressViewModel
            {
                Title = "Progress",
                ProgressReporter = fileProgressReporter
            });*/
            ProgressRequest.Raise(new FileTransferProgressTestViewModel());
        }

        /*public void Save()
            => SaveFileDialogRequest.Raise(new SaveFileDialogInfo { Title = "Get Save Path" },
                FileInfoAction);*/

        public void Save()
        {
            var saveInfo = new SaveFileDialogInfo { Title = "Get Save Path" };
            IORequest.Raise(saveInfo, _ => FileInfoAction(saveInfo));
        }
        #endregion


        #region Implementation
        private void BrowseFolderInfoAction(IBrowseFolderDialogInfo info)
        {
            Path = info.Confirmed ? info.SelectedPath : null;
        }

        private static FileProgressReporter CreateFileProgressReporter()
        {
            const long FILE_SIZE = 6312487123;

            var fileProgressReporter = new FileProgressReporter
            {
                FileName = "C:\\a.txt",
                FileSize = FILE_SIZE
            };
            Task.Run(() =>
            {
                long transferred = 0;
                var random = new Random(DateTime.Now.Millisecond);

                while (transferred < FILE_SIZE)
                {
                    Thread.Sleep(100);
                    var increase = random.Next(1000000, 10000000);
                    transferred = transferred + increase >= FILE_SIZE ? FILE_SIZE : transferred + increase;
                    Debug.WriteLine($"transferred: {transferred}");
                    fileProgressReporter.Report(transferred);
                }
            });
            fileProgressReporter.Start();
            return fileProgressReporter;
        }

        private void FileInfoAction(IFileDialogInfo info)
            => Path = info.Confirmed ? info.FileName : null;
        #endregion


        /*public GenericInteractionRequest<ISaveFileDialogInfo> SaveFileDialogRequest { get; } =
            new GenericInteractionRequest<ISaveFileDialogInfo>();*/

        /*public InteractionRequest<FileTransferProgressViewModel> WindowRequest { get; } =
            new InteractionRequest<FileTransferProgressViewModel>();*/
    }
}


// TODO: Test File Progress Pause, Resume, Cancel