using GroceryStore.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroceryStore.Models.ViewModels
{
    public class CreateGroceryItemViewModel
    {

        [NoDigits]
        public string Name { get; set; }
        public bool isAlochol { get; set; }
        public string Department { get; set; }
        
        [NonNegative]
        public int Weight { get; set; }

        public List<SelectListItem> DepartmentDropDown { get; set; }
    }
}