namespace EmlakProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("IsinmaTablosu")]
    public partial class IsinmaTablosu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IsinmaTablosu()
        {
            IlanTablosus = new HashSet<IlanTablosu>();
        }

        [Key]
        public int isinmaID { get; set; }

        [StringLength(30)]
        public string isinmaTuru { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IlanTablosu> IlanTablosus { get; set; }
    }
}
