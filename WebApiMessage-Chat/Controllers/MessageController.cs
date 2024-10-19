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

        [HttpGet("Listar")]
        public async Task<ActionResult<ResponseModel<List<MessageModel>>>> Listar()
        {
            var messages = await _messageInterface.Listar(User);
            return Ok(messages);
        }

        [HttpPost("Enviar")]
        public async Task<ActionResult<ResponseModel<MessageModel>>> Enviar(int targetId, string content)
        {
            var message = await _messageInterface.Enviar(targetId, content, User);
            return Ok(message);
        }
        [HttpDelete("Excluir/{messageId}")]
        public async Task<ActionResult<ResponseModel<MessageModel>>> Excluir(int messageId)
        {
            var message = await _messageInterface.Excluir(messageId, User);
            return Ok(message);
        }
    }
}
