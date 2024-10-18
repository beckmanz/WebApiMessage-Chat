using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiMessage_Chat.Dto.User;
using WebApiMessage_Chat.Models;
using WebApiMessage_Chat.Services.Token;
using WebApiMessage_Chat.Services.User;

namespace WebApiMessage_Chat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserInterface _userInterface;
        public readonly ITokenInterface _tokenInterface;

        public UserController(IUserInterface userInterface, ITokenInterface tokenInterface)
        {
            _userInterface = userInterface;
            _tokenInterface = tokenInterface;
        }
        
        [HttpPost("Registrar")]
        public async Task<ActionResult<ResponseModel<UserModel>>> Registrar(string Username, string Email, string Password)
        {
            var user = await _userInterface.Registrar(Username, Email, Password);
            return Ok(user);
        }

        [HttpGet("login")]
        public async Task<ActionResult<ResponseModel<LoginResponseModel>>> GenerateToken(string Email, string Password)
        {
            var user = await _tokenInterface.GenerateToken(Email, Password);
            return Ok(user);
        }
        
        [HttpGet("Listar")]
        [Authorize]
        public async Task<ActionResult<ResponseModel<List<UserModel>>>> Listar()
        {
            var users = await _userInterface.Listar();
            return Ok(users);
        }
        
        [HttpGet("Buscar/{UserId}")]
        [Authorize]
        public async Task<ActionResult<ResponseModel<UserModel>>> Buscar(int UserId)
        {
            var user = await _userInterface.Buscar(UserId);
            return Ok(user);
        }
        
        [HttpPut("Editar")]
        [Authorize]
        public async Task<ActionResult<ResponseModel<UserModel>>> Editar(EditarDto editarDto)
        {
            var user = await _userInterface.Editar(editarDto, User);
            return Ok(user);
        }

        [HttpDelete("Excluir")]
        [Authorize]
        public async Task<ActionResult<ResponseModel<UserModel>>> Excluir(int userId)
        {
            var user = await _userInterface.Excluir(userId);
            return Ok(user);
        }
    }
}
