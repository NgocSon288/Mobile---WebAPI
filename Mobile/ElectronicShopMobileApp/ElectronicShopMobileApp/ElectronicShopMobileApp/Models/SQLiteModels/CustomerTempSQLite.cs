using System;
namespace ElectronicShopMobileApp.Models.SQLiteModels
{
    public class CustomerTempSQLite
    {
        public string UserName { get; set; }

        public string PassWord { get; set; }

        public CustomerTempSQLite()
        {

        }

        public CustomerTempSQLite(Customer customer)
        {
            UserName = customer.Username;
            PassWord = customer.Password;
        }
    }
}
