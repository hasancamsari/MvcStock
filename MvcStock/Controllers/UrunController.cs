using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using MvcStock.Models.Entity;

namespace MvcStock.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        MvcDbStockEntities db = new MvcDbStockEntities ();//database bağlama
        public ActionResult Index()
        {
            var degeler = db.TBLURUNLER.ToList();
            return View(degeler);
        }

        [HttpGet]
        public ActionResult UrunEkle()
        {  
            List<SelectListItem> degerler = (from i in db.TBLKATEGORILER.ToList()
                                             select new SelectListItem
                                             { 
                                                Text = i.KATAGORIAD,
                                                Value = i.KATEGORIID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;//çantaya atıp view e taşıdı
            return View(); 
        }

        [HttpPost]
        public ActionResult UrunEkle(TBLURUNLER p1)
        {   
            var ktg= db.TBLKATEGORILER.Where(m=>m.KATEGORIID==p1.TBLKATEGORILER.KATEGORIID).FirstOrDefault();//seçilen ilk değeri getirir
            p1.TBLKATEGORILER = ktg;
            db.TBLURUNLER.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");//geri indexe gönderiyor 
        }

        public ActionResult SIL(int id)
        {
            var urun = db.TBLURUNLER.Find(id);
            db.TBLURUNLER.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrunGetir(int id)
        {
            List<SelectListItem> degerler = (from i in db.TBLKATEGORILER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATAGORIAD,
                                                 Value = i.KATEGORIID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;

            var urun = db.TBLURUNLER.Find(id);
            return View("UrunGetir", urun);
        }

        public ActionResult Guncelle(TBLURUNLER p)
        {
            var urn = db.TBLURUNLER.Find(p.URUNID);
            urn.URUNID = p.URUNID;
            urn.URUNAD = p.URUNAD;
            urn.MARKA =  p.MARKA;
            //urn.URUNKATAGORI = p1.URUNKATAGORI;
            urn.FIYAT = p.FIYAT;
            urn.STOK = p.STOK;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}