using Microsoft.EntityFrameworkCore;
using WebApiMessage_Chat.Data;
using WebApiMessage_Chat.Dto.User;
using WebApiMessage_Chat.Models;

namespace WebApiMessage_Chat.Services.User;

public class UserServices : IUserInterface
{
    private readonly AppDbContext _context;

    public UserServices( AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel<UserModel>> Registrar(string Username, string Email, string Password)
    {
        ResponseModel<UserModel> resposta = new ResponseModel<UserModel>();
        try
        {
            var user = await _context.Users.AnyAsync( u => u.Email == Email);
            if (user)
            {
                resposta.Mensagem = "Já existe um usuário registrado com esse email, use outro email!!";
                return resposta;
            }
            
            
            var HashPassword = BCrypt.Net.BCrypt.HashPassword(Password);

            var newUser = new UserModel()
            {
                Username = Username,
                Email = Email,
                PasswordHash = HashPassword,

            };

            _context.Add(newUser);
            await _context.SaveChangesAsync();

            resposta.Dados = newUser;
            resposta.Mensagem = "Usuário registrado com sucesso!!";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<UserModel>> Login(string Email, string Password)
    {
        ResponseModel<UserModel> resposta = new ResponseModel<UserModel>();
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync( u => u.Email == Email);
            if (user == null)
            {
                resposta.Mensagem = "Usuário não encontrado, verifique o email e tente novamente!!";
                return resposta;
            }
            
            if (!BCrypt.Net.BCrypt.Verify(Password, user.PasswordHash))
            {
                resposta.Mensagem = "Senha incorreta, verifique a senha e tente novamente!!";
                return resposta;
            }

            resposta.Dados = user;
            resposta.Mensagem = "Usuário logado com sucesso!!";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<UserModel>>> Listar()
    {
        ResponseModel<List<UserModel>> resposta = new ResponseModel<List<UserModel>>();
        try
        {
            var users = await _context.Users.ToListAsync();

            resposta.Dados = users;
            resposta.Mensagem = "Todos os usuários foram coletados!!";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<UserModel>> Buscar(int UserId)
    {
        ResponseModel<UserModel> resposta = new ResponseModel<UserModel>();
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == UserId);
            if (user == null)
            {
                resposta.Mensagem = "Usuário não encontrado, verifique o Id e tente novamente!!";
                return resposta;
            }
            
            resposta.Dados = user;
            resposta.Mensagem = "Usuário encontrado com sucesso!!";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<UserModel>> Editar(EditarDto editarDto, int UserId)
    {
        ResponseModel<UserModel> resposta = new ResponseModel<UserModel>();
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == UserId);
            if (user == null)
            {
                resposta.Mensagem = "Usuário não encontrado, verifique o Id e tente novamente!!";
                return resposta;
            }
            
            var HashPassword = BCrypt.Net.BCrypt.HashPassword(editarDto.Password);

            user.Username = editarDto.Username;
            user.Email = editarDto.Email;
            user.PasswordHash = HashPassword;
            user.UpdateAt = DateTime.Now;
            

            _context.Update(user);
            await _context.SaveChangesAsync();
            
            resposta.Dados = user;
            resposta.Mensagem = "Usuário atualizado com sucesso!!";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<UserModel>> Excluir(int UserId)
    {
        ResponseModel<UserModel> resposta = new ResponseModel<UserModel>();
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == UserId);
            if (user == null)
            {
                resposta.Mensagem = "Usuário não encontrado, verifique o Id e tente novamente!!";
                return resposta;
            }

            _context.Remove(user);
            await _context.SaveChangesAsync();
            
            resposta.Dados = user;
            resposta.Mensagem = "Usuário excluído com sucesso!!";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }
}