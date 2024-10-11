using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiMessage_Chat.Models;
using WebApiMessage_Chat.Services.Request;

namespace WebApiMessage_Chat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestInterface _requestInterface;

        public RequestController(IRequestInterface requestInterface)
        {
            _requestInterface = requestInterface;
        }
        
        [HttpPost]
        public async Task<ActionResult<ResponseModel<RequestModel>>> Adicionar(int userId, int targetId)
        {
            var friend = await _requestInterface.Adicionar(userId, targetId);
            return Ok(friend);
        }
        
        [HttpPut]
        public async Task<ActionResult<ResponseModel<RequestModel>>> Aceitar(int userId, int amigoId)
        {
            var friend = await _requestInterface.Aceitar(userId, amigoId);
            return Ok(friend);
        }
    }
}
