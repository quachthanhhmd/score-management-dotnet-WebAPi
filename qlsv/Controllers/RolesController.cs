using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using qlsv.Models.Interfaces;
using qlsv.ViewModels.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.Controllers
{
    [Route("v1/[Controller]")]
    [ApiController]
    [Authorize]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(
            IRoleService roleService
            )
        {
            _roleService = roleService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("create")]
        public async Task<IActionResult> CreateRole([FromForm] CreateRoleRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _roleService.CreateRoles(request);

            if (result.IsSuccessed)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("")]
        public async Task<IActionResult> GetAll()
        {

            var result = await _roleService.GetAll();
            
            if (result.IsSuccessed)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
