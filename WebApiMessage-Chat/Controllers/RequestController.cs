using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiMessage_Chat.Models;
using WebApiMessage_Chat.Services.Request;

namespace WebApiMessage_Chat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RequestController : ControllerBase
    {
        private readonly IRequestInterface _requestInterface;

        public RequestController(IRequestInterface requestInterface)
        {
            _requestInterface = requestInterface;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<ResponseModel<RequestModel>>> Listar(int userId)
        {
            var requests = await _requestInterface.Listar(userId);
            return Ok(requests);
        }
        
        [HttpPost("Aceitar/{userId}/{amigoId}")]
        public async Task<ActionResult<ResponseModel<RequestModel>>> Aceitar(int userId, int amigoId)
        {
            var friend = await _requestInterface.Aceitar(userId, amigoId);
            return Ok(friend);
        }
        [HttpDelete("Recusar/{userId}/{amigoId}")]
        public async Task<ActionResult<ResponseModel<RequestModel>>> Recusar(int userId, int amigoId)
        {
            var friend = await _requestInterface.Recusar(userId, amigoId);
            return Ok(friend);
        }
    }
}
