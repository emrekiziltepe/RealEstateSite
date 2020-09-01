namespace EmlakProject.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Emlak : DbContext
    {
        public Emlak()
            : base("name=Emlak")
        {
        }

        public virtual DbSet<AdresTablosu> AdresTablosus { get; set; }
        public virtual DbSet<BinaYasiTablosu> BinaYasiTablosus { get; set; }
        public virtual DbSet<IlanOnayTablosu> IlanOnayTablosus { get; set; }
        public virtual DbSet<IlanTablosu> IlanTablosus { get; set; }
        public virtual DbSet<IlanTuruTablosu> IlanTuruTablosus { get; set; }
        public virtual DbSet<IlceTablosu> IlceTablosus { get; set; }
        public virtual DbSet<IlTablosu> IlTablosus { get; set; }
        public virtual DbSet<IsinmaTablosu> IsinmaTablosus { get; set; }
        public virtual DbSet<KonutTuruTablosu> KonutTuruTablosus { get; set; }
        public virtual DbSet<MahalleTablosu> MahalleTablosus { get; set; }
        public virtual DbSet<ResimTablosu> ResimTablosus { get; set; }
        public virtual DbSet<SatisDurumTablosu> SatisDurumTablosus { get; set; }
        public virtual DbSet<UyeTablosu> UyeTablosus { get; set; }
        public virtual DbSet<UyeTuruTablosu> UyeTuruTablosus { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdresTablosu>()
                .Property(e => e.adresDetay)
                .IsUnicode(false);

            modelBuilder.Entity<AdresTablosu>()
                .HasMany(e => e.IlanTablosus)
                .WithRequired(e => e.AdresTablosu)
                .HasForeignKey(e => e.ilanAdresID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BinaYasiTablosu>()
                .Property(e => e.binaYasi)
                .IsUnicode(false);

            modelBuilder.Entity<BinaYasiTablosu>()
                .HasMany(e => e.IlanTablosus)
                .WithRequired(e => e.BinaYasiTablosu)
                .HasForeignKey(e => e.ilanBinaYasiID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<IlanTablosu>()
                .Property(e => e.ilanBaslik)
                .IsUnicode(false);

            modelBuilder.Entity<IlanTablosu>()
                .Property(e => e.ilanAciklama)
                .IsUnicode(false);

            modelBuilder.Entity<IlanTablosu>()
                .HasMany(e => e.IlanOnayTablosus)
                .WithRequired(e => e.IlanTablosu)
                .HasForeignKey(e => e.ilanOnayIlanID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<IlanTablosu>()
                .HasMany(e => e.ResimTablosus)
                .WithRequired(e => e.IlanTablosu)
                .HasForeignKey(e => e.resimIlanID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<IlanTablosu>()
                .HasMany(e => e.SatisDurumTablosus)
                .WithRequired(e => e.IlanTablosu)
                .HasForeignKey(e => e.satisDurumIlanID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<IlanTuruTablosu>()
                .Property(e => e.ilanTuru)
                .IsUnicode(false);

            modelBuilder.Entity<IlanTuruTablosu>()
                .HasMany(e => e.IlanTablosus)
                .WithRequired(e => e.IlanTuruTablosu)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<IlceTablosu>()
                .Property(e => e.ilceAdi)
                .IsUnicode(false);

            modelBuilder.Entity<IlceTablosu>()
                .HasMany(e => e.AdresTablosus)
                .WithRequired(e => e.IlceTablosu)
                .HasForeignKey(e => e.adresIlceID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<IlceTablosu>()
                .HasMany(e => e.MahalleTablosus)
                .WithRequired(e => e.IlceTablosu)
                .HasForeignKey(e => e.mahalleIlceID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<IlTablosu>()
                .Property(e => e.ilAdi)
                .IsUnicode(false);

            modelBuilder.Entity<IlTablosu>()
                .HasMany(e => e.AdresTablosus)
                .WithRequired(e => e.IlTablosu)
                .HasForeignKey(e => e.adresIlID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<IlTablosu>()
                .HasMany(e => e.IlceTablosus)
                .WithRequired(e => e.IlTablosu)
                .HasForeignKey(e => e.ilceIlID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<IsinmaTablosu>()
                .Property(e => e.isinmaTuru)
                .IsUnicode(false);

            modelBuilder.Entity<IsinmaTablosu>()
                .HasMany(e => e.IlanTablosus)
                .WithRequired(e => e.IsinmaTablosu)
                .HasForeignKey(e => e.ilanIsinmaID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KonutTuruTablosu>()
                .Property(e => e.konutTuru)
                .IsUnicode(false);

            modelBuilder.Entity<KonutTuruTablosu>()
                .HasMany(e => e.IlanTablosus)
                .WithRequired(e => e.KonutTuruTablosu)
                .HasForeignKey(e => e.ilanKonutTuruID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MahalleTablosu>()
                .Property(e => e.mahalleAdi)
                .IsUnicode(false);

            modelBuilder.Entity<MahalleTablosu>()
                .HasMany(e => e.AdresTablosus)
                .WithRequired(e => e.MahalleTablosu)
                .HasForeignKey(e => e.adresMahalleID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ResimTablosu>()
                .Property(e => e.resimAdi)
                .IsUnicode(false);

            modelBuilder.Entity<ResimTablosu>()
                .Property(e => e.resimLink)
                .IsUnicode(false);

            modelBuilder.Entity<UyeTablosu>()
                .Property(e => e.uyeAdSoyad)
                .IsUnicode(false);

            modelBuilder.Entity<UyeTablosu>()
                .Property(e => e.uyeSifre)
                .IsUnicode(false);

            modelBuilder.Entity<UyeTablosu>()
                .Property(e => e.uyeMail)
                .IsUnicode(false);

            modelBuilder.Entity<UyeTablosu>()
                .HasMany(e => e.IlanOnayTablosus)
                .WithRequired(e => e.UyeTablosu)
                .HasForeignKey(e => e.ilanOnayUyeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UyeTablosu>()
                .HasMany(e => e.IlanTablosus)
                .WithRequired(e => e.UyeTablosu)
                .HasForeignKey(e => e.ilanUyeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UyeTablosu>()
                .HasMany(e => e.SatisDurumTablosus)
                .WithRequired(e => e.UyeTablosu)
                .HasForeignKey(e => e.satisDurumAlanID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UyeTablosu>()
                .HasMany(e => e.SatisDurumTablosus1)
                .WithRequired(e => e.UyeTablosu1)
                .HasForeignKey(e => e.satisDurumSatanID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UyeTuruTablosu>()
                .Property(e => e.uyeTuruAdi)
                .IsUnicode(false);

            modelBuilder.Entity<UyeTuruTablosu>()
                .HasMany(e => e.UyeTablosus)
                .WithRequired(e => e.UyeTuruTablosu)
                .WillCascadeOnDelete(false);
        }
    }
}
