using ElectronicShopAPI.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectronicShopAPI.Models.DTO
{
    public class BillDTO
    {
        public int ID { get; set; }

        public DateTime? CreationTime { get; set; }

        public decimal? TotalBill { get; set; } 

        public string BillStatusName { get; set; }

        public BillDTO()
        {

        }

        public BillDTO(Bill bill)
        {
            ID = bill.ID;
            CreationTime = bill.CreationTime;
            TotalBill = bill.TotalBill;
            BillStatusName = bill.BillStatu.Status;
        }
    }
}