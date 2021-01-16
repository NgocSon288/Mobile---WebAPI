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
    public class BillInfoDAO
    {
        private static BillInfoDAO instance;

        public static BillInfoDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BillInfoDAO();
                }
                return instance;
            }
            private set => instance = value;
        }

        ElectronicDBContext db = new ElectronicDBContext();

        public async Task<List<BillInfoDTO>> GetAllBillInfoByBillID(int billID)
        {
            return (await db.BillInfoes
                            .Where(billinfo => billinfo.BillID == billID)
                            .ToListAsync())
                            .Select(billinfo => new BillInfoDTO(billinfo))
                            .ToList();
        }

        public async Task<string> CreateBillInfo(int count, int productID, int optionProduct, int billID)
        {
            try
            {
                var billInfo = new BillInfo()
                {
                    Count = count,
                    Total = 0,
                    ProductID = productID,
                    BillID = billID,
                    OptionProduct = optionProduct
                };
                db.BillInfoes.Add(billInfo);
                await db.SaveChangesAsync();
            }
            catch (Exception e)
            {

                return e.Message;
            }

            return "OK";
        }
    }
}  