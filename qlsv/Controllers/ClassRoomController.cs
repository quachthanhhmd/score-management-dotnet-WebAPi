using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using qlsv.Models.Interfaces;
using qlsv.ViewModels.ClassRoom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.Controllers
{
    [Route("v1/[Controller]")]
    [ApiController]
    [Authorize]
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
        public async Task<IActionResult> CreateClassRoom([FromForm] CreateClassRoomRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _classRoomService.CreateClassRoom(request);

            if (result == 0)
                return BadRequest();

            return Ok();
        }

        [HttpPut]
        [Route("update/{Id}")]
        public async Task<IActionResult> UpdateClassRoom(string Id, [FromForm] CreateClassRoomRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _classRoomService.UpdateClassRoom(Id, request);

            if (result == null)
                return BadRequest();

            return Ok(result);
        }


        [HttpDelete]
        [Route("delete/{Id}")]
        public async Task<IActionResult> DeleteClassRoomm(string Id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _classRoomService.DeleteClassRoom(Id);

            if (result == 0)
                return BadRequest();

            return Ok(result);
        }

        [HttpGet]
        [Route("{Id}")]
        public async Task<IActionResult> GetClassRoom(string Id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _classRoomService.GetClassRoom(Id);

            if (result == null)
                return BadRequest("Class Room not found");
            return Ok(result);
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetPagingClassRoom([FromQuery] PagingClassRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _classRoomService.GetPagingClasRoom(request);

            if (result == null)
                return BadRequest("Classroom not found");

            return Ok(result);
        }

        [HttpDelete]
        [Route("delete/{Id}")]
        public async Task<IActionResult> DeleteClassRoom(string Id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _classRoomService.DeleteClassRoom(Id);

            if (result == 0)
                return BadRequest("Delete Does not success");

            return Ok();
        }
    }
}
