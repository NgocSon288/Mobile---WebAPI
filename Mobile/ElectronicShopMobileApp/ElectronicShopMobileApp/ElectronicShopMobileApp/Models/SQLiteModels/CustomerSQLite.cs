using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using SQLite;

namespace ElectronicShopMobileApp.Models.SQLiteModels
{
    public class CustomerSQLite : INotifyPropertyChanged
    {
        [PrimaryKey]
        public int ID { get; set; }

        public string Address { get; set; }

        public string Avatar { get; set; }

        public string DisplayName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        private bool isRememberPassword;

        public bool IsRememberPassword
        {
            get => isRememberPassword;
            set
            {
                isRememberPassword = value;
                OnPropertyChanged();
            }
        }

        private bool isRegister;

        public bool IsRegister
        {
            get => isRegister;
            set
            {
                isRegister = value;
                OnPropertyChanged();
            }
        }

        public CustomerSQLite()
        {

        }

        public CustomerSQLite(Customer customer)
        {
            Address = customer.Address;
            Avatar = customer.Avatar;
            DisplayName = customer.DisplayName;
            Email = customer.Email;
            ID = customer.ID;
            PhoneNumber = customer.PhoneNumber;
            Username = customer.Username;
            Password = customer.Password;
            IsRememberPassword = false;
            IsRegister = customer.IsRegister;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
