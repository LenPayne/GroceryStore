using GroceryStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GroceryStore.Interfaces
{
    internal interface IGroceryService
    {
        bool OldEnoughToDrink(DateTime dateTime);

        IEnumerable<GroceryItem> GetAllItems();

        IEnumerable<GroceryItem> GetAllUnownedItems();

        GroceryItem GetItemById(int? id);

        GroceryItem GetItemByName(string name);

        IEnumerable<GroceryItem> GetItemsByDepartment(string department);

        IEnumerable<string> GetDepartments();

        List<SelectListItem> GetDepartmentSelectListItems(string SelectedDepartment);
    }
}
