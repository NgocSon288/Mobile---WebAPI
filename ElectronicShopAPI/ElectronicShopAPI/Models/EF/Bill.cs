namespace ElectronicShopAPI.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Bill")]
    public partial class Bill
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Bill()
        {
            BillInfoes = new HashSet<BillInfo>();
        }

        public int ID { get; set; }

        public DateTime? CreationTime { get; set; }

        public decimal? TotalBill { get; set; }

        public int? BillStatusID { get; set; }

        public int? CustomerID { get; set; }

        public bool? IsDelete { get; set; }

        public virtual BillStatu BillStatu { get; set; }

        public virtual Customer Customer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BillInfo> BillInfoes { get; set; }
    }
}
