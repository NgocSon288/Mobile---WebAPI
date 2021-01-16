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
    public class AdvertisementDAO
    {
        private static AdvertisementDAO instance;

        public static AdvertisementDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AdvertisementDAO();
                }
                return instance;
            }
            private set => instance = value;
        }

        ElectronicDBContext db = new ElectronicDBContext();

        public async Task<List<AdvertisementDTO>> GetAllAdvertisement()
        {
            return (await db.Advertisements   
                        .ToListAsync())
                        .Select(advertisement => new AdvertisementDTO(advertisement))
                        .ToList();
        }
    }
}