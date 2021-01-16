using ElectronicShopMobileApp.Assets.Contains;
using ElectronicShopMobileApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicShopMobileApp.Services
{
    public class CommentService
    {
        private static CommentService instance;

        public static CommentService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CommentService();
                }
                return instance;
            }
            private set => instance = value;
        }

        public async Task<List<Comment>> GetAllCommentByProductID(int productID)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var dataString = await client.GetStringAsync(Const.ConverToPathWithParameter(Const.GetCommentByProductIDPath, new object[] { productID}));

                    var commentList = JsonConvert.DeserializeObject<List<Comment>>(dataString);

                    return commentList;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public async Task IncreaseOrDecreaseLikeOrDisLikePath(int commentID, bool isIncrease, bool isLike)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var dataString = await client.GetStringAsync(Const.ConverToPathWithParameter(Const.IncreaseOrDecreaseLikeOrDisLikePath, new object[] { commentID, isIncrease, isLike }));

                    var result = JsonConvert.DeserializeObject<object>(dataString); 
                }
                catch (Exception)
                {
                    return;
                }
            }
        }
    }
}
