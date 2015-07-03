using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using CustomNotificationSystem.Common;
using GalaSoft.MvvmLight;

namespace CustomNotificationSystem.ViewModel
{
    public class ExtendedViewModelBase : ViewModelBase
    {
        public ExtendedViewModelBase()
        {
            App.GlobalNotificationDispatcherTimer.Tick += GlobalNotificationDispatcherTimerOnTick;
        }

        private void GlobalNotificationDispatcherTimerOnTick(object sender, object o)
        {
            if (_secondsElapsed < 5)
            {
                _secondsElapsed ++;
            }
            else
            {
                App.GlobalNotificationDispatcherTimer.Stop();
                _secondsElapsed = 0;
                NotificationText= string.Empty;
            }
        }

        private int _secondsElapsed;



        /// <summary>
        /// The <see cref="NotificationText" /> property's name.
        /// </summary>
        public const string NotificationTextPropertyName = "NotificationText";

        private string _notificationText = string.Empty;

        /// <summary>
        /// Sets and gets the NotificationText property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public string NotificationText
        {
            get { return _notificationText; }

            set
            {
                if (_notificationText == value)
                {
                    return;
                }

                if (value != string.Empty)
                {
                    _secondsElapsed = 0;

                    if (!App.GlobalNotificationDispatcherTimer.IsEnabled)
                    {
                        App.GlobalNotificationDispatcherTimer.Start();
                    }
                }

                var oldValue = _notificationText;
                _notificationText = value;

                RaisePropertyChanged(() => NotificationText, oldValue, value, true);



                
            }
        }


        







    }
}
