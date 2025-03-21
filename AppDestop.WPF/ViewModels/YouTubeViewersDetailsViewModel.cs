
using AppDestop.WPF.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouTubeViewer.Domain.Models;

namespace AppDestop.WPF.ViewModels
{
    public class YoutubeViewersDetailsViewModel : ViewModelBase
    {
        private readonly SeletedYoutubeViewersStore _seletedYoutubeViewersStore;
        private YouTubeView SelectedYoutubeViewer => _seletedYoutubeViewersStore.SelectedYoutubeViewer;

        public bool HasSelectedYoutubeViewer => SelectedYoutubeViewer != null;
        public string UserName => SelectedYoutubeViewer?.UserName ?? "Unknowm";
        public string IsSubscribedDisplay => (SelectedYoutubeViewer?.IsSubscribed ?? false) ? "Yes" : "No";
        public string IsMemberDisplay => (SelectedYoutubeViewer?.IsMember ?? false) ? "Yes" : "No";
        public YoutubeViewersDetailsViewModel(SeletedYoutubeViewersStore seletedYoutubeViewersStore)
        {
            _seletedYoutubeViewersStore = seletedYoutubeViewersStore;
            _seletedYoutubeViewersStore.SelectedYoutubeViewerChanged += SelectedYoutubeViewerStore_SelectedYoutubeViewerChanged;
        }

        protected override void Dispose()
        {
            _seletedYoutubeViewersStore.SelectedYoutubeViewerChanged -= SelectedYoutubeViewerStore_SelectedYoutubeViewerChanged;
            base.Dispose();
        }
        private void SelectedYoutubeViewerStore_SelectedYoutubeViewerChanged()
        {
            OnPropertyChanged(nameof(HasSelectedYoutubeViewer));
            OnPropertyChanged(nameof(UserName));
            OnPropertyChanged(nameof(IsSubscribedDisplay));
            OnPropertyChanged(nameof(IsMemberDisplay));
        }
    }
}
