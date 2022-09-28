using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GroceryStore.Attributes
{
    public class NoDigitsAttribute : ValidationAttribute
    {
        public NoDigitsAttribute () : base("No numbers allowed!")
        {

        }

        public override bool IsValid(object value)
        {
            string strValue = value.ToString();
            bool result = true;
            foreach (char c in strValue.ToCharArray())
            {
                if (c >= '0' && c <= '9') result = false;
            }
            return result;
        }
    }
}