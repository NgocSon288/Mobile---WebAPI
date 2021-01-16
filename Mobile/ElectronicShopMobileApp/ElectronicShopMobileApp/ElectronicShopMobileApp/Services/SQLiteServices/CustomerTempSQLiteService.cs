using System;
using System.Linq;
using ElectronicShopMobileApp.Assets.Contains;
using ElectronicShopMobileApp.Models;
using ElectronicShopMobileApp.Models.SQLiteModels;
using SQLite;

namespace ElectronicShopMobileApp.Services.SQLiteServices
{
    public class CustomerTempSQLiteService
    {
        private static CustomerTempSQLiteService instance;

        public static CustomerTempSQLiteService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CustomerTempSQLiteService();
                }
                return instance;
            }
            private set => instance = value;
        }

        public CustomerTempSQLite Get()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(Const.FolderPath, Const.SQLiteDBContextPath)))
                {
                    return connection.Table<CustomerTempSQLite>().ToList().FirstOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Insert(Customer customer)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(Const.FolderPath, Const.SQLiteDBContextPath)))
                {
                    Delete();
                    connection.Insert(new CustomerTempSQLite(customer)); 

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(Const.FolderPath, Const.SQLiteDBContextPath)))
                {
                    connection.DeleteAll<CustomerTempSQLite>();

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
