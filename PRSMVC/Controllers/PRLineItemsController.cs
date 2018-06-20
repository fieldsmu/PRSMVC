using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PRSMVC;
using PRSMVC.Models;

namespace PRSMVC.Controllers
{
    public class PRLineItemsController : Controller
    {
        private PRSAppContext db = new PRSAppContext();

        // GET: PRLineItems
        public ActionResult Index()
        {
            var pRLineItems = db.PRLineItems.Include(p => p.Product).Include(p => p.PurchaseRequest);
            return View(pRLineItems.ToList());
        }

        // GET: PRLineItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRLineItem pRLineItem = db.PRLineItems.Find(id);
            if (pRLineItem == null)
            {
                return HttpNotFound();
            }
            return View(pRLineItem);
        }

        // GET: PRLineItems/Create
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(db.Products, "Id", "PartNumber");
            ViewBag.PurchaseRequestId = new SelectList(db.PurchaseRequests, "Id", "Description");
            return View();
        }

        // POST: PRLineItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PurchaseRequestId,ProductId,Quantity")] PRLineItem pRLineItem)
        {
            if (ModelState.IsValid)
            {
                db.PRLineItems.Add(pRLineItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(db.Products, "Id", "PartNumber", pRLineItem.ProductId);
            ViewBag.PurchaseRequestId = new SelectList(db.PurchaseRequests, "Id", "Description", pRLineItem.PurchaseRequestId);
            return View(pRLineItem);
        }

        // GET: PRLineItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRLineItem pRLineItem = db.PRLineItems.Find(id);
            if (pRLineItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.Products, "Id", "PartNumber", pRLineItem.ProductId);
            ViewBag.PurchaseRequestId = new SelectList(db.PurchaseRequests, "Id", "Description", pRLineItem.PurchaseRequestId);
            return View(pRLineItem);
        }

        // POST: PRLineItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PurchaseRequestId,ProductId,Quantity")] PRLineItem pRLineItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pRLineItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.Products, "Id", "PartNumber", pRLineItem.ProductId);
            ViewBag.PurchaseRequestId = new SelectList(db.PurchaseRequests, "Id", "Description", pRLineItem.PurchaseRequestId);
            return View(pRLineItem);
        }

        // GET: PRLineItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRLineItem pRLineItem = db.PRLineItems.Find(id);
            if (pRLineItem == null)
            {
                return HttpNotFound();
            }
            return View(pRLineItem);
        }

        // POST: PRLineItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PRLineItem pRLineItem = db.PRLineItems.Find(id);
            db.PRLineItems.Remove(pRLineItem);
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
