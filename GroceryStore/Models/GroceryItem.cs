using GroceryStore.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroceryStore.Models
{
    public class GroceryItem
    {
        public int Id { get; set; }

        [NoDigits]
        public string Name { get; set; }
        public bool isAlochol { get; set; }
        public string Department { get; set; }
        public ApplicationUser Owner { get; set; }

        [NonNegative]
        public int Weight { get; set; }
    }
}