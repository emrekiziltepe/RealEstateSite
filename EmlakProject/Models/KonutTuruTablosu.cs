namespace EmlakProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KonutTuruTablosu")]
    public partial class KonutTuruTablosu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KonutTuruTablosu()
        {
            IlanTablosus = new HashSet<IlanTablosu>();
        }

        [Key]
        public int konutTuruID { get; set; }

        [StringLength(20)]
        public string konutTuru { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IlanTablosu> IlanTablosus { get; set; }
    }
}
