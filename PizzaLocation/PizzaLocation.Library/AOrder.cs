using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace PizzaLocation.Library
{
    public class AOrder : IOrder
    {
        public string LocationName { get; set; }
        public int CustomerID { get; }
        public DateTime OrderTime { get; }
        public decimal OrderCost { get; }
        
        ArrayList OrderItems = new ArrayList();



        public AOrder(string Location, int CustID, DateTime Time, decimal cost)
        {
            LocationName = Location;
            CustomerID = CustID;
            OrderTime = Time;
            OrderCost = cost;
        }
    }
}
