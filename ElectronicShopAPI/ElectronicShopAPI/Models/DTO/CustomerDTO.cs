using ElectronicShopAPI.Assets.Contains;
using ElectronicShopAPI.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectronicShopAPI.Models.DTO
{
    public class CustomerDTO
    {
        public int ID { get; set; } 
         
        public string DisplayName { get; set; }
         
        public string PhoneNumber { get; set; }
         
        public string Email { get; set; }
         
        public string Address { get; set; }
         
        public string Avatar { get; set; }

        public string Username { get; set; }

        public string Password { get; set; } 

        public bool IsRegister { get; set; }

        public CustomerDTO()
        {

        }

        public CustomerDTO(Customer customer)
        {
            ID = customer.ID;
            DisplayName = customer.DisplayName;
            PhoneNumber = customer.PhoneNumber;
            Email = customer.Email;
            Address = customer.Address;
            Avatar = Const.CustomerImagePath + customer.Avatar;
            Username = customer.UserName;
            Password = customer.PassWord; 
            IsRegister = customer.IsRegister.Value;
        }
    }
}