namespace EmlakProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("IlanOnayTablosu")]
    public partial class IlanOnayTablosu
    {
        [Key]
        public int ilanOnayID { get; set; }

        public int ilanOnayUyeID { get; set; }

        public int ilanOnayIlanID { get; set; }

        public int ilanOnayDurum { get; set; }

        public virtual UyeTablosu UyeTablosu { get; set; }

        public virtual IlanTablosu IlanTablosu { get; set; }
    }
}
