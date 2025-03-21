using AppDestop.WPF.Commands;
using AppDestop.WPF.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using YouTubeViewer.Domain.Models;

namespace AppDestop.WPF.ViewModels
{
    public class EditYoutubeViewerViewModel : ViewModelBase
    {
        public YoutubeViewerDetailsFormViewModel YoutubeViewerDetailsFormViewModel { get; }
        public EditYoutubeViewerViewModel(YouTubeView YoutubeViewer, YoutubeViewersStore YoutubeViewersStore, ModalNavigationStore modalNavigationStore) 
        {
            ICommand cancelCommand = new CloseModalCommand(modalNavigationStore);
            ICommand submitCommand = new EditYoutubeViewerCommand(this, YoutubeViewersStore, modalNavigationStore);
            YoutubeViewerDetailsFormViewModel = new YoutubeViewerDetailsFormViewModel(submitCommand, cancelCommand)
            {
                Id = YoutubeViewer.Id,
                UserName = YoutubeViewer.UserName,
                IsSubscribed = YoutubeViewer.IsSubscribed,
                IsMember = YoutubeViewer.IsMember,
            };
        }
    }
}
