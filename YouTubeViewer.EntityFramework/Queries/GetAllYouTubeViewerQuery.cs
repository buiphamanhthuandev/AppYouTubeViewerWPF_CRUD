using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouTubeViewer.Domain.Models;
using YouTubeViewer.Domain.Queries;
using YouTubeViewer.EntityFramework.DTOs;

namespace YouTubeViewer.EntityFramework.Queries
{
    public class GetAllYouTubeViewerQuery : IGetAllYouTubeViewerQuery
    {
        private readonly YouTubeViewersDbContextFactory _contextFactory;
        public GetAllYouTubeViewerQuery(YouTubeViewersDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<YouTubeView>> Execute()
        {
            using (YouTubeViewersDbContext context = _contextFactory.Create())
            {
                var youTubeViewers = await context.YouTubeViews.ToListAsync();
                foreach (var viewer in youTubeViewers)
                {
                    Debug.WriteLine($"ID: {viewer.Id}, Name: {viewer.UserName}, Subscribed: {viewer.IsSubscribed}, Member: {viewer.IsMember}");
                }
                return youTubeViewers.Select(y => new YouTubeView(y.Id, y.UserName!, y.IsSubscribed, y.IsMember));
            }
        }
    }
}
