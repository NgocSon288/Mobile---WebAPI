using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Text;

namespace ElectronicShopMobileApp.Assets.Contains
{
    public static class Const
    {
        public static int CurrentCustomerID = 1;
        public static readonly string FolderPath                            = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        public static readonly string SQLiteDBContextPath                   = "SQLite.db";
        public static readonly string Domain                                = $"http://huynhngocson1694.somee.com/";//$"http://huynhngocson.somee.com/";
        public static readonly string GetAllAdvertisementPath               = Domain + @"Api/AdvertisementController/GetAllAdvertisement"; 
        public static readonly string GetAllBillByCustomerIDPath            = Domain + @"Api/BillController/GetAllBillByCustomerID/{customerID}"; 
        public static readonly string CreateBillPath                        = Domain + @"Api/BillController/CreateBill/{customerID}"; 
        public static readonly string GetAllBillInfoByBillIDPath            = Domain + @"Api/BillInfoController/GetAllBillInfoByBillID/{billID}"; 
        public static readonly string CreateBillInfoPath                    = Domain + @"Api/BillInfoController/CreateBillInfo/{count}/{productID}/{optionProduct}/{billID}"; 
        public static readonly string GetAllCategoryPath                    = Domain + @"Api/CategoryController/GetAllCategory"; 
        public static readonly string GetCommentByProductIDPath             = Domain + @"Api/CommentController/GetCommentByProductID/{productID}"; 
        public static readonly string CreateCommentPath                     = Domain + @"Api/CommentController/CreateComment/{customerID}/{productID}/{startNumber}/{reason}/{description}";
        public static readonly string IncreaseOrDecreaseLikeOrDisLikePath = Domain + @"Api/CommentController/IncreaseOrDecreaseLikeOrDisLikeGet/{commentID}/{isIncrease}/{isLike}";
        public static readonly string LoginPath                             = Domain + @"Api/CustomerController/Login/{userName}/{passWord}";
        public static readonly string ForgetPasswordPath = Domain + @"Api/CustomerController/ForgetPassword/{userName}"; 
        public static readonly string ChangePasswordPath = Domain + @"Api/CustomerController/ChangePassword/{userName}/{passWord}/{newPassword}";
        public static readonly string GetAllNotificationByCustomerIDPath    = Domain + @"Api/NotificationController/GetAllNotificationByCustomerID/{customerID}"; 
        public static readonly string GetAllProductPath                     = Domain + @"Api/ProductController/GetAllProduct"; 
        public static readonly string GetAllProductByCategoryIDPath         = Domain + @"Api/ProductController/GetAllProductByCategoryID/{categoryID}"; 
        public static readonly string GetAllProductMostSearchedPath         = Domain + @"Api/ProductController/GetAllProductMostSearched"; 
        public static readonly string GetAllProductMostDiscountPath         = Domain + @"Api/ProductController/GetAllProductMostDiscount"; 
        public static readonly string GetAllProductByIDPath                 = Domain + @"Api/ProductController/GetAllProductByID/{ID}"; 
        public static readonly string IncreaseProductViewByIDPath           = Domain + @"Api/ProductController/IncreaseProductViewByID/{ID}";
        public static readonly string CanCreateAccountPath = Domain + @"Api/CustomerController/CanRegisterAccount/{userName}/{email}";
        public static readonly string CreateAccountPath = Domain + @"Api/CustomerController/RegisterAccount/{userName}/{passWord}/{fullName}/{phoneNumber}/{email}/{address}";
        public static readonly string ChangeAvatarPath = Domain + @"Api/CustomerController/UpdateAvatar/{customerID}";
        public static readonly string ReplayCharacter           = @"IE307_L11"; 
    
        public static readonly string Email = "sondeptrai2288@gmail.com";
        public static readonly string Password = "SOn01698182219";

        public static int LOGIN_PAGE = 1;
        public static int HOME_PAGE = 2;

        public static string ConverToPathWithParameter(string path, object[] param = null)
        {
            if(param == null)
                return path;

            foreach(var item in param)
            {
                var startIndex = path.IndexOf("{");
                var endIndex = path.IndexOf("}");
                var oldString = path.Substring(startIndex, endIndex - startIndex + 1);
                path = path.Replace(oldString, item.ToString());
            }
            return path;
        } 

        public static void SendMail(string to, string subject, string body)
        {
            try
            {
                MailMessage mail = new MailMessage(Const.Email, to);

                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);

                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;

                smtp.Credentials = new System.Net.NetworkCredential(Const.Email, Const.Password);

                smtp.Send(mail);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static byte[] GetImageBytes(Stream stream)
        {
            byte[] ImageBytes;
            using (var memoryStream = new System.IO.MemoryStream())
            {
                stream.CopyTo(memoryStream);
                ImageBytes = memoryStream.ToArray();
            }
            return ImageBytes;
        }

        public static Stream BytesToStream(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }

        public static string EncryptionEmail(string email)
        {
            string result = email.Substring(0, email.IndexOf("@") + 1);
            string domain = email.Substring(email.IndexOf("@") + 1);

            while(domain.IndexOf(".") >= 0)
            {
                domain = domain.Replace(".", ReplayCharacter);
            }
            var x = result + domain;
            return result + domain;
        }
    }

    public enum Page
    {
        LOGIN_PAGE,
        HOME_PAGE
    }
}
