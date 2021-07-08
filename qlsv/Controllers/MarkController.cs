using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using qlsv.Models.Interfaces;
using qlsv.ViewModels;
using qlsv.ViewModels.Marks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.Controllers
{
    [Route("v1/[Controller]")]
    [ApiController]
    [Authorize]
    public class MarkController : Controller
    {
        private readonly IMarkService _markService;

        public MarkController(
            IMarkService markService
            )
        {
            _markService = markService;
        }

        [HttpGet]
        [Route("/mark/{ClassId}/{UserId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetMark(string ClassId, Guid UserId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _markService.GetMark(ClassId, UserId);

            return Ok(result);
        }

        [HttpGet]
        [Route("/mark/{ClassId}/{UserId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPagingMark([FromForm] PagingMarkRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _markService.GetPagingMark(request);

            return Ok(result);
        }

        [HttpPost]
        [Route("/mark/create")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateMark(CreateMarkRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _markService.CreateMark(request);

            if (result == 0)
                return BadRequest("Create Unsucessfully");

            return Ok();
        }

        [HttpPut]
        [Route("/mark/update/{ClassId}/{UserId}")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateMark(string ClassId, Guid UserId, UpdateMarkRequest request) 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _markService.UpdateMark(ClassId, UserId, request);

            if (result == null)
                return BadRequest("Update Unsucessfully");

            return Ok(result);
        }

        [HttpDelete]
        [Route("/mark/delete/{ClassId}/{UserId}")]
        public async Task<IActionResult> DeleteMark(string ClassId, Guid UserId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _markService.DeleteMark(ClassId, UserId);

            if (result == 0)
                return BadRequest("Delete Unsucessfully");

            return Ok();
        }
    }
}
