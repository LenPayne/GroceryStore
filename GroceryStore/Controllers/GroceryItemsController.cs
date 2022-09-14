﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GroceryStore.Models;

namespace GroceryStore.Controllers
{
    public class GroceryItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: GroceryItems
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(db.GroceryItems.Where(a => a.Owner != null).ToList());
        }

        // GET: MyGroceries
        public ActionResult MyGroceries()
        {
            return View(db.GroceryItems.Where(a => a.Owner == HttpContext.User).ToList());
        }

        // GET: GroceryItems/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroceryItem groceryItem = db.GroceryItems.Find(id);
            if (groceryItem == null)
            {
                return HttpNotFound();
            }
            return View(groceryItem);
        }

        // GET: GroceryItems/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: GroceryItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Id,Name,isAlochol,Department")] GroceryItem groceryItem)
        {
            if (ModelState.IsValid)
            {
                db.GroceryItems.Add(groceryItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(groceryItem);
        }

        // GET: GroceryItems/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroceryItem groceryItem = db.GroceryItems.Find(id);
            if (groceryItem == null)
            {
                return HttpNotFound();
            }
            return View(groceryItem);
        }

        // POST: GroceryItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Id,Name,isAlochol,Department")] GroceryItem groceryItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(groceryItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(groceryItem);
        }

        // GET: GroceryItems/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroceryItem groceryItem = db.GroceryItems.Find(id);
            if (groceryItem == null)
            {
                return HttpNotFound();
            }
            return View(groceryItem);
        }

        // POST: GroceryItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            GroceryItem groceryItem = db.GroceryItems.Find(id);
            db.GroceryItems.Remove(groceryItem);
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