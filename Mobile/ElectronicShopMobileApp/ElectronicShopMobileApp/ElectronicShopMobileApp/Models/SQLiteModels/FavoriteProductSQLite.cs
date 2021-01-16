using System;
using System.Collections.Generic;
using System.Text;
using ElectronicShopMobileApp.Assets.Contains;
using SQLite;

namespace ElectronicShopMobileApp.Models.SQLiteModels
{
    public class FavoriteProductSQLite
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public int ProductID { get; set; }

        public int CustomerID { get; set; }

        public FavoriteProductSQLite()
        {
            
        }

        public FavoriteProductSQLite(int productID)
        {
            ProductID = productID;
            CustomerID = Const.CurrentCustomerID;
        }
    }
}

