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
    public class YoutubeViewersListingItemViewModel : ViewModelBase
    {
        public YouTubeView? YoutubeViewer { get; private set; }
        public string? UserName => YoutubeViewer?.UserName;
        private bool _isDeleting;
        public bool IsDeleting {
            get => _isDeleting;
            set
            {
                _isDeleting = value;
                OnPropertyChanged(nameof(IsDeleting));
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
                OnPropertyChanged(nameof(HasErrorMessage));
            }
        }

        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

        public ICommand? EditCommand { get; set; }
        public ICommand? DeleteCommand { get; set; }

        public YoutubeViewersListingItemViewModel(YouTubeView YoutubeViewer, YoutubeViewersStore YoutubeViewersStore, ModalNavigationStore modalNavigationStore)
        {
            this.YoutubeViewer = YoutubeViewer;
            EditCommand = new OpenEditYoutubeViewerCommand(this, YoutubeViewersStore, modalNavigationStore);
            DeleteCommand = new DeleteYoutubeViewerCommand(this,YoutubeViewersStore);
        }

        public void Update(YouTubeView obj)
        {
            YoutubeViewer = obj;
            OnPropertyChanged(nameof(UserName));
        }
    }
}
