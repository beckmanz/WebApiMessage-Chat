using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("Listar")]
        public async Task<ActionResult<ResponseModel<RequestModel>>> Listar()
        {
            var requests = await _requestInterface.Listar(User);
            return Ok(requests);
        }
        
        [HttpPost("Aceitar/{amigoId}")]
        public async Task<ActionResult<ResponseModel<RequestModel>>> Aceitar(int amigoId)
        {
            var friend = await _requestInterface.Aceitar(amigoId, User);
            return Ok(friend);
        }
        [HttpDelete("Recusar/{amigoId}")]
        public async Task<ActionResult<ResponseModel<RequestModel>>> Recusar(int amigoId)
        {
            var friend = await _requestInterface.Recusar(amigoId, User);
            return Ok(friend);
        }
    }
}
