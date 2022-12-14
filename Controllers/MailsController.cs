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
    public class MailsController : Controller
    {
        private BoitMailContext db = new BoitMailContext();

        // GET: Mails
        public async Task<ActionResult> Index()
        {
            return View(await db.Mails.ToListAsync());
        }

        // GET: Mails/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mail mail = await db.Mails.FindAsync(id);
            if (mail == null)
            {
                return HttpNotFound();
            }
            return View(mail);
        }

        // GET: Mails/Create
        public ActionResult Create()
        {
            //ViewBag.Id = new SelectList(db.Mails, "Id", "to");
            return View();
        }

        // POST: Mails/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(String draft ,[Bind(Include = "Id,to,_object,body")] Mail mail)
        {
            DateTime dateTime = DateTime.Now;
            Random rand = new Random();
            if (ModelState.IsValid)
            {
                mail.Id = rand.Next(9999) + 1;
                db.Mails.Add(mail);
                await db.SaveChangesAsync();
                if (draft== "draft")
                {
                    Listdraft listdraft = new Listdraft();
                    listdraft.Id = mail.Id;
                    listdraft.Idmail = mail.Id;
                    listdraft.datemail = dateTime;
                    db.Listdrafts.Add(listdraft);
                    await db.SaveChangesAsync();

                }
                else
                {
                    Listsend listsend = new Listsend();
                    listsend.Id = mail.Id;
                    listsend.Idmail = mail.Id;
                    listsend.datemail = dateTime;
                    db.Listsends.Add(listsend);
                    await db.SaveChangesAsync();
                }
               
                return RedirectToAction("Index", "ListMails");
            }

            return View(mail);
        }

        // GET: Mails/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mail mail = await db.Mails.FindAsync(id);
            if (mail == null)
            {
                return HttpNotFound();
            }
            return View(mail);
        }

        // POST: Mails/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,to,_object,body")] Mail mail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mail).State = System.Data.Entity.EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(mail);
        }

        // GET: Mails/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mail mail = await db.Mails.FindAsync(id);
            if (mail == null)
            {
                return HttpNotFound();
            }
            return View(mail);
        }

        // POST: Mails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Mail mail = await db.Mails.FindAsync(id);
            db.Mails.Remove(mail);
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
