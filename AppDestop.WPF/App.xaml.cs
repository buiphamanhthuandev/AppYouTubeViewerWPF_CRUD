using AppDestop.WPF.Stores;
using AppDestop.WPF.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows;
using YouTubeViewer.Domain.Commands;
using YouTubeViewer.Domain.Queries;
using YouTubeViewer.EntityFramework;
using YouTubeViewer.EntityFramework.Commands;
using YouTubeViewer.EntityFramework.DTOs;
using YouTubeViewer.EntityFramework.Queries;


namespace AppDestop.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ModalNavigationStore _modalNavigationStore;

        private readonly YouTubeViewersDbContextFactory _youTubeViewersDbContextFactory;

        private readonly IGetAllYouTubeViewerQuery _getAllYouTubeViewerQuery;
        private readonly ICreateYouTubeViewerCommand _createYouTubeViewerCommand;
        private readonly IUpdateYouTubeViewerCommand _updateYouTubeViewerCommand;
        private readonly IDeleteYouTubeViewerCommand _deleteYouTubeViewerCommand;

        

        
        private readonly YoutubeViewersStore _YoutubeViewersStore;
        private readonly SeletedYoutubeViewersStore _seletedYoutubeViewersStore;
        public App()
        {
            _modalNavigationStore = new ModalNavigationStore();
            string connectionString = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "YouTubeViewers.db");
            Console.WriteLine($"Database Path: {connectionString}");
            _youTubeViewersDbContextFactory = new YouTubeViewersDbContextFactory(
                new DbContextOptionsBuilder().UseSqlite($"Data Source = {connectionString}").Options);

            _getAllYouTubeViewerQuery = new GetAllYouTubeViewerQuery(_youTubeViewersDbContextFactory);
            _createYouTubeViewerCommand = new CreateYouTubeViewerCommand(_youTubeViewersDbContextFactory);
            _updateYouTubeViewerCommand = new UpdateYouTubeViewerCommand(_youTubeViewersDbContextFactory);
            _deleteYouTubeViewerCommand = new DeleteYouTubeViewerCommand(_youTubeViewersDbContextFactory);

            
            _YoutubeViewersStore = new YoutubeViewersStore(_getAllYouTubeViewerQuery, _createYouTubeViewerCommand, _updateYouTubeViewerCommand, _deleteYouTubeViewerCommand);
            _seletedYoutubeViewersStore = new SeletedYoutubeViewersStore(_YoutubeViewersStore);
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            using (YouTubeViewersDbContext context = _youTubeViewersDbContextFactory.Create())
            {
                context.Database.Migrate();
                if (!context.YouTubeViews.Any()) // Kiểm tra nếu chưa có dữ liệu
                {
                    context.YouTubeViews.Add(new YouTubeViewDto { Id = Guid.NewGuid(), UserName = "Test Viewer" ,IsSubscribed = true, IsMember = true});
                    context.SaveChanges();
                }
                
            }
            YoutubeViewersViewModel YoutubeViewersViewModel = YoutubeViewersViewModel.LoadViewModel(_YoutubeViewersStore!, _seletedYoutubeViewersStore, _modalNavigationStore);
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_modalNavigationStore, YoutubeViewersViewModel)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }
    }

}
