using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PizzaLocation.Library;

namespace PizzaLocation.Data
{
    public class UsersRepository
    {

        private readonly PizzaLocationDBContext _db;


        public UsersRepository(PizzaLocationDBContext PizzaDB)
        {
            _db = PizzaDB ?? throw new ArgumentNullException(nameof(PizzaDB));
        }

        // Returns all users from DB
        public IEnumerable<TblUsers> GetUsers()
        {
            List<TblUsers> users = _db.TblUsers.AsNoTracking().ToList();
            return users;
        }

        // Adds a new user to the DB
        public void AddUser(string given, string surname)
        {
            var newUser = new TblUsers
            {
                FirstName = given,
                LastName = surname
            };
            _db.Add(newUser);
        }


        public void DeleteUserWithId(int id)
        {
            var userToRemove = _db.TblUsers.Find(id);
            if (userToRemove == null)
            {
                throw new ArgumentException("no customer with that ID", nameof(id));
            }
            _db.Remove(userToRemove);
        }


        public void Edit(TblUsers user)
        {
            _db.Update(user);
        }


        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}

