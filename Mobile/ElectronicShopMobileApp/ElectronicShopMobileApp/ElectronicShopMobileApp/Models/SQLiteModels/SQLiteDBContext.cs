using ElectronicShopMobileApp.Assets.Contains;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElectronicShopMobileApp.Models.SQLiteModels
{
    public class SQLiteDBContext
    {  
        public bool CreateDatabase()
        {
            try
            {
                using(var connection = new SQLiteConnection(System.IO.Path.Combine(Const.FolderPath, Const.SQLiteDBContextPath)))
                { 
                    connection.CreateTable<CustomerSQLite>();
                    connection.CreateTable<CartItemSQLite>();
                    connection.CreateTable<FavoriteProductSQLite>();
                    connection.CreateTable<EvaluateCommentSQLite>();
                    connection.CreateTable<CustomerTempSQLite>();
                    connection.CreateTable<AvatarCustomerSQLite>();

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
