namespace ElectronicShopAPI.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            BillInfoes = new HashSet<BillInfo>();
            Comments = new HashSet<Comment>();
            ProductDetails = new HashSet<ProductDetail>();
            ProductOptions = new HashSet<ProductOption>();
            Promotions = new HashSet<Promotion>();
        }

        public int ID { get; set; }

        public int? CategoryID { get; set; }

        public int? KeyWordID { get; set; }

        public double? Rating { get; set; }

        public int? Discount { get; set; }

        public bool? IsFreeShip { get; set; }

        public int? ViewNumber { get; set; }

        [StringLength(500)]
        public string Image1 { get; set; }

        [StringLength(500)]
        public string Image2 { get; set; }

        [StringLength(500)]
        public string Image3 { get; set; }

        [StringLength(500)]
        public string Image4 { get; set; }

        public bool? IsNew { get; set; }

        public bool? IsInstallment { get; set; }

        [StringLength(100)]
        public string DisplayName { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BillInfo> BillInfoes { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }

        public virtual KeyWord KeyWord { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductDetail> ProductDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductOption> ProductOptions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Promotion> Promotions { get; set; }
    }
}
