using FileExplorer.Models.Managers.CreatedEntries;
using FileExplorer.Models.Managers.Roots;
using FileExplorer.Models.Objects.FileSystemEntries;
using FileExplorer.Models.Objects.FileSystemEntries.FileSystemFactories;
using FileExplorer.Presenters;
using FileExplorer.Views;
using Ninject;
using System;

namespace FileExplorer
{
    internal sealed class Application
    {
        private readonly Lazy<IKernel> container;

        public Application()
        {
            container = new Lazy<IKernel>(CreateContainer);
        }

        private IKernel CreateContainer()
        {
            var container = new StandardKernel();

            container.Bind<IFileSystemFactory<FileSystemEntry>>().To<FileSystemFactory>().InSingletonScope();
            container.Bind<ISystemRootManager, ISystemRootProvider>().To<SystemRootManager>().InSingletonScope();
            container.Bind<ICreatedEntriesManager, ICreatedEntriesProvider>().To<CreatedEntriesManager>().InSingletonScope();
            container.Bind<ITreeView, IListView, IHistoryView>().To<Explorer>().InSingletonScope();
            container.Bind<FileSystemEntry>().To<FileSystemEntry>().InSingletonScope();

            return container;
        }

        public void Run()
        {
            ExplorerPresenter explorerPresenter = container.Value.Get<ExplorerPresenter>();
            explorerPresenter.Run();
        }
    }
}