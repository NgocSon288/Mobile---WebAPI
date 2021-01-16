namespace ElectronicShopAPI.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Promotion")]
    public partial class Promotion
    {
        public int ID { get; set; }

        public int? ProductID { get; set; }

        [StringLength(500)]
        public string Content { get; set; }

        public virtual Product Product { get; set; }
    }
}
