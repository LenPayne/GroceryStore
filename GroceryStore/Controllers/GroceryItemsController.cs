using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using GroceryStore.Models;
using GroceryStore.Models.ViewModels;
using GroceryStore.Services;
using Microsoft.AspNet.Identity;

namespace GroceryStore.Controllers
{
    public class GroceryItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private GroceryService groceryService;

        private void Init()
        {
            groceryService = new GroceryService(db);
        }

        // GET: GroceryItems
        [AllowAnonymous]
        public ActionResult Index()
        {
            Init();
            IEnumerable<string> departments = groceryService.GetDepartments();
            DepartmentViewModel dvm = new DepartmentViewModel(groceryService, departments);
            return View(dvm);
        }

        // GET: MyGroceries
        [Authorize]
        public ActionResult MyGroceries()
        {
            return View("MyGroceries", db.GroceryItems.Include(x => x.Owner)
                        .Where(a => a.Owner.UserName == User.Identity.Name)
                        .ToList());
        }

        // GET: ByDepartment
        public ActionResult ByDepartment(string department)
        {
            Init();
            return View("ByDepartment", groceryService.GetItemsByDepartment(department));
        }

        // GET: GetItemsByDepartment
        public ActionResult GetItemsByDepartment(string department)
        {
            Init();
            return View("_GroceryListPartial", groceryService.GetItemsByDepartment(department));
        }

        [HttpGet]
        [Authorize]
        public ActionResult Buy(int? id)
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

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Buy(GroceryItem groceryItem)
        {
            var user = db.Users.Include(x => x.GroceryItems)
                .Where(x => x.UserName == User.Identity.Name)
                .First();
            var grocery = db.GroceryItems.Find(groceryItem.Id);
            
            var claimUser = (ClaimsPrincipal)User;
            var dateOfBirth = Convert.ToDateTime(
                claimUser.Claims.Where(claim => claim.Type == ClaimTypes.DateOfBirth)
                .First()
                .Value);
            var age = DateTime.Now.Subtract(dateOfBirth);

            if (age.Days >= 365*19) {
                grocery.Owner = user;
                user.GroceryItems.Add(grocery);
                db.SaveChanges();
            }

            return RedirectToAction("MyGroceries");
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
            Init();
            CreateGroceryItemViewModel cgivm = new CreateGroceryItemViewModel
            {
                DepartmentDropDown = groceryService.GetDepartmentSelectListItems(null)
            };
            return View(cgivm);
        }

        // POST: GroceryItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Id,Name,isAlochol,Department,Weight")] GroceryItem groceryItem)
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
            Init();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroceryItem groceryItem = groceryService.GetItemById(id);
            if (groceryItem == null)
            {
                return HttpNotFound();
            }
            EditGroceryItemViewModel egivm = new EditGroceryItemViewModel
            {
                DepartmentDropDown = groceryService.GetDepartmentSelectListItems(groceryItem.Department),
                Id = groceryItem.Id,
                Name = groceryItem.Name,
                isAlochol = groceryItem.isAlochol,
                Department = groceryItem.Department,
                Weight = groceryItem.Weight
            };
            return View(egivm);
        }

        // POST: GroceryItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Id,Name,isAlochol,Department,Weight")] GroceryItem groceryItem)
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
