using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PizzaLocation.Library;

namespace PizzaLocation.Data
{
    public class LocationRepository
    {

        private readonly PizzaLocationDBContext _db;


        public LocationRepository(PizzaLocationDBContext PizzaDB)
        {
            _db = PizzaDB ?? throw new ArgumentNullException(nameof(PizzaDB));
        }

        // Returns all users from DB
        public IEnumerable<TblLocation> GetLocations()
        {
            List<TblLocation> locations = _db.TblLocation.AsNoTracking().ToList();
            return locations;
        }


        // Adds a new user to the DB
        public void AddLocation(string name, string address)
        {
            var newLocation = new TblLocation
            {
                Name = name,
                Address = address
            };
            _db.Add(newLocation);
        }


        public void DeleteLocationWithId(int id)
        {
            var locationToRemove = _db.TblLocation.Find(id);
            if (locationToRemove == null)
            {
                throw new ArgumentException("no Pizza location with that ID", nameof(id));
            }
            _db.Remove(locationToRemove);
        }


        public void Edit(TblLocation locale)
        {
            _db.Update(locale);
        }


        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
