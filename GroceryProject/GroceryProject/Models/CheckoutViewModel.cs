using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryProject.Models
{
    public class CheckoutViewModel
    {
        public List<(string name, string amount, int quantity)> FoodItems { get; set; }
        
    }
}
