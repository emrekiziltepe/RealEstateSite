namespace EmlakProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UyeTuruTablosu")]
    public partial class UyeTuruTablosu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UyeTuruTablosu()
        {
            UyeTablosus = new HashSet<UyeTablosu>();
        }

        [Key]
        public int uyeTuruID { get; set; }

        [Required]
        [StringLength(10)]
        public string uyeTuruAdi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UyeTablosu> UyeTablosus { get; set; }
    }
}
