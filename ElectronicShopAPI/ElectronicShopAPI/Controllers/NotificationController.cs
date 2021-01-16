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
    public class NotificationController : ApiController
    {
        [Route("Api/NotificationController/GetAllNotificationByCustomerID/{customerID}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllNotificationByCustomerID(int customerID)
        {
            return Ok(await NotificationDAO.Instance.GetAllNotificationByCustomerID(customerID));
        }
    }
}
