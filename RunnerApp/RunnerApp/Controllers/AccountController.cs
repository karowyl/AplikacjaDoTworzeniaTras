using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunnerApp.Data;
using RunnerApp.DTOs;
using RunnerApp.Entities;
using RunnerApp.Interfaces;
using System;
using System.Security.Cryptography;
using System.Text;

namespace RunnerApp.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

        public AccountController(DataContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (string.IsNullOrEmpty(registerDto.Country) ||
                string.IsNullOrEmpty(registerDto.Password) ||
                string.IsNullOrEmpty(registerDto.City))
                return BadRequest("Uzupełnij wszystkie pola");

            if (await UserExists(registerDto.Username)) return BadRequest("Nazwa użytkownika już istnieje");
            
            using var hmac = new HMACSHA512();

            var user = new AppUser
            {
                Username = registerDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key,
                City = registerDto.City,
                Country = registerDto.Country,
                DateOfBirth = registerDto.DateOfBirth,
                CreatedDate = DateTime.Now,
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserDto
            {
                Username = user.Username,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _context.Users
                .SingleOrDefaultAsync(x => x.Username == loginDto.Username);

            if (user == null) return Unauthorized("Niepoprawna nazwa użytkownika");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Niepoprawne hasło");
            }

            return new UserDto
            {
                Username = user.Username,
                Token = _tokenService.CreateToken(user)
            };
        }

        private async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(x => x.Username == username.ToLower());
        }
    }
}
