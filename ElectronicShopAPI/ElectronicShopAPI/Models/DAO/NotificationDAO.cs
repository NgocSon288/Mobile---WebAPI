using ElectronicShopAPI.Models.DTO;
using ElectronicShopAPI.Models.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ElectronicShopAPI.Models.DAO
{
    public class NotificationDAO
    {
        private static NotificationDAO instance;

        public static NotificationDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NotificationDAO();
                }
                return instance;
            }
            private set => instance = value;
        }

        ElectronicDBContext db = new ElectronicDBContext();

        public async Task<List<NotificationDTO>> GetAllNotificationByCustomerID(int customerID)
        {
            return (await db.Notifications
                        .Where(notification => notification.CustomerID == customerID)
                        .ToListAsync())
                        .Select(notificaton => new NotificationDTO(notificaton))
                        .ToList();
        }
    }
}