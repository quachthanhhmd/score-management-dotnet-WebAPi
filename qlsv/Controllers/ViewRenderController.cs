using Microsoft.AspNetCore.Mvc;
using qlsv.Models.Interfaces;
using qlsv.ViewModels.Marks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.Controllers
{
    //Not implement since is use for testing
    /*
    [Route("/ViewRender")]
    public class ViewRenderController : Controller
    {
        private readonly IMarkService _markService;

        public ViewRenderController(
            IMarkService markService
            )
        {
            _markService = markService;
        }

        [Route("{Id}")]
        public async Task<IActionResult> Index(string Id)
        {

            var data = await _markService.GetTranscript(Id);
            return View(data);
        }
    }
    */
}
