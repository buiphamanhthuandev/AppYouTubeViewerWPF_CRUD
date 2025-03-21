using AppDestop.WPF.Stores;
using AppDestop.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDestop.WPF.Commands
{
    public class LoadYouTubeViewersCommand : AsyncCommandBase
    {
        private readonly YoutubeViewersViewModel _youtubeViewersViewModel;
        private readonly YoutubeViewersStore _youtubeViewersStore;

        public LoadYouTubeViewersCommand(YoutubeViewersViewModel youtubeViewersViewModel, YoutubeViewersStore youtubeViewersStore)
        {
            _youtubeViewersViewModel = youtubeViewersViewModel;
            _youtubeViewersStore = youtubeViewersStore;
        }
        public override async Task ExecuteAsync(object? parameter)
        {
            _youtubeViewersViewModel.ErrorMessage = null;
            _youtubeViewersViewModel.IsLoading = true; 
            try
            {
                await _youtubeViewersStore.Load();

            }
            catch (Exception ex)
            {
                _youtubeViewersViewModel.ErrorMessage = "Failed to load YouTube viewers. Please restart the application.";
            }
            finally
            {
                _youtubeViewersViewModel.IsLoading = false;
            }
            
        }
    }
}
