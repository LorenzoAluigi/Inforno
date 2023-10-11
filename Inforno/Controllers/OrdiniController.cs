using Inforno.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Inforno.Controllers
{
    public class OrdiniController : Controller
    {
        private ModelDBContext db = new ModelDBContext();
        // GET: Ordini
        public ActionResult OrdiniAdmin()
        {
            return View(db.Ordini.ToList());
        }

        [HttpPost]
        public ActionResult OrdiniAdmin(Ordini ordine)
        {
            return View();
        }


        public ActionResult AddOrdini()
        {
            string mail = User.Identity.Name;
            ModelDBContext dbContext = new ModelDBContext();
            Users user = dbContext.Users.FirstOrDefault(e => e.Email == mail);
            Ordini ordine = new Ordini();
            ordine.IDUser = user.IDUser;
            ordine.DataOrdine = DateTime.Now;
            ordine.IndirizzoSpedizione = $"{user.Indirizzo} - {user.Citta} {user.Provincia}";

            List<Prodotti> Pizze = db.Prodotti.ToList();
            ViewBag.Pizze = Pizze;
            if (Session["Carrello"] != null)
            {
                List<Prodotti> carrello = new List<Prodotti>();
                carrello = Session["Carrello"] as List<Prodotti>;
                ViewBag.carrello = carrello;
            }

            return View(ordine);
        }
        [HttpPost]
        public ActionResult AddOrdini(Ordini ordine)
        {
            List<Prodotti> Pizze = db.Prodotti.ToList();
            ViewBag.Pizze = Pizze;

            



            return View() ;
        }



            public ActionResult elencoPizze()
        {
            List<Prodotti> data = db.Prodotti.ToList();


            return PartialView("elencoPizze", data);
        }

        //[HttpPost]
        //public JsonResult AggiungiArticolo(ArticoliOrdine nuovoArticolo)
        //{
        //    var data = new { nome = "prova" };
        //    return Json(data);
        //}

        public ActionResult creaArticoloOrdine(int id, int quantita)
        {
            Prodotti pizza = db.Prodotti.FirstOrDefault(e => e.IDProdotto == id) as Prodotti;

            if (Session["carrello"] != null) 
            {
                List<Prodotti> carrello = new List<Prodotti>();
                carrello = Session["Carrello"] as List<Prodotti>;
                carrello.Add(pizza);
                Session["Carrello"] = carrello;
            }
            else 
            {
                List<Prodotti> carrello = new List<Prodotti>();
                carrello.Add(pizza);
                Session["Carrello"] = carrello;
            }

            var response = new
            {
                pizza.IDProdotto,
                pizza.Nome,
                pizza.Foto,
                pizza.PrezzoVendita,
                pizza.TempoConsegna,
                pizza.Ingredienti,
                Quantita = quantita,
                totaleArticolo = quantita*pizza.PrezzoVendita,
                
            };

            return Json(response, JsonRequestBehavior.AllowGet);

        }
    }
}