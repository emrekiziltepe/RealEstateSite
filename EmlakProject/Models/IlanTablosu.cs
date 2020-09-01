namespace EmlakProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("IlanTablosu")]
    public partial class IlanTablosu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IlanTablosu()
        {
            IlanOnayTablosus = new HashSet<IlanOnayTablosu>();
            ResimTablosus = new HashSet<ResimTablosu>();
            SatisDurumTablosus = new HashSet<SatisDurumTablosu>();
        }

        [Key]
        public int ilanID { get; set; }

        [Required]
        [StringLength(100)]
        public string ilanBaslik { get; set; }

        public int ilanUyeID { get; set; }

        public int ilanAdresID { get; set; }

        public int ilanTuruID { get; set; }

        public int ilanKonutTuruID { get; set; }

        public int ilanFiyat { get; set; }

        public int ilanOdaSayisi { get; set; }

        public int ilanBanyoSayisi { get; set; }

        public int ilanSalonSayisi { get; set; }

        public int ilanM2 { get; set; }

        public DateTime? ilanYayinTarihi { get; set; }

        public int ilanBinaYasiID { get; set; }

        public int ilanIsinmaID { get; set; }

        public int ilanEsyaDurumu { get; set; }

        [StringLength(200)]
        public string ilanAciklama { get; set; }

        public int ilanSatisDurumu { get; set; }

        public int ilanOnayDurumu { get; set; }

        public virtual AdresTablosu AdresTablosu { get; set; }

        public virtual BinaYasiTablosu BinaYasiTablosu { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IlanOnayTablosu> IlanOnayTablosus { get; set; }

        public virtual IsinmaTablosu IsinmaTablosu { get; set; }

        public virtual KonutTuruTablosu KonutTuruTablosu { get; set; }

        public virtual IlanTuruTablosu IlanTuruTablosu { get; set; }

        public virtual UyeTablosu UyeTablosu { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ResimTablosu> ResimTablosus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SatisDurumTablosu> SatisDurumTablosus { get; set; }
    }
}
