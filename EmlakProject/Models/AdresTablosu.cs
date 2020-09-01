namespace EmlakProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AdresTablosu")]
    public partial class AdresTablosu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AdresTablosu()
        {
            IlanTablosus = new HashSet<IlanTablosu>();
        }

        [Key]
        public int adresID { get; set; }

        public int adresIlID { get; set; }

        public int adresIlceID { get; set; }

        public int adresMahalleID { get; set; }

        [Required]
        [StringLength(100)]
        public string adresDetay { get; set; }

        public virtual IlTablosu IlTablosu { get; set; }

        public virtual IlceTablosu IlceTablosu { get; set; }

        public virtual MahalleTablosu MahalleTablosu { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IlanTablosu> IlanTablosus { get; set; }
    }
}
