using Domain.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<string>> Get([FromRoute] string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            int _id = Convert.ToInt32(id);

            BaseResponseDto<UserDto> user = await _userService.GetAsync(_id);

            if (!user.HasError || user.Data != null)
            {
                return Ok(user.Data);
            }
            else if (!user.HasError || user.Data == null)
            {
                return NotFound();
            }
            else
            {
                return BadRequest(user.Errors);
            }
        }


    }
}
