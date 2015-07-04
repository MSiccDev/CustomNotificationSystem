using System;
using Windows.UI.Xaml;
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
            _notificationTimer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 1) };
            _notificationTimer.Tick += _notificationTimer_Tick;

            //register for the global NotificationText PropertyChangedMessage from all VMs that derive from ExtendenViewModelBase
            Messenger.Default.Register<PropertyChangedMessage<string>>(this, message =>
            {
                if (message.PropertyName == ExtendedViewModelBase.NotificationTextPropertyName)
                {
                    if (!_notificationTimer.IsEnabled)
                    {
                        _notificationTimer.Start();
                    }
                    else
                    {
                        _notificationTimerElapsedSeconds = 0;
                    }

                    GlobalNotificationText = message.NewValue;
                }
            });
        }

        private void _notificationTimer_Tick(object sender, object e)
        {
            _notificationTimerElapsedSeconds++;

            if (_notificationTimerElapsedSeconds > 5)
            {
                _notificationTimer.Stop();
                _notificationTimerElapsedSeconds = 0;
                GlobalNotificationText = string.Empty;
            }
        }

        private readonly DispatcherTimer _notificationTimer;
        private int _notificationTimerElapsedSeconds;

        private int ct;

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
                        
                        NotificationText = "This is a test notification via global property on MainViewModel, set via MVVM Light Messenger   " + ct++;

                    },
                    () => true));
            }
        }



        /// <summary>
        /// The <see cref="GlobalNotificationText" /> property's name.
        /// </summary>
        public const string GlobalNotificationTextPropertyName = "GlobalNotificationText";

        private string _globalNotificationText = string.Empty;

        /// <summary>
        /// Sets and gets the GlobalNotificationText property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string GlobalNotificationText
        {
            get
            {
                return _globalNotificationText;
            }
            set
            {
                Set(() => GlobalNotificationText, ref _globalNotificationText, value);
            }
        }
    }
}