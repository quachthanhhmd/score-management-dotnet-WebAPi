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
    public class UsersController : ControllerBase
    {

        private readonly IUserPublicService  _userPublicService;

        public UsersController(IUserPublicService userPublicService)
        {
            _userPublicService = userPublicService;
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromForm] RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userPublicService.Register(request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Authencate([FromForm] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _userPublicService.Authencate(request);

            if (!string.IsNullOrEmpty(result.ResultObj))
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut]
        [Route("update/{Id}")]
        public async Task<IActionResult> UpdateUser(Guid Id, [FromForm] UpdateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userPublicService.UpdateUser(Id, request);

            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }

            return Ok(result);

        }

        [HttpDelete]
        [Route("delete/{Id}")]
        public async Task<IActionResult> DeleteUser(Guid Id)
        {

            var result = await _userPublicService.DeleteUser(Id);

            if (!result.IsSuccessed)
                return BadRequest(result);
            return Ok(result);
        }
        [HttpGet]
        [Route("{Id}")]
        public async Task<IActionResult> GetOneUser(string Id)
        {
            var user = await _userPublicService.GetOneUser(Id);

            return Ok(user);
        }

        [HttpPost]
        [Route("Enroll/{Id}/{ClassId}")]
        public async Task<IActionResult> EnrollClass(Guid Id, string ClassId)
        {
            var result = await _userPublicService.EnrollClass(Id, ClassId);

            if (result.IsSuccessed)
                return Ok();
            return BadRequest("Đăng ký không thành công");
        }
    }
}
