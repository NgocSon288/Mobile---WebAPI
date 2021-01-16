using ElectronicShopAPI.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectronicShopAPI.Models.DTO
{
    public class NotificationDTO
    {  
        public string Title { get; set; }

        public DateTime? CreationTime { get; set; } 
         
        public string Description { get; set; }

        public NotificationDTO()
        {

        }

        public NotificationDTO(Notification notification)
        {
            Title = notification.Title;
            CreationTime = notification.CreationTime;
            Description = notification.Description;
        }
    }
}