using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using qlsv.Models.Interfaces;
using qlsv.Utilities.Roles;
using qlsv.ViewModels.ClassRoom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.Controllers
{
    [Route("v1/[Controller]")]
    [ApiController]
    [Authorize(Roles = Roles.All)]
    public class ClassRoomController : Controller
    {
        private readonly IClassRoomService _classRoomService;

        public ClassRoomController(
            IClassRoomService classRoomService
            )
        {
            _classRoomService = classRoomService;
        }

        [HttpPost]
        [Route("create")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> CreateClassRoom([FromForm] CreateClassRoomRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _classRoomService.CreateClassRoom(request);

            if (!result.IsSuccessed)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut]
        [Route("update/{Id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> UpdateClassRoom(string Id, [FromForm] CreateClassRoomRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _classRoomService.UpdateClassRoom(Id, request);

            if (!result.IsSuccessed)
                return BadRequest(result);

            return Ok(result);
        }


        [HttpDelete]
        [Route("delete/{Id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> DeleteClassRoomm(string Id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _classRoomService.DeleteClassRoom(Id);

            if (!result.IsSuccessed)
                return BadRequest(result);
            
            return Ok(result);
        }

        [HttpGet]
        [Route("{Id}")]
        public async Task<IActionResult> GetClassRoom(string Id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _classRoomService.GetClassRoom(Id);

            if (!result.IsSuccessed)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetPagingClassRoom([FromQuery] PagingClassRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _classRoomService.GetPagingClasRoom(request);

            if (!result.IsSuccessed)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete]
        [Route("delete/{Id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> DeleteClassRoom(string Id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _classRoomService.DeleteClassRoom(Id);

            if (!result.IsSuccessed)
                return BadRequest(result);

            return Ok(result);
        }
    }





}
