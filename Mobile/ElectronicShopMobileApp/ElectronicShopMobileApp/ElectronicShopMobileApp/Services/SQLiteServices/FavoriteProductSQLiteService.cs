using ElectronicShopMobileApp.Assets.Contains;
using ElectronicShopMobileApp.Models.SQLiteModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicShopMobileApp.Services.SQLiteServices
{
    public class FavoriteProductSQLiteService
    {
        private static FavoriteProductSQLiteService instance;

        public static FavoriteProductSQLiteService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FavoriteProductSQLiteService();
                }
                return instance;
            }
            private set => instance = value;
        }

        public bool GetStateExists(int productID)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(Const.FolderPath, Const.SQLiteDBContextPath)))
                {
                    return GetFavoriteProductByProductid(productID) != null;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool SetStateExists(int productID)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(Const.FolderPath, Const.SQLiteDBContextPath)))
                {
                    if (!GetStateExists(productID))
                    {
                        connection.Insert(new FavoriteProductSQLite(productID));
                    }
                    else
                    {
                        connection.Delete(GetFavoriteProductByProductid(productID));
                    }
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public FavoriteProductSQLite GetFavoriteProductByProductid(int productID)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(Const.FolderPath, Const.SQLiteDBContextPath)))
                {
                    return connection.Table<FavoriteProductSQLite>()
                                        .SingleOrDefault(favoriteProductSQLite => favoriteProductSQLite.CustomerID == Const.CurrentCustomerID && favoriteProductSQLite.ProductID == productID);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
