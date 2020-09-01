namespace EmlakProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ResimTablosu")]
    public partial class ResimTablosu
    {
        [Key]
        public int resimID { get; set; }

        [StringLength(100)]
        public string resimAdi { get; set; }

        [StringLength(200)]
        public string resimLink { get; set; }

        public int resimIlanID { get; set; }

        public virtual IlanTablosu IlanTablosu { get; set; }
    }
}
