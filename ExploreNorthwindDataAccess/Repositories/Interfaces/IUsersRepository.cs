using ExploreNorthwindDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExploreNorthwindDataAccess.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        List<AppUser> GetAll();
    }
}
