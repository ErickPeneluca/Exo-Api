using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ExoApi.interfaces;
using ExoApi.Models;
using ExoApi.VIewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ExoApi.Controllers;

[Produces("application/json")]
[Route("api/[controller]")]
[ApiController]
public class LoginController : Controller
{
    private readonly IUsuarioRepository _iUsuarioRepository;

    public LoginController(IUsuarioRepository iUsuarioRepository)
    {
        _iUsuarioRepository = iUsuarioRepository;
    }
    /// <summary>
    /// Essa requisicao verifica de forma segura o usuario e a senha no banco de dados com os parametros padroes de login
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult Login(LoginViewModel login)
    {
        try
        {
            Usuario usuarioEncontrado = _iUsuarioRepository.Login(login.Email, login.Senha);

            if (usuarioEncontrado == null)
            {
                return Unauthorized(new {msg = "Email e/ou senha inválidos"});
            }

            var minhasClaims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, usuarioEncontrado.Email),
                new Claim(JwtRegisteredClaimNames.Jti, usuarioEncontrado.Id.ToString())
            };

            var chave = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("exoapi-chave-autenticacao"));

            var credencials = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var meuToken = new JwtSecurityToken(
                issuer: "exoapi.webapi",
                audience: "exoapi.webapi",
                claims: minhasClaims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credencials
            );

            return Ok(
                new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(meuToken),
                }
            );
        }
        catch (Exception)
        {
            throw;
        }
    }
}