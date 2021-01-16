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
    public class CategoryController : ApiController
    {
        [Route("Api/CategoryController/GetAllCategory")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllCategory()
        {
            return Ok(await CategoryDAO.Instance.GetAllCategory());
        }
    }
}
