using System;
using System.Collections.Generic;
using System.Linq;
using ElectronicShopMobileApp.Assets.Contains;
using ElectronicShopMobileApp.Models;
using ElectronicShopMobileApp.Models.SQLiteModels;
using SQLite;

namespace ElectronicShopMobileApp.Services.SQLiteServices
{
    public class EvaluateCommentSQLiteService
    {
        private static EvaluateCommentSQLiteService instance;

        public static EvaluateCommentSQLiteService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EvaluateCommentSQLiteService();
                }
                return instance;
            }
            private set => instance = value;
        }

        public EvaluateCommentSQLite Find(int commentID, bool isLike)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(Const.FolderPath, Const.SQLiteDBContextPath)))
                {
                    return connection.Table<EvaluateCommentSQLite>().FirstOrDefault(evaCom => evaCom.CommentID == commentID && evaCom.CustomerID == Const.CurrentCustomerID && evaCom.IsLike == isLike);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 1 = IncreaseLike
        /// 2 = IncreaseDisLike
        /// 3 = IncreaseLike, DecreaseDisLike
        /// 4 = DecreaseDisLike
        /// 5 = DecreaseLike
        /// 6 = IncreaseDisLike, DecreaseLike
        /// 0 = Error
        /// </summary>
        /// <param name="commentID"></param>
        /// <param name="isLike"></param>
        /// <returns></returns>
        public int InsertOrDelete(int commentID, bool isLike, int productID)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(Const.FolderPath, Const.SQLiteDBContextPath)))
                {
                    var evaComLike = Find(commentID, true);
                    var evaComDisLike = Find(commentID, false);

                    if(evaComLike == null && evaComDisLike == null)
                    {
                        if (isLike)
                        {
                            connection.Insert(new EvaluateCommentSQLite(commentID, true, productID));
                            return 1;
                        }
                        else
                        {
                            connection.Insert(new EvaluateCommentSQLite(commentID, false, productID));
                            return 2;
                        }
                    }
                    else if(evaComLike == null && evaComDisLike != null)
                    {
                        if (isLike)
                        {
                            connection.Insert(new EvaluateCommentSQLite(commentID, true, productID));
                            connection.Delete(evaComDisLike);
                            return 3;
                        }
                        else
                        {
                            connection.Delete(evaComDisLike);
                            return 4;
                        }
                    }
                    else if(evaComLike!=null && evaComDisLike == null)
                    {
                        if (isLike)
                        {
                            connection.Delete(evaComLike);
                            return 5;
                        }
                        else
                        {
                            connection.Insert(new EvaluateCommentSQLite(commentID, false, productID));
                            connection.Delete(evaComLike);
                            return 6;
                        }
                    }
                    return 0;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public void LoadCommentColorState(List<Comment> comments, int productID)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(Const.FolderPath, Const.SQLiteDBContextPath)))
                {
                    var result = connection.Table<EvaluateCommentSQLite>().Where(evaCom => evaCom.ProductID == productID && evaCom.CustomerID == Const.CurrentCustomerID).ToList();

                    foreach(var comment in comments)
                    {
                        var evaCom = result.SingleOrDefault(item => item.CommentID == comment.ID);
                        if (evaCom != null)
                        {
                            comment.IsLike = evaCom.IsLike;
                        }
                        else
                        {
                            comment.IsLike = null;
                        }
                    } 
                }
            }
            catch (Exception)
            {
                return;
            }
        } 
    }
}
