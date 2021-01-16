namespace ElectronicShopAPI.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Customer")]
    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            Bills = new HashSet<Bill>();
            Comments = new HashSet<Comment>();
            Notifications = new HashSet<Notification>();
        }

        public int ID { get; set; }

        [StringLength(100)]
        public string UserName { get; set; }

        [StringLength(500)]
        public string PassWord { get; set; }

        [StringLength(100)]
        public string DisplayName { get; set; }

        [StringLength(15)]
        public string PhoneNumber { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(500)]
        public string Address { get; set; }

        [StringLength(500)]
        public string Avatar { get; set; }

        public bool? IsRegister { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bill> Bills { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
