using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolarCoffee.Web.ViewModels
{
    public class SalesOrderItemViewModel
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public ProductViewModel InventoryProduct { get; set; }
    }
}
