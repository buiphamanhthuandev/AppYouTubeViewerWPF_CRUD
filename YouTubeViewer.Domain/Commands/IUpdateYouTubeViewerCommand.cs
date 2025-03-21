using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouTubeViewer.Domain.Models;

namespace YouTubeViewer.Domain.Commands
{
    public interface IUpdateYouTubeViewerCommand
    {
        Task Execute(YouTubeView youTubeView);
    }
}
