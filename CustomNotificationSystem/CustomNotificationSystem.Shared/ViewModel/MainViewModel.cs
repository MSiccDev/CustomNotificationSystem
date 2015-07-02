using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

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
            
            //if you want to collect the notification text in one centralized, use the MVVMLight Messenger
            //this enables you to show the Notification also after a navigation has taken place
            Messenger.Default.Register<PropertyChangedMessage<string>>(this, message =>
            {
                if (message.PropertyName == ExtendedViewModelBase.NotificationTextPropertyName)
                {
                    GlobalNotificationTextProperty = message.NewValue;
                }
            });
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

                        //if you need the notification only on one page, bind directly to NotificationText
                        //NotificationText = "This is a test notification via ExtendedViewModelBase";

                        
                        NotificationText = "This is a test notification via global property on MainViewModel, set via MVVM Light Messenger";

                        ClearNotificationText();

                    },
                    () => true));
            }
        }



        /// <summary>
        /// The <see cref="GlobalNotificationTextProperty" /> property's name.
        /// </summary>
        public const string GlobalNotificationTextPropertyPropertyName = "GlobalNotificationTextProperty";

        private string _globalNotificationTextProperty = string.Empty;

        /// <summary>
        /// Sets and gets the GlobalNotificationTextProperty property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string GlobalNotificationTextProperty
        {
            get
            {
                return _globalNotificationTextProperty;
            }
            set
            {
                Set(() => GlobalNotificationTextProperty, ref _globalNotificationTextProperty, value);
            }
        }
    }
}