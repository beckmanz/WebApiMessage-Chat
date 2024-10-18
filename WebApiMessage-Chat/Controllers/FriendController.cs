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
        
        [HttpPost("Adicionar/{userId}/{targetId}")]
        public async Task<ActionResult<ResponseModel<RequestModel>>> Adicionar(int userId, int targetId)
        {
            var friend = await _friendInterface.Adicionar(userId, targetId);
            return Ok(friend);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<ResponseModel<List<UserModel>>>> Amigos(int userId)
        {
            var friends = await _friendInterface.Amigos(userId);
            return Ok(friends);
        }

        [HttpDelete("Remover/{userId}/{amigoId}")]
        public async Task<ActionResult<ResponseModel<FriendModel>>> Remover(int userId, int amigoId)
        {
            var friend = await _friendInterface.Remover(userId, amigoId);
            return Ok(friend);
        }
    }
}
