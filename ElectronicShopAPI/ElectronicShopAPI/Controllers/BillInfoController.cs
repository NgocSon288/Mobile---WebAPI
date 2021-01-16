using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks; 
using ElectronicShopAPI.Models.DAO;

namespace ElectronicShopAPI.Controllers
{
    public class BillInfoController : ApiController
    {
        [Route("Api/BillInfoController/GetAllBillInfoByBillID/{billID}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllBillInfoByBillID(int billID)
        {
            return Ok(await BillInfoDAO.Instance.GetAllBillInfoByBillID(billID));
        }

        [Route("Api/BillInfoController/CreateBillInfo/{count}/{productID}/{optionProduct}/{billID}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> CreateBillInfo(int count, int productID, int optionProduct, int billID)
        {
            return Ok(await BillInfoDAO.Instance.CreateBillInfo(count, productID, optionProduct, billID));
        }
    }
}
