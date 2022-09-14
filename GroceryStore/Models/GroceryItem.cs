using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroceryStore.Models
{
    public class GroceryItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool isAlochol { get; set; }
        public string Department { get; set; }
        public ApplicationUser Owner { get; set; }
    }
}