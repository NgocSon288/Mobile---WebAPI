using ElectronicShopMobileApp.Assets.Contains;
using ElectronicShopMobileApp.Models;
using ElectronicShopMobileApp.Models.SQLiteModels;
using ElectronicShopMobileApp.Services.SQLiteServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ElectronicShopMobileApp.Services
{ 
    public class LoginService 
    {
        private static LoginService instance;

        public static LoginService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LoginService();
                }
                return instance;
            }
            private set => instance = value;
        }

        public async Task<Customer> LoginAsync(string userName, string passWord)
        {
            using(HttpClient client = new HttpClient())
            {
                try
                { 
                    var dataString = await client.GetStringAsync(Const.ConverToPathWithParameter(Const.LoginPath, new object[] { userName, passWord }));

                    var customer = JsonConvert.DeserializeObject<Customer>(dataString);

                    return customer;
                }
                catch (Exception)
                {

                    return null;
                }
            }
        }

        public async Task<Customer> ForgetPasswordAsync(string userName)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var dataString = await client.GetStringAsync(Const.ConverToPathWithParameter(Const.ForgetPasswordPath, new object[] { userName }));

                    var myCustomer = JsonConvert.DeserializeObject<Customer>(dataString);

                    if (myCustomer == null)
                        return null;

                    string body = "";

                    body += "<hr/>";
                    body += "Xin chào <b>" + myCustomer.DisplayName + "</b>,<br/><br/>";
                    body += "Chúng tôi đã nhận được yêu cầu đặt lại mật khẩu Electronic Shop của bạn.<br/><br/>";
                    body += "Nhập mã đặt lại mật khẩu sau đây: " + "<b>" + myCustomer.Password + "</b><br/>";

                    body += "Thanks and Regards,<br/><br/>";
                    body += "-----------------------------<br/>";
                    body += "<b>Huynh Ngoc Son - Leader Team</b><br/>";
                    body += "<b>Mobile:</b> (84) 35 9521 032<br/>";
                    body += "<b>Facebook: </b><a href=" + "https://www.facebook.com/HuynhNgocSon123" + ">https://www.facebook.com/HuynhNgocSon123<a/>";
                    body += "<hr/>";

                    Const.SendMail(myCustomer.Email, "Forgot Password - Electronic Shop", body);

                    return myCustomer;
                }
                catch (Exception)
                {

                    return null;
                }
            }
        }

        public async Task<Customer> ChangePasswordAsync(string password, string newPassword)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var customer = CustomerSQLiteService.Instance.Find(Const.CurrentCustomerID);
                    var dataString = await client.GetStringAsync(Const.ConverToPathWithParameter(Const.ChangePasswordPath, new object[] { customer.Username, password, newPassword}));

                    var myCustomer = JsonConvert.DeserializeObject<Customer>(dataString);

                    if (myCustomer == null)
                        return null;

                    var body = "";

                    body += "<hr/>";
                    body += "Xin chào <b>" + myCustomer.DisplayName + "</b>,<br/><br/>";
                    body += "Chúng tôi đã nhận được yêu cầu đặt lại mật khẩu Electronic Shop của bạn.<br/><br/>";
                    body += "<b>Bạn đã thay đổi mật khẩu thành công!</b><br/><br/>";
                    body += "Thanks and Regards,<br/><br/>";
                    body += "-----------------------------<br/>";
                    body += "<b>Huynh Ngoc Son - Leader Team</b><br/>";
                    body += "<b>Mobile:</b> (84) 35 9521 032<br/>";
                    body += "<b>Facebook: </b><a href=" + "https://www.facebook.com/HuynhNgocSon123" + ">https://www.facebook.com/HuynhNgocSon123<a/>";
                    body += "<hr/>";

                    Const.SendMail(myCustomer.Email, "Change Password - Electronic Shop", body);

                    return myCustomer;
                }
                catch (Exception)
                { 
                    return null;
                }
            }
        }

        public async Task<string> ConfirmOTPEmail(string fullName ,string email)
        {
            Task<string> task = new Task<string>(new Func<string>(() =>
            {
                try
                {
                    string OTP = new Random().Next(1000, 10000).ToString();
                    var body = "";

                    body += "<hr/>";
                    body += "Xin chào <b>" + fullName + "</b>,<br/><br/>";
                    body += "Chúng tôi đã nhận được yêu cầu tạo tài khoản Electronic Shop của bạn.<br/><br/>";
                    body += "Vui lòng nhập mã OTP sau đây để xác nhận việc đăng ký: <b>" + OTP + "</b><br/><br/>";
                    body += "Thanks and Regards,<br/><br/>";
                    body += "-----------------------------<br/>";
                    body += "<b>Huynh Ngoc Son - Leader Team</b><br/>";
                    body += "<b>Mobile:</b> (84) 35 9521 032<br/>";
                    body += "<b>Facebook: </b><a href=" + "https://www.facebook.com/HuynhNgocSon123" + ">https://www.facebook.com/HuynhNgocSon123<a/>";
                    body += "<hr/>";

                    Const.SendMail(email, "Confirm Password - Electronic Shop", body);
                    return OTP;

                }
                catch (Exception)
                {
                    return "";
                }
            }));

            task.Start();

            return await task; 
        }

        public async Task<bool> Register(string userName, string passWord, string fullName, string phoneNumber, string email, string address, byte[] avatarRegister)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // 
                    var dataString = await client.GetStringAsync(Const.ConverToPathWithParameter(Const.CreateAccountPath, new object[] { userName, passWord, fullName, phoneNumber, Const.EncryptionEmail(email), address }));

                    var customer = JsonConvert.DeserializeObject<Customer>(dataString);
                    customer.Password = passWord;

                    if(customer !=null)
                    {
                        Const.CurrentCustomerID = customer.ID;

                        CustomerSQLiteService.Instance.Insert(new CustomerSQLite(customer));
                        CustomerTempSQLiteService.Instance.Insert(customer);
                        AvatarCustomerSQLiteService.Instance.InsertOrUpdate(new AvatarCustomerSQLite(avatarRegister));
                        return true;
                    }
                    else
                    {
                        return true;
                    }

                }
                catch (Exception)
                {

                    return false;
                }
            }
        }
         
        public async Task<int> CanRegister(string userName, string email)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                { 
                    var dataString = await client.GetStringAsync(Const.ConverToPathWithParameter(Const.CanCreateAccountPath, new object[] { userName, Const.EncryptionEmail(email) }));

                    var result = JsonConvert.DeserializeObject<int>(dataString);

                    return result;
                }
                catch (Exception)
                {

                    return -3;
                }
            }
        }

        public async Task<bool> UpdateAvatar()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                { 
                    var dataString = await client.GetStringAsync(Const.ConverToPathWithParameter(Const.ChangeAvatarPath, new object[] { Const.CurrentCustomerID }));

                    return JsonConvert.DeserializeObject<bool>(dataString); 
                }
                catch (Exception)
                {

                    return false;
                }
            }
        }
    }
}
