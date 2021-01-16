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
    public class BillDAO
    {  
        private static BillDAO instance;

        public static BillDAO Instance
        {
            get
            {
                if(instance==null)
                {
                    instance = new BillDAO();
                }
                return instance;
            }
            private set => instance = value;
        }
         
        ElectronicDBContext db = new ElectronicDBContext();

        public async Task<List<BillDTO>> GetAllBillByCustomerID(int customerID)
        {
            return (await db.Bills
                        .Where(bill => bill.CustomerID == customerID)
                        .ToListAsync())
                        .Select(bill => new BillDTO(bill))
                        .ToList();
        }

        public async Task<int> CreateBill(int customerID)
        {
            var bill = new Bill()
            {
                CreationTime = DateTime.Now,
                TotalBill = 0,
                BillStatusID = 1,
                CustomerID = customerID,
                IsDelete = false
            };
            db.Bills.Add(bill);
            await db.SaveChangesAsync();

            return bill.ID;
        } 
    }
}