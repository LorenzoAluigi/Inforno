using Inforno.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Inforno.Controllers
{
    public class OrdiniController : Controller
    {
        private ModelDBContext db = new ModelDBContext();
        

        public ActionResult OrdiniAdmin()
        {
            DateTime today = DateTime.Today;
            var ordini = db.Ordini.Where(e => !e.IsEvaso && DbFunctions.TruncateTime(e.DataOrdine) == today).OrderBy(e => e.DataOrdine).ToList() as List<Ordini>;
            return View(ordini);
        }

       
        public ActionResult OrdiniUser()
        {
            string mail = User.Identity.Name;
            
            List<Ordini> ordinyByUser = db.Ordini.Where(e=>e.Users.Email == mail ).ToList();


            return View(ordinyByUser);
        }


        public ActionResult AddOrdini()
        {
            string mail = User.Identity.Name;
            Users user = db.Users.FirstOrDefault(e => e.Email == mail);
            Ordini ordine = new Ordini();
            ordine.IDUser = user.IDUser;
            ordine.DataOrdine = DateTime.Now;
            ordine.IndirizzoSpedizione = $"{user.Indirizzo} - {user.Citta} {user.Provincia}";
            

            List<Prodotti> Pizze = db.Prodotti.ToList();
            ViewBag.Pizze = Pizze;
            if (Session["Carrello"] != null)
            {
                List<ArticoliOrdine> carrello = new List<ArticoliOrdine>();
                carrello = Session["Carrello"] as List<ArticoliOrdine>;
                ViewBag.Carrello = carrello;
               

            }

            return View(ordine);
        }
        [HttpPost]
        public ActionResult AddOrdini(Ordini ordine)
        {
            List<Prodotti> Pizze = db.Prodotti.ToList();
            ViewBag.Pizze = Pizze;

            if (ModelState.IsValid) 
            {
                if (Session["carrello"] != null) 
                { 
                    db.Ordini.Add(ordine);
                    db.SaveChanges();
              
                    List<ArticoliOrdine> carrello = Session["Carrello"] as List<ArticoliOrdine>;
                    foreach (var item in carrello)
                    {
                        ArticoliOrdine articolo = item;
                        articolo.IDOrdine = ordine.IDOrdine;
                        articolo.Prodotti = null;  
                        db.ArticoliOrdine.Add(articolo);
                        db.SaveChanges();
                    }
                    db.Dispose();
                }
                else 
                {
                    ModelState.AddModelError("", "inserire almeno un articolo nel carrello");
                    return View(ordine);
                }
            }
            else 
            {
                
                return View(ordine);
            }

            return View(ordine) ;
        }


        public ActionResult creaArticoloOrdine(int id, int quantita)
        {
            Prodotti pizza = db.Prodotti.FirstOrDefault(e => e.IDProdotto == id);
            if (Session["Carrello"] == null)
            {
                Session["Carrello"] = new List<ArticoliOrdine>();
            }

            List<ArticoliOrdine> carrello = Session["Carrello"] as List<ArticoliOrdine>;

            ArticoliOrdine existing = carrello.FirstOrDefault(item => item.IDProdotto == id);
            
            if(existing != null)
            {
                
                existing.Quantita += quantita;
            }
            else
            {

            ArticoliOrdine newArticolo = new ArticoliOrdine();

            newArticolo.IDProdotto = pizza.IDProdotto;
            newArticolo.Quantita = quantita;
            newArticolo.PrezzoUnitario = pizza.PrezzoVendita;
            newArticolo.Prodotti = db.Prodotti.Find(newArticolo.IDProdotto);
            carrello.Add(newArticolo);
            }

            Session["Carrello"] = carrello;
            

            var response = new
            {

                Quantita = quantita,
                PrezzoUnitario = pizza.PrezzoVendita,
                totaleArticolo = quantita* pizza.PrezzoVendita,

                prodotto= new
                {
                pizza.IDProdotto,
                pizza.Nome,
                pizza.Foto,
                pizza.PrezzoVendita,
                pizza.TempoConsegna,
                pizza.Ingredienti
                }
            };

            return Json(response, JsonRequestBehavior.AllowGet);

        }

        public ActionResult evadiOrdine(bool Checked, int IdOrdine)
        {

            Ordini ordine = db.Ordini.FirstOrDefault(e => e.IDOrdine == IdOrdine);

            ordine.IsEvaso = Checked;
            db.Entry(ordine).State = EntityState.Modified;
            db.SaveChanges();

            var response = "db change ok";
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult editOrdineUser(int id) 
        {
            List<Prodotti> Pizze = db.Prodotti.ToList();
            ViewBag.Pizze = Pizze;
            Ordini ordine=db.Ordini.Find(id);


            ViewBag.Carrello = db.ArticoliOrdine.Where(e=>e.IDOrdine==ordine.IDOrdine).ToList(); 



            return View(ordine);
        
        }

        


        [HttpPost]
        public ActionResult editOrdineUser(Ordini ordine)
        {
            List<Prodotti> Pizze = db.Prodotti.ToList();
            ViewBag.Pizze = Pizze;

            return View(ordine);

        }




        //ajax

        public ActionResult contaOrdiniEvasi()
        {
            DateTime today = DateTime.Today;
            var response = db.Ordini.Count(e => e.IsEvaso && DbFunctions.TruncateTime(e.DataOrdine) == today);
            
            return Json(response, JsonRequestBehavior.AllowGet);

        }

        public ActionResult IncassoAllaData(DateTime date)
        {
            
            var ordini = db.Ordini.Where(e => DbFunctions.TruncateTime(e.DataOrdine) == date).ToList() as List<Ordini>;
            decimal response = 0 ;
            foreach (var item in ordini)
            {
                foreach (var e in item.ArticoliOrdine) 
                {
                    response += (e.PrezzoUnitario * e.Quantita);
                
                }
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        

    }
}