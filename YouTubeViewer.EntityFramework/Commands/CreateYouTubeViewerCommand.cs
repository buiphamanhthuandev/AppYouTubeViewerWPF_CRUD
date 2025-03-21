using Microsoft.EntityFrameworkCore;
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
    public class CreateYouTubeViewerCommand : ICreateYouTubeViewerCommand
    {
        private readonly YouTubeViewersDbContextFactory _contextFactory;

        public CreateYouTubeViewerCommand(YouTubeViewersDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task Execute(YouTubeView youTubeView)
        {
          
            using (YouTubeViewersDbContext context = _contextFactory.Create())
            {
                await Task.Delay(5000);
                YouTubeViewDto youTubeViewDto = new YouTubeViewDto()
                {
                    Id = youTubeView.Id,
                    UserName = youTubeView.UserName,
                    IsSubscribed = youTubeView.IsSubscribed,
                    IsMember = youTubeView.IsMember,
                };
                context.YouTubeViews.Add(youTubeViewDto);
                await context.SaveChangesAsync();
            }
        }
    }
}
