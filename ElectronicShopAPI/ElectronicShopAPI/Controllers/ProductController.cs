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
    public class ProductController : ApiController
    {  
        [Route("Api/ProductController/GetAllProduct")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllProduct()
        { 
            return Ok(await ProductDAO.Instance.GetAllProduct());
        }

        [Route("Api/ProductController/GetAllProductByCategoryID/{categoryID}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllProductByCategoryID(int categoryID)
        {
            return Ok(await ProductDAO.Instance.GetAllProductByCategoryID(categoryID));
        }

        [Route("Api/ProductController/GetAllProductMostSearched")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllProductMostSearched()
        {
            return Ok(await ProductDAO.Instance.GetAllProductMostSearched());
        }

        [Route("Api/ProductController/GetAllProductMostDiscount")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllProductMostDiscount()
        {
            return Ok(await ProductDAO.Instance.GetAllProductMostDiscount());
        }

        [Route("Api/ProductController/GetAllProductByID/{ID}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllProductByID(int ID)
        {
            return Ok(await ProductDAO.Instance.GetAllProductByID(ID));
        }

        [Route("Api/ProductController/IncreaseProductViewByID/{ID}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> IncreaseProductViewByIDGet(int ID)
        {
            return Ok(await ProductDAO.Instance.IncreaseProductViewByID(ID));
        }
    }
}
