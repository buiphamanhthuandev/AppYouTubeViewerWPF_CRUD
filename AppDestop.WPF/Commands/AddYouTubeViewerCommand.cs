
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
    public class AddYoutubeViewerCommand : AsyncCommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly AddYoutubeViewerViewModel _addYoutubeViewerViewModel;
        private readonly YoutubeViewersStore _YoutubeViewersStore;
        public AddYoutubeViewerCommand(AddYoutubeViewerViewModel addYoutubeViewerViewModel, YoutubeViewersStore YoutubeViewersStore, ModalNavigationStore modalNavigationStore)
        {
            _addYoutubeViewerViewModel = addYoutubeViewerViewModel;
            _YoutubeViewersStore = YoutubeViewersStore;
            _modalNavigationStore = modalNavigationStore;
        }
        public override async Task ExecuteAsync(object? parameter)
        {
            YoutubeViewerDetailsFormViewModel formViewModel = _addYoutubeViewerViewModel.YoutubeViewerDetailsFormViewModel;
            //Add YouTube viewer to database
            formViewModel.ErrorMessage = null;
            formViewModel.IsSubmitting = true;

            YouTubeView YoutubeViewer = new YouTubeView(Guid.NewGuid(), formViewModel.UserName, formViewModel.IsSubscribed, formViewModel.IsMember);

            try
            {
                await _YoutubeViewersStore.Add(YoutubeViewer);
                _modalNavigationStore.Close();
            }
            catch (Exception)
            {
                formViewModel.ErrorMessage = "Failed to Add Youtube viewer , Please try again later.";
            }
            finally
            {
                formViewModel.IsSubmitting = false;
            }
            
           
        }
    }
}
