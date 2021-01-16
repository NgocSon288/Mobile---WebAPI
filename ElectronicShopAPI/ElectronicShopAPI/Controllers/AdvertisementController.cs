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
    public class AdvertisementController : ApiController
    {
        [Route("Api/AdvertisementController/GetAllAdvertisement")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllAdvertisement()
        {
            return Ok(await AdvertisementDAO.Instance.GetAllAdvertisement());
        }
    }
}
