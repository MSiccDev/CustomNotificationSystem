using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace CustomNotificationSystem.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ExtendedViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
        }



        private RelayCommand _showNotificationCommand;

        /// <summary>
        /// Gets the MyCommand.
        /// </summary>
        public RelayCommand ShowNotificationCommand
        {
            get
            {
                return _showNotificationCommand
                    ?? (_showNotificationCommand = new RelayCommand(
                    () =>
                    {
                        if (!ShowNotificationCommand.CanExecute(null))
                        {
                            return;
                        }

                        NotificationText = "This is a test notification";
                        ClearNotificationText();

                    },
                    () => true));
            }
        }
    }
}