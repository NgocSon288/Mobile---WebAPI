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
    public class CustomerController : ApiController
    {
        [Route("Api/CustomerController/Login/{userName}/{passWord}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> Login(string userName, string passWord)
        {
            return Ok(await CustomerDAO.Instance.Login(userName, passWord));
        }

        [Route("Api/CustomerController/ForgetPassword/{userName}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> ForgetPassword(string userName)
        {
            return Ok(await CustomerDAO.Instance.ForgetPassword(userName));
        }

        [Route("Api/CustomerController/ChangePassword/{userName}/{password}/{newPassword}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> ChangePassword(string userName, string password, string newPassword)
        {
            return Ok(await CustomerDAO.Instance.ChangePassword(userName, password, newPassword));
        }

        [Route("Api/CustomerController/CanRegisterAccount/{userName}/{email}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> CanRegisterAccount(string userName, string email)
        {
            return Ok(await CustomerDAO.Instance.CanRegisterAccount(userName, email));
        } 

        [Route("Api/CustomerController/RegisterAccount/{userName}/{passWord}/{fullName}/{phoneNumber}/{email}/{address}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> RegisterAccount(string userName, string passWord, string fullName, string phoneNumber, string email, string address)
        {
            return Ok(await CustomerDAO.Instance.RegisterAccount(userName, passWord, fullName, phoneNumber, email, address));
        }

        [Route("Api/CustomerController/UpdateAvatar/{customerID}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> UpdateAvatar(int customerID)
        {
            return Ok(await CustomerDAO.Instance.UpdateAvatar(customerID));
        }
    } 
}
