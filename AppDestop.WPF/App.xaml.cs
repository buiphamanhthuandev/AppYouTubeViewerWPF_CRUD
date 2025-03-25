using AppDestop.WPF.HostBuilders;
using AppDestop.WPF.Stores;
using AppDestop.WPF.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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

        private readonly IHost _host;
        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .AddDbContext()
                .ConfigureServices((context, services) =>
            {
                services.AddSingleton<IGetAllYouTubeViewerQuery, GetAllYouTubeViewerQuery>();
                services.AddSingleton<ICreateYouTubeViewerCommand, CreateYouTubeViewerCommand>();
                services.AddSingleton<IUpdateYouTubeViewerCommand, UpdateYouTubeViewerCommand>();
                services.AddSingleton<IDeleteYouTubeViewerCommand, DeleteYouTubeViewerCommand>();

                services.AddSingleton<ModalNavigationStore>();
                services.AddSingleton<YoutubeViewersStore>();
                services.AddSingleton<SeletedYoutubeViewersStore>();

                services.AddTransient<YoutubeViewersViewModel>(CreateYouTubeViewersViewModel);
                services.AddSingleton<MainViewModel>();
                services.AddSingleton<MainWindow>((services) => new WPF.MainWindow()
                {
                    DataContext = services.GetRequiredService<MainViewModel>()
                });

            }).Build(); 

        }
        protected override void OnStartup(StartupEventArgs e) 
        {
            _host.Start();
            YouTubeViewersDbContextFactory youTubeViewersDbContextFactory = _host.Services.GetRequiredService<YouTubeViewersDbContextFactory>();


            using (YouTubeViewersDbContext context = youTubeViewersDbContextFactory.Create())
            {
                context.Database.Migrate();
                if (!context.YouTubeViews.Any()) // Kiểm tra nếu chưa có dữ liệu
                {
                    context.YouTubeViews.Add(new YouTubeViewDto { Id = Guid.NewGuid(), UserName = "Test Viewer" ,IsSubscribed = true, IsMember = true});
                    context.SaveChanges();
                }
                
            }


            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }
        protected override void OnExit(ExitEventArgs e)
        {
            _host.StopAsync();
            _host.Dispose();

            base.OnExit(e);
        }
        private YoutubeViewersViewModel CreateYouTubeViewersViewModel(IServiceProvider services)
        {
            return YoutubeViewersViewModel.LoadViewModel(
                services.GetRequiredService<YoutubeViewersStore>(),
                services.GetRequiredService<SeletedYoutubeViewersStore>(),
                services.GetRequiredService<ModalNavigationStore>());
        }
    }

}
