using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiMessage_Chat.Models;
using WebApiMessage_Chat.Services.Blocked;

namespace WebApiMessage_Chat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlockedController : ControllerBase
    {
        private readonly IBlockedInterface _blockedInterface;

        public BlockedController(IBlockedInterface blockedInterface)
        {
            _blockedInterface = blockedInterface;
        }
        [HttpGet("{userId}")]
        public async Task<ActionResult<ResponseModel<List<BlockedUserModel>>>> Listar(int userId)
        {
            var user = await _blockedInterface.Listar(userId);
            return Ok(user);
        }
        [HttpPost("{userId}/{blockedId}")]
        public async Task<ActionResult<ResponseModel<UserModel>>> Bloquear(int userId, int blockedId)
        {
            var user = await _blockedInterface.Bloquear(userId, blockedId);
            return Ok(user);
        }
        [HttpDelete("{userId}/{blockedId}")]
        public async Task<ActionResult<ResponseModel<UserModel>>> Desbloquear(int userId, int blockedId)
        {
            var user = await _blockedInterface.Desbloquear(userId, blockedId);
            return Ok(user);
        }
    }
}
