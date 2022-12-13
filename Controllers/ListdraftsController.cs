using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BoitMail.Data;
using BoitMail.Models;

namespace BoitMail.Controllers
{
    public class ListdraftsController : Controller
    {
        private BoitMailContext db = new BoitMailContext();

        // GET: Listdrafts
        public async Task<ActionResult> Index()
        {
            var listdrafts = db.Listdrafts.Include(l => l.Mail);
            return View(await listdrafts.ToListAsync());
        }

        // GET: Listdrafts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Listdraft listdraft = await db.Listdrafts.FindAsync(id);
            if (listdraft == null)
            {
                return HttpNotFound();
            }
            return View(listdraft);
        }

        // GET: Listdrafts/Create
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.Mails, "Id", "to");
            return View();
        }

        // POST: Listdrafts/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Idmail,datemail")] Listdraft listdraft)
        {
            if (ModelState.IsValid)
            {
                db.Listdrafts.Add(listdraft);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.Mails, "Id", "to", listdraft.Id);
            return View(listdraft);
        }

        // GET: Listdrafts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Listdraft listdraft = await db.Listdrafts.FindAsync(id);
            if (listdraft == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.Mails, "Id", "to", listdraft.Id);
            return View(listdraft);
        }

        // POST: Listdrafts/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Idmail,datemail")] Listdraft listdraft)
        {
            if (ModelState.IsValid)
            {
                db.Entry(listdraft).State = System.Data.Entity.EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.Mails, "Id", "to", listdraft.Id);
            return View(listdraft);
        }

        // GET: Listdrafts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Listdraft listdraft = await db.Listdrafts.FindAsync(id);
            if (listdraft == null)
            {
                return HttpNotFound();
            }
            return View(listdraft);
        }

        // POST: Listdrafts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Listdraft listdraft = await db.Listdrafts.FindAsync(id);
            db.Listdrafts.Remove(listdraft);
            await db.SaveChangesAsync();
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
