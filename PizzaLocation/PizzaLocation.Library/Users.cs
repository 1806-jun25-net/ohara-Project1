using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaLocation.Library
{
    public partial class Users : IUser
    {

        public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        /* public tblUsers(string first, string last)
        {
            firstName = first;
            lastName = last;
        } */
    } 
}
