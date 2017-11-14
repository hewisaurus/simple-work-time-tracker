using System.Diagnostics;
using System.Threading.Tasks;
using BCrypt;
using Database.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleWorkTimeTracker.Helpers;
using SimpleWorkTimeTracker.Models;

namespace SimpleWorkTimeTracker.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private readonly IPersonStatusQueryRepository _personStatusQuery;

        public HomeController(IPersonStatusQueryRepository personStatusQuery)
        {
            _personStatusQuery = personStatusQuery;
        }

        public async Task<IActionResult> Index()
        {
            var statuses = await _personStatusQuery.GetAllAsync();

            return View();
        }
        
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
