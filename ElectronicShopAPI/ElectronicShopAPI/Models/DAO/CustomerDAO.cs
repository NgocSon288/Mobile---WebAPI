using ElectronicShopAPI.Assets.Contains;
using ElectronicShopAPI.Models.DTO;
using ElectronicShopAPI.Models.EF;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ElectronicShopAPI.Models.DAO
{
    public class CustomerDAO
    {
        private static CustomerDAO instance;

        public static CustomerDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CustomerDAO();
                }
                return instance;
            }
            private set => instance = value;
        }

        ElectronicDBContext db = new ElectronicDBContext();

        public async Task<CustomerDTO> Login(string userName, string passWord)
        {
            var pw = passWord;

            passWord = Encryption.Instance.CreateMD5(passWord);

            var myCustommer = await db.Customers.SingleOrDefaultAsync(customer => customer.UserName == userName && customer.PassWord == passWord);

            return myCustommer == null ? null : new CustomerDTO(myCustommer) { Password = pw };
        }

        public async Task<CustomerDTO> ForgetPassword(string userName)
        {

            var myCustomer = await db.Customers.SingleOrDefaultAsync(customer => customer.UserName == userName);
            Random rand = new Random();
            string newPassword = "";
            var length = 0;
            CustomerDTO customertDto = null;

            if (myCustomer == null)
            {
                return null;
            }

            while (length <= 8)
            {
                length++;
                if (length % 2 == 0)
                {
                    newPassword += (char)rand.Next(65, 91);
                }
                else
                {
                    newPassword += (char)rand.Next(97, 123);
                }
            }

            customertDto = new CustomerDTO(myCustomer) { Password = newPassword };

            myCustomer.PassWord = Encryption.Instance.CreateMD5(newPassword);

            await db.SaveChangesAsync();

            return customertDto;
        }

        public async Task<CustomerDTO> ChangePassword(string userName, string password, string newPassword)
        {
            password = Encryption.Instance.CreateMD5(password);
            var myCustomer = await db.Customers.SingleOrDefaultAsync(customer => customer.UserName == userName && customer.PassWord == password);
            CustomerDTO customertDto = null;

            if (myCustomer == null)
                return null;

            customertDto = new CustomerDTO(myCustomer) { Password = newPassword };

            myCustomer.PassWord = Encryption.Instance.CreateMD5(newPassword);

            await db.SaveChangesAsync();

            return customertDto;
        }

        public async Task<int> CanRegisterAccount(string userName, string email)
        {
            if (await db.Customers.SingleOrDefaultAsync(customer => customer.UserName == userName) != null)
                return -1;

            return await Const.VerifyEmail(Const.ConvertEmail(email)) ? 1 : -2; 
        }  

        public async Task<CustomerDTO> RegisterAccount(string userName, string passWord, string fullName, string phoneNumber, string email, string address)
        {
            passWord = Encryption.Instance.CreateMD5(passWord);

            var customer = new Customer()
            {
                UserName = userName,
                PassWord = passWord,
                DisplayName = fullName,
                PhoneNumber = phoneNumber,
                Email = Const.ConvertEmail(email),
                Address = address,
                Avatar = "", 
                IsRegister = true
            };

            db.Customers.Add(customer);
            await db.SaveChangesAsync();

            return new CustomerDTO(customer);
        }

        public async Task<bool> UpdateAvatar(int customerID)
        {
            try
            {
                var myCustomer = await db.Customers.SingleOrDefaultAsync(Customer => Customer.ID == customerID); 
                myCustomer.IsRegister = true; 

                await db.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            } 
        }

    }
} 