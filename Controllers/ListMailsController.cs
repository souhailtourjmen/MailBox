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
    public class ListMailsController : Controller
    {
        private BoitMailContext db = new BoitMailContext();

        // GET: ListMails
        public async Task<ActionResult> Index()
        {
            var listMails = db.ListMails.Include(l => l.Mail);
            return View(await listMails.ToListAsync());
        }

        // GET: ListMails/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListMail listMail = await db.ListMails.FindAsync(id);
            if (listMail == null)
            {
                return HttpNotFound();
            }
            return View(listMail);
        }

        // GET: ListMails/Create
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.Mails, "Id", "to");
            return View();
        }

        // POST: ListMails/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Idmail,datemail")] ListMail listMail)
        {
            if (ModelState.IsValid)
            {
                db.ListMails.Add(listMail);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.Mails, "Id", "to", listMail.Id);
            return View(listMail);
        }

        // GET: ListMails/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListMail listMail = await db.ListMails.FindAsync(id);
            if (listMail == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.Mails, "Id", "to", listMail.Id);
            return View(listMail);
        }

        // POST: ListMails/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Idmail,datemail")] ListMail listMail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(listMail).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.Mails, "Id", "to", listMail.Id);
            return View(listMail);
        }

        // GET: ListMails/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListMail listMail = await db.ListMails.FindAsync(id);
            if (listMail == null)
            {
                return HttpNotFound();
            }
            return View(listMail);
        }

        // POST: ListMails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ListMail listMail = await db.ListMails.FindAsync(id);
            db.ListMails.Remove(listMail);
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
