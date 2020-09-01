using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmlakProject.Models;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.Entity.Migrations;
using System.IO;
using System.Drawing;
using System.Data.Entity.Core.Common.CommandTrees;

namespace EmlakProject.Controllers
{
    public class IlanController : Controller
    {
        Emlak e = new Emlak();
        static int adresGetir;
        static int ilanGetir;

        // GET: Ilan
        [Authorize(Roles = "1")]
        public ActionResult Index()
        {

            List<IlanTablosu> ilanlar = e.IlanTablosus.ToList();
            List<UyeTablosu> uyeler = e.UyeTablosus.ToList();
            List<AdresTablosu> adresler = e.AdresTablosus.ToList();
            List<IlTablosu> iller = e.IlTablosus.ToList();
            List<IlceTablosu> ilceler = e.IlceTablosus.ToList();
            List<MahalleTablosu> mahaller = e.MahalleTablosus.ToList();
            List<IlanTuruTablosu> ilanTurleri = e.IlanTuruTablosus.ToList();
            List<KonutTuruTablosu> konutTurleri = e.KonutTuruTablosus.ToList();
            List<BinaYasiTablosu> yaslar = e.BinaYasiTablosus.ToList();
            List<IsinmaTablosu> isinmalar = e.IsinmaTablosus.ToList();
            List<ResimTablosu> resimler = e.ResimTablosus.ToList();

            IEnumerable<ResimTablosu> ilkResim = resimler.GroupBy(x => x.resimIlanID).Select(group => group.First());

            var TumIlanlar = from i in ilanlar
                             join u in uyeler on i.ilanUyeID equals u.uyeID into table1
                             from u in table1.ToList()
                             join a in adresler on i.ilanAdresID equals a.adresID into table2
                             from a in table2.ToList()
                             join il in iller on a.adresIlID equals il.ilID into table3
                             from il in table3.ToList()
                             join ilc in ilceler on a.adresIlceID equals ilc.ilceID into table4
                             from ilc in table4.ToList()
                             join m in mahaller on a.adresMahalleID equals m.mahalleID into table5
                             from m in table5.ToList()
                             join it in ilanTurleri on i.ilanTuruID equals it.ilanTuruID into table6
                             from it in table6.ToList()
                             join k in konutTurleri on i.ilanKonutTuruID equals k.konutTuruID into table7
                             from k in table7.ToList()
                             join y in yaslar on i.ilanBinaYasiID equals y.binaYasiID into table8
                             from y in table8.ToList()
                             join isi in isinmalar on i.ilanIsinmaID equals isi.isinmaID into table9
                             from isi in table9.ToList()
                             join r in ilkResim on i.ilanID equals r.resimIlanID into table10
                             from r in table10.ToList()
                             select new TumIlanBilgileri
                             {
                                 ilan = i,
                                 uye = u,
                                 adres = a,
                                 ils = il,
                                 ilce = ilc,
                                 mahal = m,
                                 ilanTur = it,
                                 konutTur = k,
                                 yas = y,
                                 isin = isi,
                                 resim = r
                             };
            return View(TumIlanlar);

        }

        [Authorize]
        public ActionResult AdresEkle()
        {
            ViewBag.iller = e.IlTablosus.ToList();
            ViewBag.ilceler = e.IlceTablosus.ToList();
            ViewBag.mahalleler = e.MahalleTablosus.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult AdresEkle(AdresTablosu adres)
        {
            e.AdresTablosus.Add(adres);
            e.SaveChanges();

            adresGetir = adres.adresID;

            return RedirectToAction("IlanEkle", adresGetir);
        }

        [Authorize]
        public ActionResult IlanEkle()
        {
            UyeTablosu uye = e.UyeTablosus.FirstOrDefault(x => x.uyeAdSoyad == HttpContext.User.Identity.Name);
            ViewBag.uyeIdGetir = uye.uyeID;

            ViewBag.ilanTurleri = e.IlanTuruTablosus.ToList();
            ViewBag.konutTurleri = e.KonutTuruTablosus.ToList();
            ViewBag.binaYaslari = e.BinaYasiTablosus.ToList();
            ViewBag.isinmalar = e.IsinmaTablosus.ToList();
            ViewBag.adresGetir = adresGetir;

            return View();
        }
        [HttpPost]
        public ActionResult IlanEkle(IlanTablosu i)
        {
            e.IlanTablosus.Add(i);
            e.SaveChanges();

            ilanGetir = i.ilanID;

            return RedirectToAction("ResimEkle");
        }

        [Authorize]
        public ActionResult ResimEkle()
        {
            ViewBag.ilanIdGetir = ilanGetir;

            return View();
        }
        [HttpPost]
        public ActionResult ResimEkle(IEnumerable<HttpPostedFileBase> files)
        {
            ResimTablosu rsm = new ResimTablosu();

            if (files.First() != null)
            {
                foreach (var file in files)
                {
                    if (file != null)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("/Images"), fileName);
                        file.SaveAs(path);
                        rsm.resimAdi = file.FileName;
                        rsm.resimLink = path;
                        rsm.resimIlanID = ilanGetir;
                        e.ResimTablosus.Add(rsm);
                        e.SaveChanges();
                    }
                }
            }
            else
            {
                rsm.resimAdi = "resimYok.jpg";
                rsm.resimLink = "/Images/resimYok.jpg";
                rsm.resimIlanID = ilanGetir;
                e.ResimTablosus.Add(rsm);
                e.SaveChanges();
            }

            return RedirectToAction("Ilanlarim");
        }

        [HttpPost]
        public void IlanSil(int sid)
        {
            IlanTablosu ilan = e.IlanTablosus.FirstOrDefault(x => x.ilanID == sid);
            AdresTablosu adres = e.AdresTablosus.FirstOrDefault(x => x.adresID == ilan.ilanAdresID);
            List<ResimTablosu> resim = e.ResimTablosus.Where(x => x.resimIlanID == ilan.ilanID).ToList();
            e.AdresTablosus.Remove(adres);
            foreach (var item in resim)
            {
                e.ResimTablosus.Remove(item);
            }
            e.IlanTablosus.Remove(ilan);
            e.SaveChanges();
        }

        public ActionResult IlanGuncelle(int id)
        {
            UyeTablosu uye = e.UyeTablosus.FirstOrDefault(x => x.uyeAdSoyad == HttpContext.User.Identity.Name);
            ViewBag.uyeIdGetir = uye.uyeID;

            ViewBag.ilanTurleri = e.IlanTuruTablosus.ToList();
            ViewBag.konutTurleri = e.KonutTuruTablosus.ToList();
            ViewBag.binaYaslari = e.BinaYasiTablosus.ToList();
            ViewBag.isinmalar = e.IsinmaTablosus.ToList();

            IlanTablosu i = e.IlanTablosus.FirstOrDefault(x => x.ilanID == id);

            return View(i);
        }
        [HttpPost]
        public ActionResult IlanGuncelle(IlanTablosu ilan)
        {
            e.IlanTablosus.AddOrUpdate(ilan);
            e.SaveChanges();
            return RedirectToAction("Ilanlarim");
        }

        [Authorize]
        public ActionResult Ilanlarim()
        {
            UyeTablosu uye = e.UyeTablosus.FirstOrDefault(x => x.uyeAdSoyad == HttpContext.User.Identity.Name);

            List<IlanTablosu> ilanlarim = e.IlanTablosus.Where(x => x.ilanUyeID == uye.uyeID).ToList<IlanTablosu>();

            List<UyeTablosu> uyeler = e.UyeTablosus.ToList();
            List<AdresTablosu> adresler = e.AdresTablosus.ToList();
            List<IlTablosu> iller = e.IlTablosus.ToList();
            List<IlceTablosu> ilceler = e.IlceTablosus.ToList();
            List<MahalleTablosu> mahaller = e.MahalleTablosus.ToList();
            List<IlanTuruTablosu> ilanTurleri = e.IlanTuruTablosus.ToList();
            List<KonutTuruTablosu> konutTurleri = e.KonutTuruTablosus.ToList();
            List<BinaYasiTablosu> yaslar = e.BinaYasiTablosus.ToList();
            List<IsinmaTablosu> isinmalar = e.IsinmaTablosus.ToList();
            List<ResimTablosu> resimler = e.ResimTablosus.ToList();

            IEnumerable<ResimTablosu> ilkResim = resimler.GroupBy(x => x.resimIlanID).Select(group => group.First());


            var TumIlanlar = from i in ilanlarim
                             join u in uyeler on i.ilanUyeID equals u.uyeID into table1
                             from u in table1.ToList()
                             join a in adresler on i.ilanAdresID equals a.adresID into table2
                             from a in table2.ToList()
                             join il in iller on a.adresIlID equals il.ilID into table3
                             from il in table3.ToList()
                             join ilc in ilceler on a.adresIlceID equals ilc.ilceID into table4
                             from ilc in table4.ToList()
                             join m in mahaller on a.adresMahalleID equals m.mahalleID into table5
                             from m in table5.ToList()
                             join it in ilanTurleri on i.ilanTuruID equals it.ilanTuruID into table6
                             from it in table6.ToList()
                             join k in konutTurleri on i.ilanKonutTuruID equals k.konutTuruID into table7
                             from k in table7.ToList()
                             join y in yaslar on i.ilanBinaYasiID equals y.binaYasiID into table8
                             from y in table8.ToList()
                             join isi in isinmalar on i.ilanIsinmaID equals isi.isinmaID into table9
                             from isi in table9.ToList()
                             join r in ilkResim on i.ilanID equals r.resimIlanID into table10
                             from r in table10.ToList()
                             select new TumIlanBilgileri
                             {
                                 ilan = i,
                                 uye = u,
                                 adres = a,
                                 ils = il,
                                 ilce = ilc,
                                 mahal = m,
                                 ilanTur = it,
                                 konutTur = k,
                                 yas = y,
                                 isin = isi,
                                 resim = r,
                             };
            return View(TumIlanlar);
        }

        public ActionResult Satilik()
        {
            ViewBag.iller = e.IlTablosus.ToList();
            ViewBag.ilceler = e.IlceTablosus.ToList();
            ViewBag.mahalleler = e.MahalleTablosus.ToList();
            ViewBag.konutTurleri = e.KonutTuruTablosus.ToList();

            List<IlanTablosu> satilik = e.IlanTablosus.Where(x => x.ilanTuruID == 2 && x.ilanOnayDurumu == 1).ToList<IlanTablosu>();

            List<IlanTablosu> ilanlar = e.IlanTablosus.ToList();
            List<UyeTablosu> uyeler = e.UyeTablosus.ToList();
            List<AdresTablosu> adresler = e.AdresTablosus.ToList();
            List<IlTablosu> iller = e.IlTablosus.ToList();
            List<IlceTablosu> ilceler = e.IlceTablosus.ToList();
            List<MahalleTablosu> mahaller = e.MahalleTablosus.ToList();
            List<IlanTuruTablosu> ilanTurleri = e.IlanTuruTablosus.ToList();
            List<KonutTuruTablosu> konutTurleri = e.KonutTuruTablosus.ToList();
            List<BinaYasiTablosu> yaslar = e.BinaYasiTablosus.ToList();
            List<IsinmaTablosu> isinmalar = e.IsinmaTablosus.ToList();
            List<ResimTablosu> resimler = e.ResimTablosus.ToList();

            IEnumerable<ResimTablosu> ilkResim = resimler.GroupBy(x => x.resimIlanID).Select(group => group.First());

            var TumIlanlar = from i in satilik
                             join u in uyeler on i.ilanUyeID equals u.uyeID into table1
                             from u in table1.ToList()
                             join a in adresler on i.ilanAdresID equals a.adresID into table2
                             from a in table2.ToList()
                             join il in iller on a.adresIlID equals il.ilID into table3
                             from il in table3.ToList()
                             join ilc in ilceler on a.adresIlceID equals ilc.ilceID into table4
                             from ilc in table4.ToList()
                             join m in mahaller on a.adresMahalleID equals m.mahalleID into table5
                             from m in table5.ToList()
                             join it in ilanTurleri on i.ilanTuruID equals it.ilanTuruID into table6
                             from it in table6.ToList()
                             join k in konutTurleri on i.ilanKonutTuruID equals k.konutTuruID into table7
                             from k in table7.ToList()
                             join y in yaslar on i.ilanBinaYasiID equals y.binaYasiID into table8
                             from y in table8.ToList()
                             join isi in isinmalar on i.ilanIsinmaID equals isi.isinmaID into table9
                             from isi in table9.ToList()
                             join r in ilkResim on i.ilanID equals r.resimIlanID into table10
                             from r in table10.ToList()
                             select new TumIlanBilgileri
                             {
                                 ilan = i,
                                 uye = u,
                                 adres = a,
                                 ils = il,
                                 ilce = ilc,
                                 mahal = m,
                                 ilanTur = it,
                                 konutTur = k,
                                 yas = y,
                                 isin = isi,
                                 resim = r
                             };
            return View(TumIlanlar);
        }
        [HttpPost]
        public ActionResult Satilik(TumIlanBilgileri ara)
        {
            ViewBag.iller = e.IlTablosus.ToList();
            ViewBag.ilceler = e.IlceTablosus.ToList();
            ViewBag.mahalleler = e.MahalleTablosus.ToList();
            ViewBag.konutTurleri = e.KonutTuruTablosus.ToList();

            List<IlanTablosu> ilanlar = e.IlanTablosus.Where(x => x.ilanKonutTuruID == ara.ilan.ilanKonutTuruID && x.ilanTuruID == ara.ilan.ilanTuruID && x.ilanOdaSayisi == ara.ilan.ilanOdaSayisi && x.ilanFiyat <= ara.ilan.ilanFiyat && x.ilanOnayDurumu == 1).ToList<IlanTablosu>();
            List<UyeTablosu> uyeler = e.UyeTablosus.ToList();
            List<AdresTablosu> adresler = e.AdresTablosus.Where(x => x.adresIlID == ara.adres.adresIlID && x.adresIlceID == ara.adres.adresIlceID && x.adresMahalleID == ara.adres.adresMahalleID).ToList<AdresTablosu>();
            List<IlTablosu> iller = e.IlTablosus.ToList();
            List<IlceTablosu> ilceler = e.IlceTablosus.ToList();
            List<MahalleTablosu> mahaller = e.MahalleTablosus.ToList();
            List<IlanTuruTablosu> ilanTurleri = e.IlanTuruTablosus.ToList();
            List<KonutTuruTablosu> konutTurleri = e.KonutTuruTablosus.ToList();
            List<BinaYasiTablosu> yaslar = e.BinaYasiTablosus.ToList();
            List<IsinmaTablosu> isinmalar = e.IsinmaTablosus.ToList();
            List<ResimTablosu> resimler = e.ResimTablosus.ToList();

            IEnumerable<ResimTablosu> ilkResim = resimler.GroupBy(x => x.resimIlanID).Select(group => group.First());

            var TumIlanlar = from i in ilanlar
                             join u in uyeler on i.ilanUyeID equals u.uyeID into table1
                             from u in table1.ToList()
                             join a in adresler on i.ilanAdresID equals a.adresID into table2
                             from a in table2.ToList()
                             join il in iller on a.adresIlID equals il.ilID into table3
                             from il in table3.ToList()
                             join ilc in ilceler on a.adresIlceID equals ilc.ilceID into table4
                             from ilc in table4.ToList()
                             join m in mahaller on a.adresMahalleID equals m.mahalleID into table5
                             from m in table5.ToList()
                             join it in ilanTurleri on i.ilanTuruID equals it.ilanTuruID into table6
                             from it in table6.ToList()
                             join k in konutTurleri on i.ilanKonutTuruID equals k.konutTuruID into table7
                             from k in table7.ToList()
                             join y in yaslar on i.ilanBinaYasiID equals y.binaYasiID into table8
                             from y in table8.ToList()
                             join isi in isinmalar on i.ilanIsinmaID equals isi.isinmaID into table9
                             from isi in table9.ToList()
                             join r in ilkResim on i.ilanID equals r.resimIlanID into table10
                             from r in table10.ToList()
                             select new TumIlanBilgileri
                             {
                                 ilan = i,
                                 uye = u,
                                 adres = a,
                                 ils = il,
                                 ilce = ilc,
                                 mahal = m,
                                 ilanTur = it,
                                 konutTur = k,
                                 yas = y,
                                 isin = isi,
                                 resim = r
                             };
            return View(TumIlanlar);
        }

        public ActionResult Kiralik()
        {
            ViewBag.iller = e.IlTablosus.ToList();
            ViewBag.ilceler = e.IlceTablosus.ToList();
            ViewBag.mahalleler = e.MahalleTablosus.ToList();
            ViewBag.konutTurleri = e.KonutTuruTablosus.ToList();

            List<IlanTablosu> kiralik = e.IlanTablosus.Where(x => x.ilanTuruID == 1 && x.ilanOnayDurumu == 1).ToList<IlanTablosu>();

            List<IlanTablosu> ilanlar = e.IlanTablosus.ToList();
            List<UyeTablosu> uyeler = e.UyeTablosus.ToList();
            List<AdresTablosu> adresler = e.AdresTablosus.ToList();
            List<IlTablosu> iller = e.IlTablosus.ToList();
            List<IlceTablosu> ilceler = e.IlceTablosus.ToList();
            List<MahalleTablosu> mahaller = e.MahalleTablosus.ToList();
            List<IlanTuruTablosu> ilanTurleri = e.IlanTuruTablosus.ToList();
            List<KonutTuruTablosu> konutTurleri = e.KonutTuruTablosus.ToList();
            List<BinaYasiTablosu> yaslar = e.BinaYasiTablosus.ToList();
            List<IsinmaTablosu> isinmalar = e.IsinmaTablosus.ToList();
            List<ResimTablosu> resimler = e.ResimTablosus.ToList();

            IEnumerable<ResimTablosu> ilkResim = resimler.GroupBy(x => x.resimIlanID).Select(group => group.First());

            var TumIlanlar = from i in kiralik
                             join u in uyeler on i.ilanUyeID equals u.uyeID into table1
                             from u in table1.ToList()
                             join a in adresler on i.ilanAdresID equals a.adresID into table2
                             from a in table2.ToList()
                             join il in iller on a.adresIlID equals il.ilID into table3
                             from il in table3.ToList()
                             join ilc in ilceler on a.adresIlceID equals ilc.ilceID into table4
                             from ilc in table4.ToList()
                             join m in mahaller on a.adresMahalleID equals m.mahalleID into table5
                             from m in table5.ToList()
                             join it in ilanTurleri on i.ilanTuruID equals it.ilanTuruID into table6
                             from it in table6.ToList()
                             join k in konutTurleri on i.ilanKonutTuruID equals k.konutTuruID into table7
                             from k in table7.ToList()
                             join y in yaslar on i.ilanBinaYasiID equals y.binaYasiID into table8
                             from y in table8.ToList()
                             join isi in isinmalar on i.ilanIsinmaID equals isi.isinmaID into table9
                             from isi in table9.ToList()
                             join r in ilkResim on i.ilanID equals r.resimIlanID into table10
                             from r in table10.ToList()
                             select new TumIlanBilgileri
                             {
                                 ilan = i,
                                 uye = u,
                                 adres = a,
                                 ils = il,
                                 ilce = ilc,
                                 mahal = m,
                                 ilanTur = it,
                                 konutTur = k,
                                 yas = y,
                                 isin = isi,
                                 resim = r
                             };
            return View(TumIlanlar);
        }

        [HttpPost]
        public ActionResult Kiralik(TumIlanBilgileri ara)
        {
            ViewBag.iller = e.IlTablosus.ToList();
            ViewBag.ilceler = e.IlceTablosus.ToList();
            ViewBag.mahalleler = e.MahalleTablosus.ToList();
            ViewBag.konutTurleri = e.KonutTuruTablosus.ToList();

            List<IlanTablosu> ilanlar = e.IlanTablosus.Where(x => x.ilanKonutTuruID == ara.ilan.ilanKonutTuruID && x.ilanTuruID == ara.ilan.ilanTuruID && x.ilanOdaSayisi == ara.ilan.ilanOdaSayisi && x.ilanFiyat <= ara.ilan.ilanFiyat && x.ilanOnayDurumu == 1).ToList<IlanTablosu>();
            List<UyeTablosu> uyeler = e.UyeTablosus.ToList();
            List<AdresTablosu> adresler = e.AdresTablosus.Where(x => x.adresIlID == ara.adres.adresIlID && x.adresIlceID == ara.adres.adresIlceID && x.adresMahalleID == ara.adres.adresMahalleID).ToList<AdresTablosu>();
            List<IlTablosu> iller = e.IlTablosus.ToList();
            List<IlceTablosu> ilceler = e.IlceTablosus.ToList();
            List<MahalleTablosu> mahaller = e.MahalleTablosus.ToList();
            List<IlanTuruTablosu> ilanTurleri = e.IlanTuruTablosus.ToList();
            List<KonutTuruTablosu> konutTurleri = e.KonutTuruTablosus.ToList();
            List<BinaYasiTablosu> yaslar = e.BinaYasiTablosus.ToList();
            List<IsinmaTablosu> isinmalar = e.IsinmaTablosus.ToList();
            List<ResimTablosu> resimler = e.ResimTablosus.ToList();

            IEnumerable<ResimTablosu> ilkResim = resimler.GroupBy(x => x.resimIlanID).Select(group => group.First());

            var TumIlanlar = from i in ilanlar
                             join u in uyeler on i.ilanUyeID equals u.uyeID into table1
                             from u in table1.ToList()
                             join a in adresler on i.ilanAdresID equals a.adresID into table2
                             from a in table2.ToList()
                             join il in iller on a.adresIlID equals il.ilID into table3
                             from il in table3.ToList()
                             join ilc in ilceler on a.adresIlceID equals ilc.ilceID into table4
                             from ilc in table4.ToList()
                             join m in mahaller on a.adresMahalleID equals m.mahalleID into table5
                             from m in table5.ToList()
                             join it in ilanTurleri on i.ilanTuruID equals it.ilanTuruID into table6
                             from it in table6.ToList()
                             join k in konutTurleri on i.ilanKonutTuruID equals k.konutTuruID into table7
                             from k in table7.ToList()
                             join y in yaslar on i.ilanBinaYasiID equals y.binaYasiID into table8
                             from y in table8.ToList()
                             join isi in isinmalar on i.ilanIsinmaID equals isi.isinmaID into table9
                             from isi in table9.ToList()
                             join r in ilkResim on i.ilanID equals r.resimIlanID into table10
                             from r in table10.ToList()
                             select new TumIlanBilgileri
                             {
                                 ilan = i,
                                 uye = u,
                                 adres = a,
                                 ils = il,
                                 ilce = ilc,
                                 mahal = m,
                                 ilanTur = it,
                                 konutTur = k,
                                 yas = y,
                                 isin = isi,
                                 resim = r
                             };
            return View(TumIlanlar);
        }

        [Authorize(Roles = "1")]
        public ActionResult Onay()
        {
            List<IlanTablosu> onay = e.IlanTablosus.Where(x => x.ilanOnayDurumu == 0).ToList<IlanTablosu>();

            List<IlanTablosu> ilanlar = e.IlanTablosus.ToList();
            List<UyeTablosu> uyeler = e.UyeTablosus.ToList();
            List<AdresTablosu> adresler = e.AdresTablosus.ToList();
            List<IlTablosu> iller = e.IlTablosus.ToList();
            List<IlceTablosu> ilceler = e.IlceTablosus.ToList();
            List<MahalleTablosu> mahaller = e.MahalleTablosus.ToList();
            List<IlanTuruTablosu> ilanTurleri = e.IlanTuruTablosus.ToList();
            List<KonutTuruTablosu> konutTurleri = e.KonutTuruTablosus.ToList();
            List<BinaYasiTablosu> yaslar = e.BinaYasiTablosus.ToList();
            List<IsinmaTablosu> isinmalar = e.IsinmaTablosus.ToList();
            List<ResimTablosu> resimler = e.ResimTablosus.ToList();

            IEnumerable<ResimTablosu> ilkResim = resimler.GroupBy(x => x.resimIlanID).Select(group => group.First());

            var TumIlanlar = from i in onay
                             join u in uyeler on i.ilanUyeID equals u.uyeID into table1
                             from u in table1.ToList()
                             join a in adresler on i.ilanAdresID equals a.adresID into table2
                             from a in table2.ToList()
                             join il in iller on a.adresIlID equals il.ilID into table3
                             from il in table3.ToList()
                             join ilc in ilceler on a.adresIlceID equals ilc.ilceID into table4
                             from ilc in table4.ToList()
                             join m in mahaller on a.adresMahalleID equals m.mahalleID into table5
                             from m in table5.ToList()
                             join it in ilanTurleri on i.ilanTuruID equals it.ilanTuruID into table6
                             from it in table6.ToList()
                             join k in konutTurleri on i.ilanKonutTuruID equals k.konutTuruID into table7
                             from k in table7.ToList()
                             join y in yaslar on i.ilanBinaYasiID equals y.binaYasiID into table8
                             from y in table8.ToList()
                             join isi in isinmalar on i.ilanIsinmaID equals isi.isinmaID into table9
                             from isi in table9.ToList()
                             join r in ilkResim on i.ilanID equals r.resimIlanID into table10
                             from r in table10.ToList()
                             select new TumIlanBilgileri
                             {
                                 ilan = i,
                                 uye = u,
                                 adres = a,
                                 ils = il,
                                 ilce = ilc,
                                 mahal = m,
                                 ilanTur = it,
                                 konutTur = k,
                                 yas = y,
                                 isin = isi,
                                 resim = r
                             };
            return View(TumIlanlar);
        }

        [HttpPost]
        public void Onay(int id)
        {
            IlanTablosu ilan = e.IlanTablosus.FirstOrDefault(x => x.ilanID == id);
            ilan.ilanOnayDurumu = 1;
            e.IlanTablosus.AddOrUpdate(ilan);
            e.SaveChanges();
        }

        public ActionResult IlanDetay(int id)
        {
            IlanTablosu ilanDetay = e.IlanTablosus.FirstOrDefault(x => x.ilanID == id);
            ViewBag.ilan = ilanDetay;
            AdresTablosu adresD = e.AdresTablosus.FirstOrDefault(x => x.adresID == ilanDetay.ilanAdresID);
            ViewBag.adres = adresD.adresDetay;
            IlTablosu ilDetay = e.IlTablosus.FirstOrDefault(x => x.ilID == adresD.adresIlID);
            ViewBag.il = ilDetay.ilAdi;
            IlceTablosu ilceDetay = e.IlceTablosus.FirstOrDefault(x => x.ilceID == adresD.adresIlceID);
            ViewBag.ilce = ilceDetay.ilceAdi;
            MahalleTablosu mahalleDetay = e.MahalleTablosus.FirstOrDefault(x => x.mahalleID == adresD.adresMahalleID);
            ViewBag.mahalle = mahalleDetay.mahalleAdi;
            UyeTablosu uyeDetay = e.UyeTablosus.FirstOrDefault(x => x.uyeID == ilanDetay.ilanUyeID);
            ViewBag.uye = uyeDetay.uyeAdSoyad;
            IlanTuruTablosu ilanTurDetay = e.IlanTuruTablosus.FirstOrDefault(x => x.ilanTuruID == ilanDetay.ilanTuruID);
            ViewBag.ilanturu = ilanTurDetay.ilanTuru;
            KonutTuruTablosu konutDetay = e.KonutTuruTablosus.FirstOrDefault(x => x.konutTuruID == ilanDetay.ilanKonutTuruID);
            ViewBag.konut = konutDetay.konutTuru;
            BinaYasiTablosu yasDetay = e.BinaYasiTablosus.FirstOrDefault(x => x.binaYasiID == ilanDetay.ilanBinaYasiID);
            ViewBag.yas = yasDetay.binaYasi;
            IsinmaTablosu isiDetay = e.IsinmaTablosus.FirstOrDefault(x => x.isinmaID == ilanDetay.ilanIsinmaID);
            ViewBag.isi = isiDetay.isinmaTuru;

            IEnumerable<ResimTablosu> resimDetay = e.ResimTablosus.Where(x => x.resimIlanID == ilanDetay.ilanID).ToList();
            return View(resimDetay);
        }
    }

}
