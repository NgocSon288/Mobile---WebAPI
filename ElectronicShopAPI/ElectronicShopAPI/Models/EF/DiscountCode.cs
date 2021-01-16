namespace ElectronicShopAPI.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DiscountCode")]
    public partial class DiscountCode
    {
        [Key]
        [StringLength(500)]
        public string Code { get; set; }

        public int? Discount { get; set; }
    }
}
