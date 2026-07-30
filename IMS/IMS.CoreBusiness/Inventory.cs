using System;
using System.Collections.Generic;
using System.Text;

namespace IMS.CoreBusiness
    {
    public  class Inventory
        {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        }
    }
