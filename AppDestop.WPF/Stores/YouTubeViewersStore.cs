
using System.Security.Permissions;
using YouTubeViewer.Domain.Commands;
using YouTubeViewer.Domain.Models;
using YouTubeViewer.Domain.Queries;

namespace AppDestop.WPF.Stores
{
    public class YoutubeViewersStore
    {
        private readonly IGetAllYouTubeViewerQuery _getAllYouTubeViewerQuery;
        private readonly ICreateYouTubeViewerCommand _createYouTubeViewerCommand;
        private readonly IUpdateYouTubeViewerCommand _updateYouTubeViewerCommand;
        private readonly IDeleteYouTubeViewerCommand _deleteYouTubeViewerCommand;
        private readonly List<YouTubeView> _youTubeViewers;

        public IEnumerable<YouTubeView> YouTubeViewers => _youTubeViewers;
        public YoutubeViewersStore(IGetAllYouTubeViewerQuery getAllYouTubeViewerQuery, 
            ICreateYouTubeViewerCommand createYouTubeViewerCommand, 
            IUpdateYouTubeViewerCommand updateYouTubeViewerCommand, 
            IDeleteYouTubeViewerCommand deleteYouTubeViewerCommand)
        {
            _youTubeViewers = new List<YouTubeView>();
            _getAllYouTubeViewerQuery = getAllYouTubeViewerQuery;
            _createYouTubeViewerCommand = createYouTubeViewerCommand;
            _updateYouTubeViewerCommand = updateYouTubeViewerCommand;
            _deleteYouTubeViewerCommand = deleteYouTubeViewerCommand;

            
        }
        public event Action? YouTubeViewersLoaded;
        public event Action<YouTubeView>? YoutubeViewerAdded;
        public event Action<YouTubeView>? YoutubeViewerUpdated;
        public event Action<Guid> YoutubeViewerDeleted;
        public async Task Add(YouTubeView YoutubeViewer)
        {
            await _createYouTubeViewerCommand.Execute(YoutubeViewer);
            _youTubeViewers.Add(YoutubeViewer);
            YoutubeViewerAdded?.Invoke(YoutubeViewer);
        }
        public async Task Update(YouTubeView YoutubeViewer)
        {
            await _updateYouTubeViewerCommand.Execute(YoutubeViewer);

            var currentIndex = _youTubeViewers.FindIndex(y => y.Id == YoutubeViewer.Id); 
            if(currentIndex != -1)
            {
                _youTubeViewers[currentIndex] = YoutubeViewer;
            }else
            {
                _youTubeViewers.Add(YoutubeViewer);
            }
            YoutubeViewerUpdated?.Invoke(YoutubeViewer);
        }
        public async Task Load()
        {
            IEnumerable<YouTubeView> youTubeViewers = await _getAllYouTubeViewerQuery.Execute();

            _youTubeViewers.Clear();

            _youTubeViewers.AddRange(youTubeViewers);

            YouTubeViewersLoaded?.Invoke();
        }
        public async Task Delete(Guid id)
        {
            await _deleteYouTubeViewerCommand.Execute(id);

            _youTubeViewers.RemoveAll(y => y.Id == id);

            YoutubeViewerDeleted?.Invoke(id);
        }
    }
}
