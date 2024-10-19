using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiMessage_Chat.Models;
using WebApiMessage_Chat.Services.Blocked;

namespace WebApiMessage_Chat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BlockedController : ControllerBase
    {
        private readonly IBlockedInterface _blockedInterface;

        public BlockedController(IBlockedInterface blockedInterface)
        {
            _blockedInterface = blockedInterface;
        }
        [HttpGet("Listar")]
        public async Task<ActionResult<ResponseModel<List<BlockedUserModel>>>> Listar()
        {
            var user = await _blockedInterface.Listar(User);
            return Ok(user);
        }
        [HttpPost("Bloquear/{blockedId}")]
        public async Task<ActionResult<ResponseModel<UserModel>>> Bloquear(int blockedId)
        {
            var user = await _blockedInterface.Bloquear(blockedId, User);
            return Ok(user);
        }
        [HttpDelete("Desbloquear/{blockedId}")]
        public async Task<ActionResult<ResponseModel<UserModel>>> Desbloquear(int blockedId)
        {
            var user = await _blockedInterface.Desbloquear(blockedId, User);
            return Ok(user);
        }
    }
}
