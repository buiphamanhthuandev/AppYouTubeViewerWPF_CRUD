using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouTubeViewer.Domain.Commands;
using YouTubeViewer.Domain.Models;
using YouTubeViewer.EntityFramework.DTOs;

namespace YouTubeViewer.EntityFramework.Commands
{
    public class DeleteYouTubeViewerCommand : IDeleteYouTubeViewerCommand
    {
        private readonly YouTubeViewersDbContextFactory _contextFactory;

        public DeleteYouTubeViewerCommand(YouTubeViewersDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task Execute(Guid id)
        {
            using (YouTubeViewersDbContext context = _contextFactory.Create())
            {
                var result = context.YouTubeViews.Where(x => x.Id == id).FirstOrDefault();
                if (result != null)
                {
                    context.YouTubeViews.Remove(result);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
