using ElectronicShopMobileApp.Assets.Contains;
using ElectronicShopMobileApp.Models.SQLiteModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicShopMobileApp.Services.SQLiteServices
{
    public class CustomerSQLiteService
    {
        private static CustomerSQLiteService instance;

        public static CustomerSQLiteService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CustomerSQLiteService();
                }
                return instance;
            }
            private set => instance = value;
        }

        public List<CustomerSQLite> GetAll()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(Const.FolderPath, Const.SQLiteDBContextPath)))
                {
                    return connection.Table<CustomerSQLite>().ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public CustomerSQLite Find(int ID)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(Const.FolderPath, Const.SQLiteDBContextPath)))
                {
                    return connection.Table<CustomerSQLite>().FirstOrDefault(customerSQLite => customerSQLite.ID == ID);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Insert(CustomerSQLite customerSQLite)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(Const.FolderPath, Const.SQLiteDBContextPath)))
                {
                    connection.Insert(customerSQLite);

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(CustomerSQLite customerSQLite)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(Const.FolderPath, Const.SQLiteDBContextPath)))
                {
                    connection.Update(customerSQLite);

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(CustomerSQLite customerSQLite)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(Const.FolderPath, Const.SQLiteDBContextPath)))
                {
                    connection.Delete(customerSQLite);

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
