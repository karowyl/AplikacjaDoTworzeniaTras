using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunnerApp.Data;
using RunnerApp.DTOs;
using RunnerApp.Entities;
using RunnerApp.Interfaces;
using System.Security.Claims;

namespace RunnerApp.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var users = await _userRepository.GetUsersAsync(Convert.ToInt32(userId));

            var usersToReturn = _mapper.Map<IEnumerable<MemberDto>>(users);
            return  Ok(usersToReturn);
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            return await _userRepository.GetMemberAsync(username);
        }

        [HttpGet("find/{keyWord}")]
        public async Task<ActionResult<IEnumerable<MemberDto>>> FindUsers(string keyWord)
        {
            var users = await _userRepository.FindUserAsync(keyWord);

            var usersToReturn = _mapper.Map<IEnumerable<MemberDto>>(users);
            return Ok(usersToReturn);
        }
         

        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userRepository.GetUserByIdAsync(Convert.ToInt32(userId));

            _mapper.Map(memberUpdateDto, user);
            _userRepository.Update(user);

            if (await _userRepository.SaveAllAsync()) return NoContent();
            return BadRequest("NIe udało się zaktualizować danych");

        }

    }
}
