using ElectronicShopAPI.Models.DTO;
using ElectronicShopAPI.Models.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ElectronicShopAPI.Models.DAO
{
    public class CommentDAO
    {
        private static CommentDAO instance;

        public static CommentDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CommentDAO();
                }
                return instance;
            }
            private set => instance = value;
        }

        ElectronicDBContext db = new ElectronicDBContext();

        public async Task<List<CommentDTO>> GetCommentByProductID(int productID)
        {
            return (await db.Comments
                        .Where(comment => comment.ProductID == productID)
                        .ToListAsync())
                        .Select(comment => new CommentDTO(comment))
                        .ToList();
        }

        public async Task<int> CreateComment(int customerID, int productID, int startNumber, string reason, string description)
        {
            var comment = new Comment()
            {
                CustomerID = customerID,
                ProductID = productID,
                LikeNumber = 0,
                DisLikeNumber = 0,
                StarNumber = startNumber,
                Reason = reason,
                Description = description
            };

            db.Comments.Add(comment);
            await db.SaveChangesAsync();

            return comment.ID;
        }

        public async Task<int?> IncreaseOrDecreaseLikeOrDisLike(int commentID, bool isIncrease, bool isLike)
        {
            var myComment = await db.Comments.SingleOrDefaultAsync(comment => comment.ID == commentID);

            if (isLike)
            {
                if (isIncrease)
                {
                    myComment.LikeNumber++;
                }
                else
                {
                    myComment.LikeNumber--;
                }
            }
            else
            {
                if (isIncrease)
                {
                    myComment.DisLikeNumber++;
                }
                else
                {
                    myComment.DisLikeNumber--;
                }
            }

            await db.SaveChangesAsync();

            return isLike ? myComment.LikeNumber : myComment.DisLikeNumber;
        }
    }
}