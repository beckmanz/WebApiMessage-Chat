using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApiMessage_Chat.Data;
using WebApiMessage_Chat.Dto.User;
using WebApiMessage_Chat.Models;

namespace WebApiMessage_Chat.Services.Token;

public class TokenService : ITokenInterface
{
    public readonly AppDbContext _context;
    public readonly IConfiguration _configuration;
    public TokenService(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<ResponseModel<LoginResponseModel>> GenerateToken(LoginDto login)
    {
        ResponseModel<LoginResponseModel> response = new ResponseModel<LoginResponseModel>();
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == login.Email);
            if (user == null)
            {
                response.Mensagem = "Usuário não encontrado, verifique o email e tente novamente!!";
                return response;
            }
            
            if (!BCrypt.Net.BCrypt.Verify(login.Password, user.PasswordHash))
            {
                response.Mensagem = "Senha incorreta, verifique a senha e tente novamente!!";
                return response;
            }

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? String.Empty));
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken
                (
                issuer: issuer,
                audience: audience,
                claims: new []
                {
                    new Claim(type: ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(type: ClaimTypes.Name, user.Username)
                },
                expires: DateTime.Now.AddHours(4),
                signingCredentials: signingCredentials
                );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            
            var UserData = new LoginResponseModel()
            {
                Name = user.Username,
                Token = token
            };
            
            response.Mensagem = "Usuário logado e token gerado com sucesso!!";
            response.Dados = UserData;
            return response;
        }
        catch (Exception ex)
        {
            response.Mensagem = ex.Message;
            response.Status = false;
            return response;
        }
    }
}