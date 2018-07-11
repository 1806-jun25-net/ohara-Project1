using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaLocation.Library
{
    public interface IOrder
    {
        string LocationName { get; set; }
        int CustomerID { get; }
    }
}
