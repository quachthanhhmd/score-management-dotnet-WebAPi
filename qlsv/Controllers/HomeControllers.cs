using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.Controllers
{
    public class HomeControllers : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
