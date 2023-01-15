using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RunnerApp.DTOs;
using RunnerApp.Entities;
using RunnerApp.Interfaces;
using RunnerApp.Extensions;
using RunnerApp.Helpers;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace RunnerApp.Controllers
{
    [Authorize]
    public class MessagesController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;
        public MessagesController(IUserRepository userRepository, IMessageRepository messageRepository,
            IMapper mapper)
        {
            _mapper = mapper;
            _messageRepository = messageRepository;
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<ActionResult<MessageDto>> CreateMessage(CreateMessageDto createMessageDto)
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value;

            if (username == createMessageDto.RecipientUsername.ToLower())
                return BadRequest("You cannot send messages to yourself");

            var sender = await _userRepository.GetUserByUsernameAsync(username);
            var recipient = await _userRepository.GetUserByUsernameAsync(createMessageDto.RecipientUsername);

            if (recipient == null) return NotFound();

            var message = new Message
            {
                Sender = sender,
                Recipient = recipient,
                SenderUsername = sender.Username,
                RecipientUsername = recipient.Username,
                Content = createMessageDto.Content
            };

            _messageRepository.AddMessage(message);
            if (await _messageRepository.SaveAllAsync()) return Ok(_mapper.Map<MessageDto>(message));

            return BadRequest("Nie udał się proces wysłania wiadomości");

        }

        [HttpGet]
        public async Task<ActionResult<PagedList<MessageDto>>> GetMessagesForUser([FromQuery]
            MessageParams messageParams)
        {
            messageParams.Username = User.FindFirst(ClaimTypes.Name)?.Value;

            var messages = await _messageRepository.GetMessagesForUser(messageParams);

            Response.AddPaginationHeader(new PaginationHeader(messages.CurrentPage, messages.PageSize,
                messages.TotalCount, messages.TotalPages));

            return messages;
        }

        [HttpGet("thread/{username}")]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessagesThread(string username)
        {
            var currentUsername = User.FindFirst(ClaimTypes.Name)?.Value;

            return Ok(await _messageRepository.GetMessageThread(currentUsername, username));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveMessage(int id)
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value;

            var message = await _messageRepository.GetMessageId(id);

            if (message.SenderUsername != username && message.RecipientUsername != username) return Unauthorized();

            if (message.SenderUsername == username)
                message.SenderDeleted = true;

            if (message.RecipientDeleted)
                message.RecipientDeleted = true;

            if (message.SenderDeleted && message.RecipientDeleted)
            {
                _messageRepository.RemoveMessage(message);
            }

            if (await _messageRepository.SaveAllAsync()) return Ok();

            return BadRequest("Problem z usunięciem wiadomości");
        }
    }
}
