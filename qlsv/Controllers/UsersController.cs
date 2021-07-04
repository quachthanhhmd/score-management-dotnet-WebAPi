﻿using Microsoft.AspNetCore.Mvc;
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
        [Route("/register")]
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
        [Route("/login")]
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

        [HttpGet]
        [Route("user/{Id}")]
        public async Task<IActionResult> GetOneUser(string Id)
        {
            var user = await _userPublicService.GetOneUser(Id);

            return Ok(user);
        }
    }
}
