namespace ElectronicShopAPI.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BillInfo")]
    public partial class BillInfo
    {
        public int ID { get; set; }

        public int? Count { get; set; }

        public decimal? Total { get; set; }

        public int? ProductID { get; set; }

        public int? OptionProduct { get; set; }

        public int? BillID { get; set; }

        public virtual Bill Bill { get; set; }

        public virtual ProductOption ProductOption { get; set; }

        public virtual Product Product { get; set; }
    }
}
