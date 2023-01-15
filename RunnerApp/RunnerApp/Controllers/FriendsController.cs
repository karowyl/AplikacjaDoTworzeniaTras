using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RunnerApp.DTOs;
using RunnerApp.Interfaces;
using System.Security.Claims;

namespace RunnerApp.Controllers
{
    [Authorize]
    public class FriendsController : BaseApiController
    {
        private readonly IFriendRepository _friendRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public FriendsController(IFriendRepository friendRepository, IUserRepository userRepository, IMapper mapper)
        {
            _friendRepository = friendRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpPost("addFriend/{friendId}")]
        public async Task<ActionResult> AddFriend(int friendId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (await _friendRepository.AddFriend(Convert.ToInt32(userId), friendId)) return Ok();
            return BadRequest("Nie udał się proces dodawania znajomego");
        }

        [HttpPost("removeFriend/{friendId}")]
        public async Task<ActionResult> RemoveFriend(int friendId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (await _friendRepository.RemoveFriend(Convert.ToInt32(userId), friendId)) return Ok();
            return BadRequest("Nieudał się proces usuwania znajomego");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetFriends()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var friends = await _friendRepository.GetAllFriends(Convert.ToInt32(userId));

            var usersToReturn = _mapper.Map<IEnumerable<MemberDto>>(friends);
            return Ok(usersToReturn);
        }
    }
}
