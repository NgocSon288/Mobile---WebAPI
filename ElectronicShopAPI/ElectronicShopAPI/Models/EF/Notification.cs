namespace ElectronicShopAPI.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Notification")]
    public partial class Notification
    {
        public int ID { get; set; }

        public int? CustomerID { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        public DateTime? CreationTime { get; set; }

        public bool? IsDelete { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
