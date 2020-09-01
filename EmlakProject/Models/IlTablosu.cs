namespace EmlakProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("IlTablosu")]
    public partial class IlTablosu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IlTablosu()
        {
            AdresTablosus = new HashSet<AdresTablosu>();
            IlceTablosus = new HashSet<IlceTablosu>();
        }

        [Key]
        public int ilID { get; set; }

        [Required]
        [StringLength(15)]
        public string ilAdi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AdresTablosu> AdresTablosus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IlceTablosu> IlceTablosus { get; set; }
    }
}
