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
    public class ListsendsController : Controller
    {
        private BoitMailContext db = new BoitMailContext();

        // GET: Listsends
        public async Task<ActionResult> Index()
        {
            var listsends = db.Listsends.Include(l => l.Mail);
            return View(await listsends.ToListAsync());
        }

        // GET: Listsends/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Listsend listsend = await db.Listsends.FindAsync(id);
            if (listsend == null)
            {
                return HttpNotFound();
            }
            return View(listsend);
        }

        // GET: Listsends/Create
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.Mails, "Id", "to");
            return View();
        }

        // POST: Listsends/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Idmail,datemail")] Listsend listsend)
        {
            if (ModelState.IsValid)
            {
                db.Listsends.Add(listsend);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.Mails, "Id", "to", listsend.Id);
            return View(listsend);
        }

        // GET: Listsends/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Listsend listsend = await db.Listsends.FindAsync(id);
            if (listsend == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.Mails, "Id", "to", listsend.Id);
            return View(listsend);
        }

        // POST: Listsends/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Idmail,datemail")] Listsend listsend)
        {
            if (ModelState.IsValid)
            {
                db.Entry(listsend).State = System.Data.Entity.EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.Mails, "Id", "to", listsend.Id);
            return View(listsend);
        }

        // GET: Listsends/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Listsend listsend = await db.Listsends.FindAsync(id);
            if (listsend == null)
            {
                return HttpNotFound();
            }
            return View(listsend);
        }

        // POST: Listsends/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Listsend listsend = await db.Listsends.FindAsync(id);
            db.Listsends.Remove(listsend);
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
