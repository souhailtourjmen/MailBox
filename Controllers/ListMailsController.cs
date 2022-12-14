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

        // GET: Mails/Details/
        public async Task<ActionResult> Details(int? id)
        {
            string url = "/Mails/Details/" + id + Request.QueryString["redirect_url"];

            return Redirect(url);

        }

        // GET: Mails/Edit/
        public async Task<ActionResult> Edit(int? id)
        {
            string url = "/Mails/Edit/" + id + Request.QueryString["redirect_url"];

            return Redirect(url);
        }

        // GET: Mails/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            string url = "/Mails/Delete/" + id + Request.QueryString["redirect_url"];

            return Redirect(url);
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
