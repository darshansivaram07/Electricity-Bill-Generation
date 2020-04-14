﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ElectricityBillGeneration.Models;

namespace ElectricityBillGeneration.Controllers
{
    public class ElectricityBillController : Controller
    {
        private EBBillContext db = new EBBillContext();

        // GET: ElectricityBill
        public ActionResult Index()
        {
            return View(db.ElectricityBill.ToList());
        }

        // GET: ElectricityBill/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ElectricityBill electricityBill = db.ElectricityBill.Find(id);
            if (electricityBill == null)
            {
                return HttpNotFound();
            }
            return View(electricityBill);
        }

        // GET: ElectricityBill/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ElectricityBill/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ConsumerNumber,ConsumerName,UnitsConsumed,BillAmount")] ElectricityBill electricityBill)
        {
            CalculateBill(electricityBill);
            if (ModelState.IsValid)
            {
                
                db.ElectricityBill.Add(electricityBill);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(electricityBill);
        }

        // GET: ElectricityBill/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ElectricityBill electricityBill = db.ElectricityBill.Find(id);
            if (electricityBill == null)
            {
                return HttpNotFound();
            }
            return View(electricityBill);
        }

        // POST: ElectricityBill/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ConsumerNumber,ConsumerName,UnitsConsumed,BillAmount")] ElectricityBill electricityBill)
        {
            CalculateBill(electricityBill);
            if (ModelState.IsValid)
            {
                db.Entry(electricityBill).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(electricityBill);
        }

        // GET: ElectricityBill/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ElectricityBill electricityBill = db.ElectricityBill.Find(id);
            if (electricityBill == null)
            {
                return HttpNotFound();
            }
            return View(electricityBill);
        }

        // POST: ElectricityBill/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ElectricityBill electricityBill = db.ElectricityBill.Find(id);
            db.ElectricityBill.Remove(electricityBill);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public void CalculateBill(ElectricityBill electricityBill)
        {
            int unitsConsumed = electricityBill.UnitsConsumed;
            double totalCost=0;
            if (unitsConsumed <= 100)
            {
                totalCost = 0;
            }
            else if (unitsConsumed <= 300)
            {
                totalCost = (unitsConsumed - 100) * 1.5;
            }
            else if (unitsConsumed <= 600)
            {
                unitsConsumed -= 100;
                totalCost = (200 * 1.5) + ((unitsConsumed - 200) * 3.5);
            }
            else if (unitsConsumed <= 1000)
            {
                unitsConsumed -= 100;
                totalCost = (200 * 1.5) + (300 * 3.5) + ((unitsConsumed - 500) * 5.5);
            }
            else
            {
                unitsConsumed -= 100;
                totalCost = (200 * 1.5) + (300 * 3.5) + (400 * 5.5) + ((unitsConsumed - 900) * 7.5);
            }
            electricityBill.BillAmount = totalCost;
            
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
