using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RunnerApp.DTOs;
using RunnerApp.Entities;
using RunnerApp.Interfaces;
using System.Security.Claims;

namespace RunnerApp.Controllers
{
    //[Authorize]
    public class MapController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapRepository _mapRepository;
        public MapController(IUserRepository userRepository, IMapRepository mapRepository)
        {
            _mapRepository = mapRepository;
            _userRepository = userRepository;
        }

        [HttpPost("createMap")]
        public async Task<ActionResult> CreateRoute(MapDto createMapDto)
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value;

            var user = await _userRepository.GetUserByUsernameAsync(username);

            var map = new Map
            {
                City = createMapDto.City,
                CreatedBy = user.Username,
                Polyline = createMapDto.Polyline,
                
            };

            if (await _mapRepository.AddMap(map)) return Ok();
            return BadRequest("Nie udał się proces dodawania trasy");

        }

        [HttpGet("GetMyMaps")]
        public async Task<ActionResult> GetMyRoutes()
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value;

            var routes = await _mapRepository.GetMyMaps(username);

            return Ok(routes);

        }

        [HttpGet("GetAllMaps")]
        public async Task<ActionResult> GetAllRoutes()
        {
            var routes = await _mapRepository.GetAllMaps();

            return Ok(routes);

        }

        [HttpGet("GetMapsInCity/{city}")]
        public async Task<ActionResult> GetAllRoutes(string city)
        {
            var routes = await _mapRepository.GetMapsInTheCity(city);

            return Ok(routes);

        }

        [HttpGet("GetMapsCreatedBy/{username}")]
        public async Task<ActionResult> GetAllRoutesCreatedByUser(string username)
        {
            var routes = await _mapRepository.GetMapsCreatedByUser(username);

            return Ok(routes);

        }


        [HttpPost("AddComment")]
        public async Task<ActionResult> AddComment(CommentDto commentDto)
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value;

            var user = await _userRepository.GetUserByUsernameAsync(username);

            var comment = new Comment
            {
                Context = commentDto.Context,
                CreatedBy = user.Username,
                MapId = commentDto.MapId,
                CreatedDate = DateTime.Now

            };

            if (await _mapRepository.AddComment(comment)) return Ok();
            return BadRequest("Nie udał się proces dodawania komentarza");

        }

    }
}
