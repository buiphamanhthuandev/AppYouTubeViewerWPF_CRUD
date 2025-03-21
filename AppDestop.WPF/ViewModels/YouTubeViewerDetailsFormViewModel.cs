using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppDestop.WPF.ViewModels
{
    public class YoutubeViewerDetailsFormViewModel : ViewModelBase
    {
        private string? _username;
        public string UserName {
            get => _username!;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(UserName));
                OnPropertyChanged(nameof(CanSubmit));
            }
        }
        private bool _isSubscribed;
        public bool IsSubscribed
        {
            get => _isSubscribed;
            set
            {
                _isSubscribed = value;
                OnPropertyChanged(nameof(IsSubscribed));
            }
        }
        private bool _isMember;

        public bool IsMember
        {
            get => _isMember;
            set
            {
                _isMember = value;
                OnPropertyChanged(nameof(IsMember));
            }
        }

        private bool _isSubmitting;
        public bool IsSubmitting
        { 
            get => _isSubmitting;
            set
            {
                _isSubmitting = value;
                OnPropertyChanged(nameof(IsSubmitting));
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
                OnPropertyChanged(nameof(HasErrorMessage));
            }
        }

        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);



        public Guid Id {  get; set; }
        public bool CanSubmit => !string.IsNullOrEmpty(UserName);
        public ICommand? SubmitCommand { get; }
        public ICommand? CancelCommand { get; }

        public YoutubeViewerDetailsFormViewModel(ICommand? submitCommand, ICommand? cancelCommand)
        {
            SubmitCommand = submitCommand;
            CancelCommand = cancelCommand;
        }

    } 
}
