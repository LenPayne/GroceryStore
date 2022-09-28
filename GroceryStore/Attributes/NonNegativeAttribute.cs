using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing.Design;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace GroceryStore.Attributes
{
    public class NonNegativeAttribute : ValidationAttribute
    {
        public NonNegativeAttribute() : base("Number cannot be negative")
        {            
        }

        public override bool IsValid(object value)
        {
            try
            {
                int intValue = Convert.ToInt32(value);
                return intValue >= 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}