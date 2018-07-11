using System;
using System.Collections.Generic;

namespace PizzaLocation.Data
{
    public partial class TblOrders
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string OrderLocation { get; set; }
        public DateTime? OrderTime { get; set; }
        public decimal OrderCost { get; set; }
        public int? LocationId { get; set; }
    }
}
