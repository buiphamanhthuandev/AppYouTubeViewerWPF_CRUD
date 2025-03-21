
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
    public class EditYoutubeViewerCommand : AsyncCommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly EditYoutubeViewerViewModel _editYoutubeViewerViewModel;
        private readonly YoutubeViewersStore _YoutubeViewersStore;
        public EditYoutubeViewerCommand(EditYoutubeViewerViewModel editYoutubeViewerViewModel, YoutubeViewersStore YoutubeViewersStore, ModalNavigationStore modalNavigationStore)
        {
            _modalNavigationStore = modalNavigationStore;
            _editYoutubeViewerViewModel = editYoutubeViewerViewModel;
            _YoutubeViewersStore = YoutubeViewersStore;
        }
        public override async Task ExecuteAsync(object? parameter)
        {
            //Edit YouTube viewer to database

            YoutubeViewerDetailsFormViewModel formViewModel = _editYoutubeViewerViewModel.YoutubeViewerDetailsFormViewModel;
            //Add YouTube viewer to database
            formViewModel.ErrorMessage = null;
            formViewModel.IsSubmitting = true;

            YouTubeView YoutubeViewer = new YouTubeView(formViewModel.Id, formViewModel.UserName, formViewModel.IsSubscribed, formViewModel.IsMember);

            try
            {
                await _YoutubeViewersStore.Update(YoutubeViewer);
                _modalNavigationStore.Close();
            }
            catch (Exception)
            {
                formViewModel.ErrorMessage = "Failed to update Youtube viewer , Please try again later.";
            }
            finally
            {
                formViewModel.IsSubmitting = false;
            }

        }
    }
}
