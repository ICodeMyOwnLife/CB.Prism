using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using CB.Model.Common;
using CB.Wpf.UserControls;


namespace Test.CB.Prism.Interactivity
{
    public class FileTransferProgressTestViewModel: FileTransferProgressViewModel
    {
        #region Fields
        private static readonly Random _random = new Random(DateTime.Now.Millisecond);
        private bool _canceled;
        private bool _continue = true;
        #endregion


        #region  Constructors & Destructor
        public FileTransferProgressTestViewModel()
        {
            Title = "Progress";
            CancelAction = () => _canceled = true;
            PauseAction = () => _continue = false;
            ResumeAction = () => _continue = true;
            ProgressReporter = CreateFileProgressReporter();
        }
        #endregion


        #region Implementation
        private FileProgressReporter CreateFileProgressReporter()
        {
            var fileSize = 4000000000L + _random.Next(1000000000, int.MaxValue);

            var fileProgressReporter = new FileProgressReporter
            {
                FileName = $@"C:\{_random.Next()}.txt",
                FileSize = fileSize
            };

            Task.Run(() =>
            {
                long transferred = 0;

                while (transferred < fileSize && !_canceled)
                {
                    Thread.Sleep(100);
                    if (_continue)
                    {
                        var increase = _random.Next(1000000, 10000000);
                        transferred = transferred + increase >= fileSize ? fileSize : transferred + increase;
                        Debug.WriteLine($"transferred: {transferred}");
                        fileProgressReporter.Report(transferred);
                    }
                }
            });
            fileProgressReporter.Start();
            return fileProgressReporter;
        }
        #endregion
    }
}