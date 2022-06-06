using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Artezanias2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Artezanias2.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, Editor")]
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        public UsersController(UserManager<AppUser>userManager)
        {
            this.userManager = userManager;

        }
        public IActionResult Index()

        {
            return View(userManager.Users);
        }
    }
}