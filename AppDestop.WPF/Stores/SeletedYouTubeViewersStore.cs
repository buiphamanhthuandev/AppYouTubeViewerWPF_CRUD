using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouTubeViewer.Domain.Models;
namespace AppDestop.WPF.Stores
{
    public class SeletedYoutubeViewersStore
    {
        private readonly YoutubeViewersStore _YoutubeViewersStore;
        private YouTubeView? _selectedYoutubeViewer;

        public SeletedYoutubeViewersStore(YoutubeViewersStore YoutubeViewersStore)
        {
            _YoutubeViewersStore = YoutubeViewersStore;
            _YoutubeViewersStore.YoutubeViewerAdded += YoutubeViewersStore_YoutubeViewerAdded;
            _YoutubeViewersStore.YoutubeViewerUpdated += YoutubeViewersStore_YoutubeViewerUpdated;
        }

        private void YoutubeViewersStore_YoutubeViewerAdded(YouTubeView obj)
        {
            SelectedYoutubeViewer = obj;
        }

        private void YoutubeViewersStore_YoutubeViewerUpdated(YouTubeView obj)
        {
            if (obj.Id == SelectedYoutubeViewer?.Id)
            {
                SelectedYoutubeViewer = obj;
            }
        }

        public YouTubeView? SelectedYoutubeViewer
        {
            get
            {
                return _selectedYoutubeViewer;
            }
            set 
            {
                _selectedYoutubeViewer = value;
                SelectedYoutubeViewerChanged?.Invoke();
            }
        }
        public event Action? SelectedYoutubeViewerChanged;
    }
}
