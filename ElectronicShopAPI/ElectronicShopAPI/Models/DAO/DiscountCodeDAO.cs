using ElectronicShopAPI.Assets.Contains;
using ElectronicShopAPI.Models.DTO;
using ElectronicShopAPI.Models.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ElectronicShopAPI.Models.DAO
{
    public class DiscountCodeDAO
    {
        private static DiscountCodeDAO instance;

        public static DiscountCodeDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DiscountCodeDAO();
                }
                return instance;
            }
            private set => instance = value;
        } 

        ElectronicDBContext db = new ElectronicDBContext();

        public async Task<bool> IsCorrectDiscountCode(string code)
        {
            code = Encryption.Instance.CreateMD5(code);
            return await db.DiscountCodes.SingleOrDefaultAsync(discountCode => discountCode.Code == code) != null;
        }
    }
}