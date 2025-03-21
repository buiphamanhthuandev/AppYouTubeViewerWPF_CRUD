using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTubeViewer.Domain.Models
{
    public class YouTubeView
    {
        public Guid Id { get; set; }
        public string? UserName { get; set; }
        public bool IsSubscribed { get; set; }
        public bool IsMember { get; set; }
        public YouTubeView(Guid id, string username, bool isSubscribed, bool isMember)
        {
            Id = id;
            UserName = username;
            IsSubscribed = isSubscribed;
            IsMember = isMember;
        }
    }
}
