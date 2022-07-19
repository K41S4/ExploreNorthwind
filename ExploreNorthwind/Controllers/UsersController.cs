using ExploreNorthwindDataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExploreNorthwind.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private IUsersRepository usersRepo { get; }
        public UsersController(IUsersRepository usersRepo)
        {
            this.usersRepo = usersRepo;
        }

        public IActionResult Index()
        {
            return View(usersRepo.GetAll().Select(w => w.Email));
        }
    }
}
