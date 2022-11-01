using GroceryStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroceryStore.Models.ViewModels
{
    public class DepartmentViewModel
    {
        public GroceryService GroceryService { get; set; }
        public IEnumerable<string> Departments { get; set; }

        public DepartmentViewModel(GroceryService groceryService, IEnumerable<string> departments)
        {
            GroceryService = groceryService;
            Departments = departments;
        }
    }
}