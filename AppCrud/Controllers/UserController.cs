using Microsoft.AspNetCore.Mvc;

using AppCrud.Data;
using AppCrud.Models;
using Microsoft.EntityFrameworkCore;

namespace AppCrud.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDBContext _appDbContext;
        public UserController(AppDBContext appDBContext)
        {
            _appDbContext = appDBContext;
        }

        [HttpGet]
        public async Task<IActionResult> LstUser()
        {
            List<User> lstUser = await _appDbContext.Users.ToListAsync();
            return View(lstUser);
        }
    }
}
