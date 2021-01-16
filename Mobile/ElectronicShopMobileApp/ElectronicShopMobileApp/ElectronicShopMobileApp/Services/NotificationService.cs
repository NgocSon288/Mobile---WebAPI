using ElectronicShopMobileApp.Assets.Contains;
using ElectronicShopMobileApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicShopMobileApp.Services
{
    public class NotificationService
    {
        private static NotificationService instance;

        public static NotificationService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NotificationService();
                }
                return instance;
            }
            private set => instance = value;
        }

        public async Task<List<Notification>> GetAllNotification()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var dataString = await client.GetStringAsync(Const.ConverToPathWithParameter(Const.GetAllNotificationByCustomerIDPath, new object[] { Const.CurrentCustomerID }));

                    var notificationList = JsonConvert.DeserializeObject<List<Notification>>(dataString);

                    return notificationList;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}
