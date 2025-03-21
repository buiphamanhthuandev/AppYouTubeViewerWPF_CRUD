using AppDestop.WPF.Stores;
using AppDestop.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouTubeViewer.Domain.Models;

namespace AppDestop.WPF.Commands
{
    public class OpenEditYoutubeViewerCommand :CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly YoutubeViewersListingItemViewModel _YoutubeViewersListingItemViewModel;
        private readonly YoutubeViewersStore _YoutubeViewersStore;
        
        public OpenEditYoutubeViewerCommand(YoutubeViewersListingItemViewModel YoutubeViewersListingItemViewModel, YoutubeViewersStore YoutubeViewersStore, ModalNavigationStore modalNavigationStore)
        {
            
            _modalNavigationStore = modalNavigationStore;
            _YoutubeViewersListingItemViewModel = YoutubeViewersListingItemViewModel;
            _YoutubeViewersStore = YoutubeViewersStore;
        }

        public override void Execute(object? parameter)
        {
            YouTubeView YoutubeViewer = _YoutubeViewersListingItemViewModel.YoutubeViewer;
            EditYoutubeViewerViewModel editYoutubeViewerViewModel = new EditYoutubeViewerViewModel(YoutubeViewer, _YoutubeViewersStore, _modalNavigationStore);

            _modalNavigationStore.CurrentViewModel = editYoutubeViewerViewModel;
        }
    }
}
