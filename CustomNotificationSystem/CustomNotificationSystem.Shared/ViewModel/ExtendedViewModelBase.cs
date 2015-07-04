using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using CustomNotificationSystem.Common;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

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
                return  _notificationText = string.Empty;
            }
            set
            {
                Set(() => NotificationText, ref _notificationText, value, true);
            }
        }


        







    }
}
