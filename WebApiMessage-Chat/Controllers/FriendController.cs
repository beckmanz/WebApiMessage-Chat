using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiMessage_Chat.Models;
using WebApiMessage_Chat.Services.Friend;

namespace WebApiMessage_Chat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendController : ControllerBase
    {
        private readonly IFriendInterface _friendInterface;

        public FriendController(IFriendInterface friendInterface)
        {
            _friendInterface = friendInterface;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<ResponseModel<List<UserModel>>>> Amigos(int userId)
        {
            var friends = await _friendInterface.Amigos(userId);
            return Ok(friends);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseModel<FriendModel>>> Adicionar(int userId, int targetId)
        {
            var friend = await _friendInterface.Adicionar(userId, targetId);
            return Ok(friend);
        }
    }
}
