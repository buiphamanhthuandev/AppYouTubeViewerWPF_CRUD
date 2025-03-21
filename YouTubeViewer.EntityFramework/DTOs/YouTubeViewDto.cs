using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTubeViewer.EntityFramework.DTOs
{
    public class YouTubeViewDto
    {
        public Guid Id { get; set; }
        public string? UserName { get; set; }
        public bool IsSubscribed { get; set; }
        public bool IsMember { get; set; }
    }
}
