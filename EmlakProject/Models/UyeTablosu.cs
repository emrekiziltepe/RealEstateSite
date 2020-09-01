namespace EmlakProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UyeTablosu")]
    public partial class UyeTablosu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UyeTablosu()
        {
            IlanOnayTablosus = new HashSet<IlanOnayTablosu>();
            IlanTablosus = new HashSet<IlanTablosu>();
            SatisDurumTablosus = new HashSet<SatisDurumTablosu>();
            SatisDurumTablosus1 = new HashSet<SatisDurumTablosu>();
        }

        [Key]
        public int uyeID { get; set; }

        [Required]
        [StringLength(50)]
        public string uyeAdSoyad { get; set; }

        [Required]
        [StringLength(20)]
        public string uyeSifre { get; set; }

        [Required]
        [StringLength(50)]
        public string uyeMail { get; set; }

        public int uyeTuruID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IlanOnayTablosu> IlanOnayTablosus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IlanTablosu> IlanTablosus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SatisDurumTablosu> SatisDurumTablosus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SatisDurumTablosu> SatisDurumTablosus1 { get; set; }

        public virtual UyeTuruTablosu UyeTuruTablosu { get; set; }
    }
}
