using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace CustomNotificationSystem.ViewModel
{
    public class ExtendedViewModelBase : ViewModelBase
    {
        public ExtendedViewModelBase()
        {
            
        }

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
            get
            {
                return _notificationText;
            }

            set
            {
                if (_notificationText == value)
                {
                    return;
                }

                var oldValue = _notificationText;
                _notificationText = value;

                RaisePropertyChanged(() => NotificationText, oldValue, value, true);

            }
        }





        public async void ClearNotificationText()
        {
            await Task.Delay(5000);
            NotificationText = string.Empty;
        }


    }
}
