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
        public async Task<ActionResult<ResponseModel<UserModel>>> Registrar(RegisterDto register)
        {
            var user = await _userInterface.Registrar(register);
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ResponseModel<LoginResponseModel>>> GenerateToken(LoginDto login)
        {
            var user = await _tokenInterface.GenerateToken(login);
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
        public async Task<ActionResult<ResponseModel<UserModel>>> Excluir()
        {
            var user = await _userInterface.Excluir(User);
            return Ok(user);
        }
    }
}
