using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiMessage_Chat.Models;
using WebApiMessage_Chat.Services.Message;

namespace WebApiMessage_Chat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
    }
}
