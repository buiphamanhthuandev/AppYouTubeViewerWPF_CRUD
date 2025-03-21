using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YouTubeViewer.Domain.Models;
using YouTubeViewer.EntityFramework.DTOs;

namespace YouTubeViewer.EntityFramework
{
    public class YouTubeViewersDbContext : DbContext
    {
        public YouTubeViewersDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<YouTubeViewDto> YouTubeViews { get; set; }
    }
}
