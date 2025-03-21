using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTubeViewer.EntityFramework
{
    public class YouTubeViewerDesignTimeDbContextFactory : IDesignTimeDbContextFactory<YouTubeViewersDbContext>
    {
        string connectionString = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "YouTubeViewers.db");
        public YouTubeViewersDbContext CreateDbContext(string[] args = null)
        {
            return new YouTubeViewersDbContext(new DbContextOptionsBuilder().UseSqlite($"Data Source = {connectionString}").Options);
        }
    }
}
