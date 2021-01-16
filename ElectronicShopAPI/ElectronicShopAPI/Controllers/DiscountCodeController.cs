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
    public class DiscountCodeController : ApiController
    {
        [Route("Api/DiscountCodeController/IsCorrectDiscountCode/{code}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> IsCorrectDiscountCode(string code)
        {
            return Ok(await DiscountCodeDAO.Instance.IsCorrectDiscountCode(code));
        }
    }
}
