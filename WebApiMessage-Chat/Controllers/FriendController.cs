using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiMessage_Chat.Models;
using WebApiMessage_Chat.Services.Friend;

namespace WebApiMessage_Chat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FriendController : ControllerBase
    {
        private readonly IFriendInterface _friendInterface;

        public FriendController(IFriendInterface friendInterface)
        {
            _friendInterface = friendInterface;
        }
        
        [HttpPost("Adicionar/{targetId}")]
        public async Task<ActionResult<ResponseModel<RequestModel>>> Adicionar(int targetId)
        {
            var friend = await _friendInterface.Adicionar(targetId, User);
            return Ok(friend);
        }

        [HttpGet("Amigos")]
        public async Task<ActionResult<ResponseModel<List<UserModel>>>> Amigos()
        {
            var friends = await _friendInterface.Amigos(User);
            return Ok(friends);
        }

        [HttpDelete("Remover/{amigoId}")]
        public async Task<ActionResult<ResponseModel<FriendModel>>> Remover(int amigoId)
        {
            var friend = await _friendInterface.Remover(amigoId, User);
            return Ok(friend);
        }
    }
}
