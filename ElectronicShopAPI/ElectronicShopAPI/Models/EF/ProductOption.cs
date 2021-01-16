namespace ElectronicShopAPI.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductOption")]
    public partial class ProductOption
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductOption()
        {
            BillInfoes = new HashSet<BillInfo>();
        }

        public int ID { get; set; }

        public int? ProductID { get; set; }

        public decimal? DelPrice { get; set; }

        public decimal? Price { get; set; }

        [StringLength(100)]
        public string OptionContent { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BillInfo> BillInfoes { get; set; }

        public virtual Product Product { get; set; }
    }
}
