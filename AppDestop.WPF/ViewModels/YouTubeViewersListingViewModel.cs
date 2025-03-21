using AppDestop.WPF.Commands;
using AppDestop.WPF.Stores;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using YouTubeViewer.Domain.Models;

namespace AppDestop.WPF.ViewModels
{
    public class YoutubeViewersListingViewModel : ViewModelBase
    {
        private readonly ObservableCollection<YoutubeViewersListingItemViewModel> _items;
        private readonly SeletedYoutubeViewersStore _seletedYoutubeViewersStore;
        private readonly YoutubeViewersStore _YoutubeViewersStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        public IEnumerable<YoutubeViewersListingItemViewModel> YoutubeViewersListingItemViews => _items;

        private YoutubeViewersListingItemViewModel? _selectedYoutubeViewerListingItemViewModel;
        public YoutubeViewersListingItemViewModel SelectedYoutubeViewerListingItemViewModel
        {
            get => _selectedYoutubeViewerListingItemViewModel!;
            set
            {
                _selectedYoutubeViewerListingItemViewModel = value;
                OnPropertyChanged(nameof(SelectedYoutubeViewerListingItemViewModel));
                _seletedYoutubeViewersStore.SelectedYoutubeViewer = _selectedYoutubeViewerListingItemViewModel?.YoutubeViewer;
            }
        }


        public YoutubeViewersListingViewModel(YoutubeViewersStore youtubeViewersStore,SeletedYoutubeViewersStore seletedYoutubeViewersStore, ModalNavigationStore modalNavigationStore)
        {
            _YoutubeViewersStore = youtubeViewersStore;
            _seletedYoutubeViewersStore = seletedYoutubeViewersStore;
            _modalNavigationStore = modalNavigationStore;
            _items = new ObservableCollection<YoutubeViewersListingItemViewModel>();

            

            _YoutubeViewersStore.YouTubeViewersLoaded += YoutubeViewersStore_YouTubeViewersLoaded;
            _YoutubeViewersStore.YoutubeViewerAdded += YoutubeViewersStore_YoutubeViewerAdded;
            _YoutubeViewersStore.YoutubeViewerUpdated += YoutubeViewersStore_YoutubeViewerUpdated;
            _YoutubeViewersStore.YoutubeViewerDeleted += YoutubeViewersStore_YoutubeViewerDeleted;
        }

        private void YoutubeViewersStore_YoutubeViewerDeleted(Guid id)
        {
            var youYubeViewerId = _items.FirstOrDefault(y => y.YoutubeViewer?.Id == id);
            if (youYubeViewerId != null)
            {
                _items.Remove(youYubeViewerId);
            }
        }



        protected override void Dispose()
        {
            _YoutubeViewersStore.YouTubeViewersLoaded -= YoutubeViewersStore_YouTubeViewersLoaded;
            _YoutubeViewersStore.YoutubeViewerAdded -= YoutubeViewersStore_YoutubeViewerAdded;
            _YoutubeViewersStore.YoutubeViewerUpdated -= YoutubeViewersStore_YoutubeViewerUpdated;
            _YoutubeViewersStore.YoutubeViewerDeleted -= YoutubeViewersStore_YoutubeViewerDeleted;
            base.Dispose();
        }

        private void YoutubeViewersStore_YouTubeViewersLoaded()
        {
            _items.Clear();
            Debug.WriteLine($"test load {_YoutubeViewersStore.YouTubeViewers}");
               
            foreach (YouTubeView item in _YoutubeViewersStore.YouTubeViewers)
            {
                AddYoutubeViewer(item);
            }
        }
        private void YoutubeViewersStore_YoutubeViewerAdded(YouTubeView YoutubeViewer)
        {
            AddYoutubeViewer(YoutubeViewer);
        }
        private void YoutubeViewersStore_YoutubeViewerUpdated(YouTubeView obj)
        {
            YoutubeViewersListingItemViewModel YoutubeViewerViewModel = _items.FirstOrDefault(y => y.YoutubeViewer.Id == obj.Id)!;
            if (YoutubeViewerViewModel != null)
            {
                YoutubeViewerViewModel.Update(obj);
            }
        }
        private void AddYoutubeViewer(YouTubeView YoutubeViewer)
        {
            YoutubeViewersListingItemViewModel itemViewModel = new YoutubeViewersListingItemViewModel(YoutubeViewer, _YoutubeViewersStore, _modalNavigationStore);
            _items.Add(itemViewModel);
        }
    }
}
