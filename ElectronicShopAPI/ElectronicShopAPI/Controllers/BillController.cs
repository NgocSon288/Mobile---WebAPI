using ElectronicShopAPI.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Http;

namespace ElectronicShopAPI.Controllers
{
    public class BillController : ApiController
    {
        [Route("Api/BillController/SayHello")]
        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult SayHello()
        {
            return Ok("Hello world");
        }

        [Route("Api/BillController/GetAllBillByCustomerID/{customerID}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllBillByCustomerID(int customerID)
        {
            return Ok(await BillDAO.Instance.GetAllBillByCustomerID(customerID));
        }
         
        [Route("Api/BillController/CreateBill/{customerID}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> CreateBill(int customerID)
        {
            return Ok(await BillDAO.Instance.CreateBill(customerID));
        }  
    }
}
