using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ElectronicShopAPI.Assets.Contains
{
    public static class Const
    {
        public static readonly string AdvertisementImagePath    = @"http://HuynhNgocSon1694.somee.com/Assets/Images/Advertisement/";
        public static readonly string CategoryImagePath         = @"http://HuynhNgocSon1694.somee.com/Assets/Images/Category/";
        public static readonly string CustomerImagePath         = @"http://HuynhNgocSon1694.somee.com/Assets/Images/Customer/";
        public static readonly string AccessoriesImagePath      = @"http://HuynhNgocSon1694.somee.com/Assets/Images/Product/Accessories/";     
        public static readonly string CellphoneImagePath        = @"http://HuynhNgocSon1694.somee.com/Assets/Images/Product/Cellphone/";
        public static readonly string LaptopImagePath           = @"http://HuynhNgocSon1694.somee.com/Assets/Images/Product/Laptop/";
        public static readonly string SmartWatchImagePath       = @"http://HuynhNgocSon1694.somee.com/Assets/Images/Product/SmartWatch/";
        public static readonly string TabletImagePath           = @"http://HuynhNgocSon1694.somee.com/Assets/Images/Product/Tablet/";
        public static readonly string ReplaceCharacter           = "IE307_L11";


        public static string ConvertEmail(string email)
        {
            string result = email.Substring(0, email.IndexOf("@") + 1);
            string domain = email.Substring(email.IndexOf("@") + 1);
            
            while(domain.IndexOf(ReplaceCharacter) >= 0)
            {
                domain = domain.Replace(ReplaceCharacter, ".");
            }

            return result + domain;
        }

        #region Verify email
        public static async Task<bool> VerifyEmail(string email)
        {
            Task<bool> task = new Task<bool>(new Func<bool>(() =>
            {
                TcpClient tClient = new TcpClient("gmail-smtp-in.l.google.com", 25);
                string CRLF = "\r\n";
                byte[] dataBuffer;
                string ResponseString;
                NetworkStream netStream = tClient.GetStream();
                StreamReader reader = new StreamReader(netStream);
                ResponseString = reader.ReadLine();
                /* Perform HELO to SMTP Server and get Response */
                dataBuffer = BytesFromString("HELO KirtanHere" + CRLF);
                netStream.Write(dataBuffer, 0, dataBuffer.Length);
                ResponseString = reader.ReadLine();
                dataBuffer = BytesFromString("MAIL FROM:<YourGmailIDHere@gmail.com>" + CRLF);
                netStream.Write(dataBuffer, 0, dataBuffer.Length);
                ResponseString = reader.ReadLine();
                /* Read Response of the RCPT TO Message to know from google if it exist or not */
                dataBuffer = BytesFromString("RCPT TO:<" + email + ">" + CRLF);
                netStream.Write(dataBuffer, 0, dataBuffer.Length);
                ResponseString = reader.ReadLine();
                /* QUITE CONNECTION */
                dataBuffer = BytesFromString("QUITE" + CRLF);
                netStream.Write(dataBuffer, 0, dataBuffer.Length);
                tClient.Close();

                return ResponseString.Contains("OK");
            }));

            task.Start();

            return await task;
        }

        public static byte[] BytesFromString(string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }

        public static int GetResponseCode(string ResponseString)
        {
            return int.Parse(ResponseString.Substring(0, 3));
        } 
        #endregion
    }
}