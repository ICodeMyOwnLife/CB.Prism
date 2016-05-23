using System;
using System.Threading.Tasks;
using CB.Model.Prism;
using CB.Prism.Interactivity;


namespace Test.CB.Prism.Interactivity
{
    public class ProgressViewModel: PrismViewModelBase, IReportProgress<double>
    {
        #region Fields
        private double _progress;
        #endregion


        #region  Properties & Indexers
        public object Content { get; set; }

        public double Progress
        {
            get { return _progress; }
            private set { SetProperty(ref _progress, value); }
        }

        public string Title { get; set; }
        #endregion


        #region Methods
        public static ProgressViewModel StartNew()
        {
            var pvm = new ProgressViewModel { Title = "Progress" };
            pvm.Start();
            return pvm;
        }

        public async Task Start()
        {
            var random = new Random(DateTime.Now.Millisecond);

            Progress = 0;
            while (Progress < 1)
            {
                await Task.Delay(random.Next(300));
                var increase = random.NextDouble() * 0.05;
                Progress = Progress + increase > 1 ? 1 : Progress + increase;
            }
        }
        #endregion
    }
}