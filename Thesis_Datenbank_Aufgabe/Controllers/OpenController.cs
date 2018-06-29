using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Thesis_Datenbank_Aufgabe.Models;

namespace Thesis_Datenbank_Aufgabe.Controllers
{
    public class OpenController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        

        // GET: Open
        public ActionResult Index()
        {
            //hier nur abgegebene Thesen übergeben
            List<Thesis> thesisliste = new List<Thesis>();
            foreach (Thesis thesis in db.ThesisDb.ToList())
            {
                if (thesis.Status.Equals(Models.Status.frei))
                {
                    thesisliste.Add(thesis);
                }
            }
            return View(thesisliste);
        }

        // GET: Open/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Thesis thesis = db.ThesisDb.Find(id);
            if (thesis == null)
            {
                return HttpNotFound();
            }
            return View(thesis);
        }

        // GET: Open/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Open/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,Bachelor,Master,Status,StudentName,StudentEmail,StudentID, Registration,Filing,Typ,Summary,Strenghts,Weaknesses,Evaluation,ContentVal,LayoutVal,StructureVal,StyleVal,LiteraturVal,DifficultyVal,NoveltyVal,RichnessVal,ContentWt,LayoutWt,StructureWt,StyleWt,LiteratureWt,DifficultyWt,NoveltyWt,RichnessWt,Grade,LastModified,Creator")] Thesis thesis)
        {
            //Man muss zum erstellen angemeldet sein
            /*
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated==false)
            {
                throw new Exception("Bitte zum erstellen einer Thesis einloggen bzw. registrieren");
            }
            */
            if (ModelState.IsValid)
            {
                db.ThesisDb.Add(thesis);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(thesis);
        }

        // GET: Open/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Environment.UserDomainName != db.ThesisDb.Find(id).Creator)
            {
                throw new Exception("Es dürfen nur selbst angelegte Themen geaendert werden");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Thesis thesis = db.ThesisDb.Find(id);
            if (thesis == null)
            {
                return HttpNotFound();
            }
            return View(thesis);
        }

        // POST: Open/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,Bachelor,Master,Status,StudentName,StudentEmail,StudentID,Registration,Filing,Typ,Summary,Strenghts,Weaknesses,Evaluation,ContentVal,LayoutVal,StructureVal,StyleVal,LiteraturVal,DifficultyVal,NoveltyVal,RichnessVal,ContentWt,LayoutWt,StructureWt,StyleWt,LiteratureWt,DifficultyWt,NoveltyWt,RichnessWt,Grade,LastModified,Creator")] Thesis thesis)
        {

            
            if (ModelState.IsValid)
            {thesis.LastModified = DateTime.Now;
                db.Entry(thesis).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(thesis);
        }

        // GET: Open/Delete/5
        public ActionResult Delete(int? id)
        {
          if (Environment.UserDomainName != db.ThesisDb.Find(id).Creator)
            {
                throw new Exception("Es dürfen nur selbst angelegte Themen geloescht werden");
            } 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Thesis thesis = db.ThesisDb.Find(id);
            if (thesis == null)
            {
                return HttpNotFound();
            }
            return View(thesis);
        }

        // POST: Open/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            
            Thesis thesis = db.ThesisDb.Find(id);
            db.ThesisDb.Remove(thesis);
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
