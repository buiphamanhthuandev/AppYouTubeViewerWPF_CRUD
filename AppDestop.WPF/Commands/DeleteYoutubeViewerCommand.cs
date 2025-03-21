using AppDestop.WPF.Stores;
using AppDestop.WPF.ViewModels;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouTubeViewer.Domain.Models;

namespace AppDestop.WPF.Commands
{
    class DeleteYoutubeViewerCommand : AsyncCommandBase
    {
        private readonly YoutubeViewersListingItemViewModel _youtubeViewersListingItemViewModel;
        private readonly YoutubeViewersStore _YoutubeViewersStore;

        public DeleteYoutubeViewerCommand(YoutubeViewersListingItemViewModel YoutubeViewersListingItemViewModel, YoutubeViewersStore YoutubeViewersStore)
        {
            _youtubeViewersListingItemViewModel = YoutubeViewersListingItemViewModel;
            _YoutubeViewersStore = YoutubeViewersStore;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            _youtubeViewersListingItemViewModel.IsDeleting = true;
            _youtubeViewersListingItemViewModel.ErrorMessage = null;
            YouTubeView youTubeView = _youtubeViewersListingItemViewModel.YoutubeViewer!;
            try
            {
                await _YoutubeViewersStore.Delete(youTubeView.Id);
            }
            catch (Exception ex)
            {
            _youtubeViewersListingItemViewModel.ErrorMessage = "Failed to delete YouTube viewer. Please try again.";
            }
            finally
            {
                _youtubeViewersListingItemViewModel.IsDeleting = false;
            }
        }
    }
}
