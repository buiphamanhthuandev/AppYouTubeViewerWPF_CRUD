using AppDestop.WPF.Commands;
using AppDestop.WPF.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppDestop.WPF.ViewModels
{
    public class AddYoutubeViewerViewModel : ViewModelBase
    {
        public YoutubeViewerDetailsFormViewModel YoutubeViewerDetailsFormViewModel { get; set; }

        public AddYoutubeViewerViewModel(YoutubeViewersStore YoutubeViewersStore, ModalNavigationStore modalNavigationStore)
        {
            ICommand cancelCommand = new CloseModalCommand(modalNavigationStore);
            ICommand submitCommand = new AddYoutubeViewerCommand(this, YoutubeViewersStore, modalNavigationStore);
            YoutubeViewerDetailsFormViewModel = new YoutubeViewerDetailsFormViewModel(submitCommand, cancelCommand);
        }
    }
}
