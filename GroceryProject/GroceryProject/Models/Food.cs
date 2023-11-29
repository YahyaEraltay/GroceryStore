using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryProject.Models
{
    public class Food
    {
        public int FoodID { get; set; }
        public string FoodName { get; set; }
        public double FoodPrice { get; set; }
        public string FoodDescription { get; set; }
        public int FoodStock { get; set; }
        public string FoodImage { get; set; }
        public bool FoodStatus { get; set; } = true;
        public int CategoryID { get; set; } // Bir yiyeceğin sadece bir kategorisi olabilir.
        public virtual Category Category { get; set; }
    }
}
