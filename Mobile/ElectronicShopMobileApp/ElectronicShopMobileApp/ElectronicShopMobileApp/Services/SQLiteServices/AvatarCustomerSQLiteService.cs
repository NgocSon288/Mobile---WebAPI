using System;
using System.Linq;
using ElectronicShopMobileApp.Assets.Contains;
using ElectronicShopMobileApp.Models.SQLiteModels;
using SQLite;

namespace ElectronicShopMobileApp.Services.SQLiteServices
{
    public class AvatarCustomerSQLiteService
    {
        private static AvatarCustomerSQLiteService instance;

        public static AvatarCustomerSQLiteService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AvatarCustomerSQLiteService();
                }
                return instance;
            }
            private set => instance = value;
        }

        public AvatarCustomerSQLite Get()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(Const.FolderPath, Const.SQLiteDBContextPath)))
                {
                    var x = Const.CurrentCustomerID;
                    var y = connection.Table<AvatarCustomerSQLite>().ToList();
                    return connection.Table<AvatarCustomerSQLite>().ToList().FirstOrDefault(avatar=>avatar.CustomerID==Const.CurrentCustomerID);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool InsertOrUpdate(AvatarCustomerSQLite avatarCustomer)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(Const.FolderPath, Const.SQLiteDBContextPath)))
                {
                    var avatar = Get();
                    if(avatar != null)
                    {
                        connection.Update(avatarCustomer);
                    }
                    else
                    {
                        connection.Insert(avatarCustomer);
                    }

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        } 
    }
}
