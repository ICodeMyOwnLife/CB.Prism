using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using CB.Model.Common;
using CB.Model.Prism;
using CB.Prism.Interactivity;
using CB.Wpf.UserControls;
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
            ShowCommand = new DelegateCommand(Progress);
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

        public InteractionRequest<FileTransferProgressViewModel> WindowRequest { get; } =
            new InteractionRequest<FileTransferProgressViewModel>();
        #endregion


        #region Methods
        public void Browse()
            => BrowseFolderDialogRequest.Raise(new BrowseFolderDialogInfo { Title = "Browse folder" },
                BrowseFolderInfoAction);

        public void Open()
            => OpenFileDialogRequest.Raise(new OpenFileDialogInfo { Title = "Get Save Path" },
                FileInfoAction);

        public void Progress()
        {
            var fileProgressReporter = CreateFileProgressReporter();
            WindowRequest.Raise(new FileTransferProgressViewModel
            {
                Title = "Progress",
                ProgressReporter = fileProgressReporter
            });
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

        public void Save()
            => SaveFileDialogRequest.Raise(new SaveFileDialogInfo { Title = "Get Save Path" },
                FileInfoAction);
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