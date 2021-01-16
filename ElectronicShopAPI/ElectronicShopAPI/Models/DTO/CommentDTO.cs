using ElectronicShopAPI.Assets.Contains;
using ElectronicShopAPI.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectronicShopAPI.Models.DTO
{
    public class CommentDTO
    {   
        public int? ID { get; set; }

        public int? LikeNumber { get; set; }

        public int? DisLikeNumber { get; set; }

        public int? StarNumber { get; set; }
         
        public string Reason { get; set; }
         
        public string Description { get; set; }

        public string CustomerName { get; set; }

        public string AvatarCustomer { get; set; }

        public CommentDTO()
        {

        }

        public CommentDTO(Comment comment)
        {
            ID = comment.ID;
            LikeNumber = comment.LikeNumber;
            DisLikeNumber = comment.DisLikeNumber;
            StarNumber = comment.StarNumber;
            Reason = comment.Reason;
            Description = comment.Description;
            CustomerName = comment.Customer.DisplayName;
            AvatarCustomer = Const.CustomerImagePath + comment.Customer.Avatar;
        }
    }
}