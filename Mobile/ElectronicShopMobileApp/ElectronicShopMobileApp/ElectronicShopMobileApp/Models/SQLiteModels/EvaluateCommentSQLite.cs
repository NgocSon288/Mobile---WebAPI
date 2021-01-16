using System;
using SQLite;
using ElectronicShopMobileApp.Assets.Contains;

namespace ElectronicShopMobileApp.Models.SQLiteModels
{
    public class EvaluateCommentSQLite
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public int CommentID { get; set; }

        public int CustomerID { get; set; }

        public bool IsLike { get; set; }

        public int ProductID { get; set; }

        public EvaluateCommentSQLite()
        {

        }

        public EvaluateCommentSQLite(int commentID, bool isLike, int productID)
        {
            CommentID = commentID;
            CustomerID = Const.CurrentCustomerID;
            IsLike = isLike;
            ProductID = productID;
        }
    }
}
