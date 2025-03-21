﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTubeViewer.EntityFramework
{
    public class YouTubeViewersDbContextFactory
    {
        private readonly DbContextOptions _options;

        public YouTubeViewersDbContextFactory(DbContextOptions options)
        {
           _options = options;
        }

        public YouTubeViewersDbContext Create()
        {
            
            return new YouTubeViewersDbContext(_options);
        }
    }
}
