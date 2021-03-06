using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using qlsv.Models.Interfaces;
using qlsv.Utilities.Roles;
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
    [Authorize(Roles = Roles.All)]
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
        [Route("{ClassId}/{UserId}")]
        
        public async Task<IActionResult> GetMark(string ClassId, string UserId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _markService.GetMark(ClassId, UserId);

            return Ok(result);
        }

        [HttpGet]
        [Route("")]
        
        public async Task<IActionResult> GetPagingMark([FromQuery] PagingMarkRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _markService.GetPagingMark(request);

            return Ok(result);
        }

        [HttpPost]
        [Route("create")]
        [Authorize(Roles = Roles.Manager)]
        public async Task<IActionResult> CreateMark([FromForm] CreateMarkRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _markService.CreateMark(request);

            if (!result.IsSuccessed)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut]
        [Route("update/{ClassId}/{UserId}")]
        [Authorize(Roles = Roles.Manager)]
        public async Task<IActionResult> UpdateMark(string ClassId, string UserId,[FromForm] UpdateMarkRequest request) 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _markService.UpdateMark(ClassId, UserId, request);

            if (!result.IsSuccessed)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete]
        [Route("delete/{ClassId}/{UserId}")]
        [Authorize(Roles = Roles.Manager)]
        public async Task<IActionResult> DeleteMark(string ClassId, string UserId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _markService.DeleteMark(ClassId, UserId);

            if (!result.IsSuccessed)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet]
        [Route("transcript/{Id}")]
        //Only student with your Id and Admin can get transcript of THIS student.
        [Authorize(Roles = Roles.Admin)]
        [Authorize(Roles = Roles.Student)]
        public async Task<IActionResult> GetTranscript(string Id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _markService.GetTranscript(Id);

            if (!result.IsSuccessed)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet]
        [Route("gpa/{Id}")]
        [Authorize(Roles = Roles.Student)]
        //Only student with your Id and Admin can get transcripy of THIS student.
        public async Task<IActionResult> GetGPA(string Id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _markService.GetGPA(Id);

            if (!result.IsSuccessed)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet]
        [Route("transcript/pdf/{Id}")]
        [Authorize(Roles = Roles.Student)]
        [Authorize(Roles = Roles.Admin)]
        //Only student with your Id and Admin can get transcripy of THIS student.
        public async Task<IActionResult> ExportTranscriptToPdf(string Id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _markService.ExportTranscriptToPdf(Id);

            if (!result.IsSuccessed)
                return BadRequest(result);

            return Ok(result);
        }



        [HttpGet]
        [AllowAnonymous]
        [Route("GetMarkSemester")]
        //Only student with your Id and Admin can get transcripy of THIS student.
        public async Task<IActionResult> GetMarkInSemester([FromQuery]MarkSemesterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _markService.GetMarkInSemester(request);

            if (!result.IsSuccessed)
                return BadRequest(result);

            return Ok(result);
        }

    }
}
