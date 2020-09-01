using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmlakProject.Models
{
    public class TumIlanBilgileri
    {
        public IlanTablosu ilan { get; set; }
        public UyeTablosu uye { get; set; }
        public AdresTablosu adres { get; set; }
        public IlTablosu ils { get; set; }
        public IlceTablosu ilce { get; set; }
        public MahalleTablosu mahal { get; set; }
        public IlanTuruTablosu ilanTur { get; set; }
        public KonutTuruTablosu konutTur { get; set; }
        public BinaYasiTablosu yas { get; set; }
        public IsinmaTablosu isin { get; set; }
        public ResimTablosu resim { get; set; }
    }
}