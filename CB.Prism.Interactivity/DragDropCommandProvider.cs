using System;
using System.Windows;
using System.Windows.Input;
using Prism.Commands;


namespace CB.Prism.Interactivity
{
    public class DragDropCommandProvider
    {
        #region  Constructors & Destructor
        public DragDropCommandProvider()
        {
            DropFilesCommand = new DelegateCommand<object>(o => Drop(o, DataFormats.FileDrop));
        }
        #endregion


        #region  Commands
        public ICommand DropFilesCommand { get; }
        #endregion


        #region Events
        public event EventHandler<string[]> DropFiles;
        #endregion


        #region Implementation
        private void Drop(object dropData, string dataFormat)
        {
            var dataObject = dropData as IDataObject;

            if (dataObject == null)
            {
                var args = dropData as DragEventArgs;

                if (args != null)
                {
                    dataObject = args.Data;
                }
            }
            if (dataObject != null)
            {
                Drop(dataObject, dataFormat);
            }
        }

        private void Drop(IDataObject dataObject, string dataFormat)
        {
            var data = dataObject.GetData(dataFormat, true);
            if (Equals(dataFormat, DataFormats.FileDrop))
            {
                var files = data as string[];
                if (files != null) OnDropFiles(files);
            }
        }

        private void OnDropFiles(string[] files)
            => DropFiles?.Invoke(this, files);
        #endregion
    }
}