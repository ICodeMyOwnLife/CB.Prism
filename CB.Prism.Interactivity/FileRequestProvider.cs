using System;


namespace CB.Prism.Interactivity
{
    public class FileRequestProvider: RequestProviderBase
    {
        public virtual void OpenFile(IOpenFileDialogInfo info, Action<IOpenFileDialogInfo> callback)
            => Request.Raise(info, callback);

        public virtual void BrowseFolder(IBrowseFolderDialogInfo info, Action<IBrowseFolderDialogInfo> callback)
            => Request.Raise(info, callback);

        public virtual void SaveFile(ISaveFileDialogInfo info, Action<ISaveFileDialogInfo> callback)
            => Request.Raise(info, callback);
    }
}