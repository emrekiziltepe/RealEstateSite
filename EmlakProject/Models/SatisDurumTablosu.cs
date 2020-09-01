namespace EmlakProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SatisDurumTablosu")]
    public partial class SatisDurumTablosu
    {
        [Key]
        public int satisDurumID { get; set; }

        public int satisDurumIlanID { get; set; }

        public int satisDurumAlanID { get; set; }

        public int satisDurumSatanID { get; set; }

        public virtual IlanTablosu IlanTablosu { get; set; }

        public virtual UyeTablosu UyeTablosu { get; set; }

        public virtual UyeTablosu UyeTablosu1 { get; set; }
    }
}
