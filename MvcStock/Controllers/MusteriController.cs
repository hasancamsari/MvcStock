using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStock.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MvcStock.Controllers
{
    public class MusteriController : Controller
    {
        // GET: Musteri
        MvcDbStockEntities db = new MvcDbStockEntities();
        public ActionResult Index(/*int sayfa = 1*/ string p)
        {
            var degerler = from d in db.TBLMUSTERILER select d;
            if (!string.IsNullOrEmpty(p))
            {
                degerler = degerler.Where(x => x.MUSTERIAD.Contains(p));
            }
            //var degerler = db.TBLMUSTERILER.ToList().ToPagedList(sayfa,4);
            //var degerler = db.TBLMUSTERILER.ToList();
            ViewBag.Arama = degerler;
            return View(degerler.ToList());
        }
        [HttpGet]
        public ActionResult YeniMusteri()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniMusteri(TBLMUSTERILER p1)
        {
            if (!ModelState.IsValid) 
            {
                return View("YeniMusteri");
            }
            db.TBLMUSTERILER.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");  
        }

        public ActionResult SIL(int id)
        {
            var musteri = db.TBLMUSTERILER.Find(id);
            db.TBLMUSTERILER.Remove(musteri);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult MusteriGetir(int id)
        {
            var must = db.TBLMUSTERILER.Find(id);
            return View("MusteriGetir",must);
        }

        public ActionResult Guncelle(TBLMUSTERILER p1)
        {
            var mstr = db.TBLMUSTERILER.Find(p1.MUSTERIID);
            mstr.MUSTERIAD = p1.MUSTERIAD; 
            mstr.MUSTERISOYAD = p1.MUSTERISOYAD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}