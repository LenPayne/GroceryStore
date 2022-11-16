using GroceryStore.Interfaces;
using GroceryStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroceryStore.Services
{
    public class GroceryService : IGroceryService
    {
        private ApplicationDbContext context;
        public GroceryService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public bool OldEnoughToDrink(DateTime dateTime)
        {
            TimeSpan difference = DateTime.Now.Subtract(dateTime);
            if (difference.Days > 365 * 19) return true;
            return false;
        }

        public IEnumerable<GroceryItem> GetAllItems()
        {
            return context.GroceryItems.ToList();
        }

        public IEnumerable<GroceryItem> GetAllUnownedItems()
        {
            return context.GroceryItems.Where(x => x.Owner == null);
        }

        public GroceryItem GetItemById(int? id)
        {
            return context.GroceryItems.Where(x => x.Id == id).First();
        }

        public GroceryItem GetItemByName(string name)
        {
            return context.GroceryItems.Where(x => x.Name == name).First();
        }

        public IEnumerable<GroceryItem> GetItemsByDepartment(string department)
        {
            return context.GroceryItems.Where(x => x.Department == department);
        }

        public IEnumerable<string> GetDepartments()
        {
            //List<string> result = new List<String>();
            //foreach (GroceryItem item in context.GroceryItems)
            //{
            //    if (!result.Contains(item.Department)) result.Add(item.Department);
            //}
            //result.Sort();
            //return result;
            return context.GroceryItems.Select(x => x.Department).Distinct();
        }

        public List<SelectListItem> GetDepartmentSelectListItems(string SelectedDepartment)
        {
            return GetDepartments().Select(x => new SelectListItem { 
                Text = x, 
                Value = x, 
                Selected = (x == SelectedDepartment) 
            }).ToList();
        }
    }
}