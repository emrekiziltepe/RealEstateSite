namespace EmlakProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("IlceTablosu")]
    public partial class IlceTablosu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IlceTablosu()
        {
            AdresTablosus = new HashSet<AdresTablosu>();
            MahalleTablosus = new HashSet<MahalleTablosu>();
        }

        [Key]
        public int ilceID { get; set; }

        [Required]
        [StringLength(20)]
        public string ilceAdi { get; set; }

        public int ilceIlID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AdresTablosu> AdresTablosus { get; set; }

        public virtual IlTablosu IlTablosu { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MahalleTablosu> MahalleTablosus { get; set; }
    }
}
