using GroceryStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroceryStore.Services
{
    public class GroceryService
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
    }
}