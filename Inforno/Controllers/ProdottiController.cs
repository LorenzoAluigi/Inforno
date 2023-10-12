using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Inforno.Models;

namespace Inforno.Controllers
{
    public class ProdottiController : Controller
    {
        private ModelDBContext db = new ModelDBContext();

       
        public ActionResult Index()
        {
            return View(db.Prodotti.ToList());
        }

        
        public ActionResult Details(int id)
        {
            
            Prodotti prodotti = db.Prodotti.Find(id);
           
            return View(prodotti);
        }

       
        public ActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Prodotti prodotto, HttpPostedFileBase Foto)
        {


            if (ModelState.IsValid)
            {
                if (Foto!= null)
                {
                    string ext = Path.GetExtension(Foto.FileName);
                    string nomeFile = Foto.FileName;
                    string pathToSave = Path.Combine(Server.MapPath("~/Content/Upload"), nomeFile);
                    Foto.SaveAs(pathToSave);
                    prodotto.Foto = Foto.FileName;
                }
                else
                {
                    prodotto.Foto = "";
                }

                db.Prodotti.Add(prodotto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(prodotto);
        }

        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prodotti prodotti = db.Prodotti.Find(id);
            if (prodotti == null)
            {
                return HttpNotFound();
            }
            return View(prodotti);
        }

 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Prodotti prodotti)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prodotti).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(prodotti);
        }

        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prodotti prodotti = db.Prodotti.Find(id);
            if (prodotti == null)
            {
                return HttpNotFound();
            }
            return View(prodotti);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prodotti prodotti = db.Prodotti.Find(id);
            db.Prodotti.Remove(prodotti);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
