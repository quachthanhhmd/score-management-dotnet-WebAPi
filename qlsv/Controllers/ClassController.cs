using Microsoft.AspNetCore.Mvc;
using qlsv.Models.Interfaces;
using qlsv.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using qlsv.ViewModels;

namespace qlsv.Controllers
{

    [Route("v1/[Controller]")]
    [ApiController]
    [Authorize]
    public class ClassController : ControllerBase
    {

        private readonly IClassService _classService;

        public ClassController(
            IClassService classService)
        {
            _classService = classService;
        }

        [HttpPost]
        [Route("/class/create")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateClass([FromForm] AddClassRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _classService.CreateClass(request);

            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
            
    }
}
