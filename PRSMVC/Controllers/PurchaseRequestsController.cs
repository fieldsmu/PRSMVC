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
using PRSMVC.ViewModels;

namespace PRSMVC.Controllers
{
	public class PurchaseRequestsController : Controller
	{
		private PRSAppContext db = new PRSAppContext();

		public ActionResult LineItemsForPR(int? id) {
			LineItemsForPR lineItemsForPR = new LineItemsForPR() {
				PurchaseRequest = db.PurchaseRequests.Find(id),
				PRLineItems = db.PRLineItems.Where(i => i.PurchaseRequestId == id).ToList()
			};
			return View(lineItemsForPR);
		}

		public ActionResult ShowAllPRsWithLineItemTotal() {
			ShowAllPRsWithLineItemTotal showall = new ShowAllPRsWithLineItemTotal() {
				PurchaseRequests = db.PurchaseRequests.Include(p => p.User).ToList(),
				PRLineItems = db.PRLineItems.ToList()
			};
			return View(showall);
		}

		// GET: PurchaseRequests
		public ActionResult Index()
		{
			var purchaseRequests = db.PurchaseRequests.Include(p => p.User);
			return View(purchaseRequests.ToList());
		}

		// GET: PurchaseRequests/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			PurchaseRequest purchaseRequest = db.PurchaseRequests.Find(id);
			if (purchaseRequest == null)
			{
				return HttpNotFound();
			}
			return View(purchaseRequest);
		}

		// GET: PurchaseRequests/Create
		public ActionResult Create()
		{
			ViewBag.UserId = new SelectList(db.Users, "Id", "Username");
			return View();
		}

		// POST: PurchaseRequests/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "Id,Description,Justification,RejectionReason,DeliveryMode,Status,UserId")] PurchaseRequest purchaseRequest)
		{
			if (ModelState.IsValid)
			{
				db.PurchaseRequests.Add(purchaseRequest);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			ViewBag.UserId = new SelectList(db.Users, "Id", "Username", purchaseRequest.UserId);
			return View(purchaseRequest);
		}

		// GET: PurchaseRequests/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			PurchaseRequest purchaseRequest = db.PurchaseRequests.Find(id);
			if (purchaseRequest == null)
			{
				return HttpNotFound();
			}
			ViewBag.UserId = new SelectList(db.Users, "Id", "Username", purchaseRequest.UserId);
			return View(purchaseRequest);
		}

		// POST: PurchaseRequests/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Id,Description,Justification,RejectionReason,DeliveryMode,Status,UserId")] PurchaseRequest purchaseRequest)
		{
			if (ModelState.IsValid)
			{
				db.Entry(purchaseRequest).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			ViewBag.UserId = new SelectList(db.Users, "Id", "Username", purchaseRequest.UserId);
			return View(purchaseRequest);
		}

		// GET: PurchaseRequests/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			PurchaseRequest purchaseRequest = db.PurchaseRequests.Find(id);
			if (purchaseRequest == null)
			{
				return HttpNotFound();
			}
			return View(purchaseRequest);
		}

		// POST: PurchaseRequests/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			PurchaseRequest purchaseRequest = db.PurchaseRequests.Find(id);
			db.PurchaseRequests.Remove(purchaseRequest);
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
