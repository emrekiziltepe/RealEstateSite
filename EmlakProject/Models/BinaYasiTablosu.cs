namespace EmlakProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BinaYasiTablosu")]
    public partial class BinaYasiTablosu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BinaYasiTablosu()
        {
            IlanTablosus = new HashSet<IlanTablosu>();
        }

        [Key]
        public int binaYasiID { get; set; }

        [StringLength(10)]
        public string binaYasi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IlanTablosu> IlanTablosus { get; set; }
    }
}
