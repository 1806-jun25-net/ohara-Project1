using System;
using System.Linq;
using Xunit;
using PizzaLocation.Data;
using PizzaLocation.Library;

namespace PizzaLocation.Testing
{
    public class PizzaLocationTests
    {

        Users users = new Users();
        Location location = new Location();
        AOrder order;


        [Fact]
        public void NewUser_CorrectInput_StoresCorrectly()
        {
            const string first = "Ludwig", last = "Wittgenstein";
            users.FirstName = first;
            users.LastName = last;
            Assert.Equal(first, users.FirstName);
            Assert.Equal(last, users.LastName);

        }
        
    }
}
