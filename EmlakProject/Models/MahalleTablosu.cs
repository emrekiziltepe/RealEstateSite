namespace EmlakProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MahalleTablosu")]
    public partial class MahalleTablosu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MahalleTablosu()
        {
            AdresTablosus = new HashSet<AdresTablosu>();
        }

        [Key]
        public int mahalleID { get; set; }

        [Required]
        [StringLength(20)]
        public string mahalleAdi { get; set; }

        public int mahalleIlceID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AdresTablosu> AdresTablosus { get; set; }

        public virtual IlceTablosu IlceTablosu { get; set; }
    }
}
