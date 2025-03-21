using AppDestop.WPF.Stores;
using AppDestop.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppDestop.WPF.Commands
{
    public class OpenAddYoutubeViewerCommand : CommandBase
    {
        private readonly YoutubeViewersStore _youtubeViewersStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public OpenAddYoutubeViewerCommand(YoutubeViewersStore YoutubeViewersStore, ModalNavigationStore modalNavigationStore)
        {
            _youtubeViewersStore = YoutubeViewersStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object? parameter)
        { 
            AddYoutubeViewerViewModel addYoutubeViewerViewModel = new AddYoutubeViewerViewModel(_youtubeViewersStore, _modalNavigationStore);
            if (addYoutubeViewerViewModel != null)
            {
                _modalNavigationStore.CurrentViewModel = addYoutubeViewerViewModel;
            }
        }
    }
}
