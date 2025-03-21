using AppDestop.WPF.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDestop.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        public ViewModelBase CurrentModal => _modalNavigationStore.CurrentViewModel;
        public bool IsModalOpen => _modalNavigationStore.IsOpen!;
        public YoutubeViewersViewModel? YoutubeViewersViewModel { get; set; }


        public MainViewModel(ModalNavigationStore modalNavigationStore, YoutubeViewersViewModel YoutubeViewersViewModel)
        {
            _modalNavigationStore = modalNavigationStore;
            this.YoutubeViewersViewModel = YoutubeViewersViewModel;

            _modalNavigationStore.CurrentViewModelChanged += ModalNavigationStore_CurrentViewModelChanged;
        }

        protected override void Dispose()
        {
            _modalNavigationStore.CurrentViewModelChanged -= ModalNavigationStore_CurrentViewModelChanged;
            base.Dispose();
        }

        private void ModalNavigationStore_CurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentModal));
            OnPropertyChanged(nameof(IsModalOpen)!);
        }
    }
}
