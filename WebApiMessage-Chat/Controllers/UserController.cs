using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiMessage_Chat.Dto.User;
using WebApiMessage_Chat.Models;
using WebApiMessage_Chat.Services.User;

namespace WebApiMessage_Chat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserInterface _userInterface;

        public UserController(IUserInterface userInterface)
        {
            _userInterface = userInterface;
        }
        
        [HttpPost]
        public async Task<ActionResult<ResponseModel<UserModel>>> Registrar(string Username, string Email, string Password)
        {
            var user = await _userInterface.Registrar(Username, Email, Password);
            return Ok(user);
        }

        [HttpGet("login")]
        public async Task<ActionResult<ResponseModel<UserModel>>> Login(string Email, string Password)
        {
            var user = await _userInterface.Login(Email, Password);
            return Ok(user);
        }
        
        [HttpGet]
        public async Task<ActionResult<ResponseModel<List<UserModel>>>> Listar()
        {
            var users = await _userInterface.Listar();
            return Ok(users);
        }
        
        [HttpGet("{UserId}")]
        public async Task<ActionResult<ResponseModel<UserModel>>> Buscar(int UserId)
        {
            var user = await _userInterface.Buscar(UserId);
            return Ok(user);
        }
        
        [HttpPut("{userId}")]
        public async Task<ActionResult<ResponseModel<UserModel>>> Editar(EditarDto editarDto, int userId)
        {
            var user = await _userInterface.Editar(editarDto, userId);
            return Ok(user);
        }

        [HttpDelete("{userId}")]
        public async Task<ActionResult<ResponseModel<UserModel>>> Excluir(int userId)
        {
            var user = await _userInterface.Excluir(userId);
            return Ok(user);
        }
    }
}
