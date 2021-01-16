using ElectronicShopMobileApp.Models;
using ElectronicShopMobileApp.Services;
using ElectronicShopMobileApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace ElectronicShopMobileApp.ViewModels
{
    public class NotificationViewModel:BaseViewModel
    {
        private ObservableCollection<Notification> notifications;

        public ObservableCollection<Notification> Notifications
        {
            get => notifications;
            set
            {
                notifications = value;
                OnPropertyChanged();
            }
        }

        public NotificationViewModel()
        {
            LoadData();
        }

        async private void LoadData()
        {
            Notifications = new ObservableCollection<Notification>(await NotificationService.Instance.GetAllNotification());
        }
    }
}
