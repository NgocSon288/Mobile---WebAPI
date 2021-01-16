using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ElectronicShopMobileApp.Assets.Contains;
using SQLite;

namespace ElectronicShopMobileApp.Models.SQLiteModels
{
    public class AvatarCustomerSQLite
    {
        [PrimaryKey]
        public int CustomerID { get; set; }

        private byte[] avatar;

        public byte[] Avatar
        {
            get => avatar;
            set
            {
                avatar = value;
                OnPropertyChanged();
            }
        }

        public AvatarCustomerSQLite()
        {
            CustomerID = Const.CurrentCustomerID;
        }

        public AvatarCustomerSQLite(byte[] avatar)
        {
            CustomerID = Const.CurrentCustomerID;
            Avatar = avatar;
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
