using System;
using System.Collections.Generic;
using System.Text;

namespace ElectronicShopMobileApp.Models
{
    public class Customer
    {
        public string Address { get; set; }

        public string Avatar { get; set; }

        public string DisplayName { get; set; }

        public string Email { get; set; }

        public int ID { get; set; } 

        public bool IsRegister { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        public string Username { get; set; }

        public byte[] AvatarRegister { get; set; }
    }
}
