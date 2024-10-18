using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiMessage_Chat.Models;
using WebApiMessage_Chat.Services.Message;

namespace WebApiMessage_Chat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MessageController : ControllerBase
    {
        private readonly IMessageInterface _messageInterface;

        public MessageController(IMessageInterface messageInterface)
        {
            _messageInterface = messageInterface;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<ResponseModel<List<MessageModel>>>> Listar(int userId)
        {
            var messages = await _messageInterface.Listar(userId);
            return Ok(messages);
        }

        [HttpPost("Enviar")]
        public async Task<ActionResult<ResponseModel<MessageModel>>> Enviar(int userId, int targetId, string content)
        {
            var message = await _messageInterface.Enviar(userId, targetId, content);
            return Ok(message);
        }
        [HttpDelete("Excluir/{userId}/{messageId}")]
        public async Task<ActionResult<ResponseModel<MessageModel>>> Excluir(int userId, int messageId)
        {
            var message = await _messageInterface.Excluir(userId, messageId);
            return Ok(message);
        }
    }
}
