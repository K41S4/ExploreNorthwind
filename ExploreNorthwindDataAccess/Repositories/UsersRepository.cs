using ExploreNorthwindDataAccess.Models;
using ExploreNorthwindDataAccess.NorthwindDB;
using ExploreNorthwindDataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExploreNorthwindDataAccess.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private INorthwindContext Context { get; }
        public UsersRepository(INorthwindContext northwindContext)
        {
            this.Context = northwindContext;
        }

        public List<AppUser> GetAll()
        {
            return Context.Users.ToList();
        }
    }
}
