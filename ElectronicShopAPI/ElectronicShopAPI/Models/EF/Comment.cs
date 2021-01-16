namespace ElectronicShopAPI.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Comment")]
    public partial class Comment
    {
        public int ID { get; set; }

        public int? CustomerID { get; set; }

        public int? ProductID { get; set; }

        public int? LikeNumber { get; set; }

        public int? DisLikeNumber { get; set; }

        public int? StarNumber { get; set; }

        [StringLength(100)]
        public string Reason { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Product Product { get; set; }
    }
}
