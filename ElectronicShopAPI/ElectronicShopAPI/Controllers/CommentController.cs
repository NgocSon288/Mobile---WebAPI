using ElectronicShopAPI.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ElectronicShopAPI.Controllers
{
    public class CommentController : ApiController
    {
        [Route("Api/CommentController/GetCommentByProductID/{productID}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetCommentByProductID(int productID)
        {
            return Ok(await CommentDAO.Instance.GetCommentByProductID(productID));
        }

        [Route("Api/CommentController/CreateComment")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> CreateComment(int customerID, int productID, int startNumber, string reason, string description)
        {
            return Ok(await CommentDAO.Instance.CreateComment(customerID, productID, startNumber, reason, description));
        } 

        [Route("Api/CommentController/IncreaseOrDecreaseLikeOrDisLikeGet/{commentID}/{isIncrease}/{isLike}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> IncreaseOrDecreaseLikeOrDisLikeGet(int commentID, bool isIncrease, bool isLike)
        {
            return Ok(await CommentDAO.Instance.IncreaseOrDecreaseLikeOrDisLike(commentID, isIncrease, isLike));
        }
    }
}
