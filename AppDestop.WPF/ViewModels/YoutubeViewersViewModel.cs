using AppDestop.WPF.Commands;
using AppDestop.WPF.Stores;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppDestop.WPF.ViewModels
{
    public class YoutubeViewersViewModel : ViewModelBase
    {
        public YoutubeViewersListingViewModel? YoutubeViewersListingViewModel { get; }
        public YoutubeViewersDetailsViewModel? YoutubeViewersDetailsViewModel { get; }

        private bool _isLoading;
        public bool IsLoading 
        {   
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }
        private string _errorMessage;
        public string ErrorMessage {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
                OnPropertyChanged(nameof(HasErrorMessage));
            }
        }

        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

        public ICommand AddYoutubeViewerCommand { get;}
        public ICommand LoadYouTubeViewerCommand { get; }

        public YoutubeViewersViewModel(YoutubeViewersStore YoutubeViewersStore, SeletedYoutubeViewersStore seletedYoutubeViewersStore, ModalNavigationStore modalNavigationStore)
        {

            YoutubeViewersListingViewModel = new YoutubeViewersListingViewModel(YoutubeViewersStore,seletedYoutubeViewersStore, modalNavigationStore);
            YoutubeViewersDetailsViewModel = new YoutubeViewersDetailsViewModel(seletedYoutubeViewersStore);

            LoadYouTubeViewerCommand = new LoadYouTubeViewersCommand(this, YoutubeViewersStore);
            AddYoutubeViewerCommand = new OpenAddYoutubeViewerCommand(YoutubeViewersStore, modalNavigationStore);
            
        }
        public static YoutubeViewersViewModel LoadViewModel(YoutubeViewersStore youtubeViewersStore, SeletedYoutubeViewersStore seletedYoutubeViewersStore, ModalNavigationStore modalNavigationStore)
        {
            
            YoutubeViewersViewModel viewModel = new YoutubeViewersViewModel(youtubeViewersStore, seletedYoutubeViewersStore, modalNavigationStore);

            viewModel.LoadYouTubeViewerCommand.Execute(null);

            return viewModel;
        }
    }
}         
