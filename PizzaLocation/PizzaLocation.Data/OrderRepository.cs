using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PizzaLocation.Library;

namespace PizzaLocation.Data
{
    public class OrderRepository
    {

        private readonly PizzaLocationDBContext _db;


        public OrderRepository(PizzaLocationDBContext PizzaDB)
        {
            _db = PizzaDB ?? throw new ArgumentNullException(nameof(PizzaDB));
        }

        // Returns all Orders from DB
        public IEnumerable<TblOrders> GetOrders()
        {
            List<TblOrders> hordeurves = _db.TblOrders.AsNoTracking().ToList();
            return hordeurves;
        }

        // Adds a new Order to the DB
        public void AddOrder(int custID, string ordLocation, DateTime? orderTime, decimal orderCost, int? locID)
        {
            var newOrder = new TblOrders
            {
                CustomerId = custID,
                OrderLocation = ordLocation,
                OrderTime = orderTime,
                OrderCost = orderCost,
                LocationId = locID
            };
            _db.Add(newOrder);
        }

        //  This isn't something that the customer should be able to do unless they just placed
        //    order and cancel it before 20 minutes have elapsed
        public void DeleteOrderWithId(int id)
        {
            var orderToRemove = _db.TblUsers.Find(id);

            if (orderToRemove == null)
            {
                throw new ArgumentException("no order with that ID number", nameof(id));
            }
            _db.Remove(orderToRemove);
        }


        public void Edit(TblOrders order)
        {
            _db.Update(order);
        }


        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}

