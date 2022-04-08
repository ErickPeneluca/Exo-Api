using ExoApi.Contexts;
using ExoApi.interfaces;
using ExoApi.Models;

namespace ExoApi.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly ExoContext _context;

    public UsuarioRepository(ExoContext context)
    {
        _context = context;
    }

    public Usuario Login(string email, string senha)
    {
        return _context.Usuario.FirstOrDefault(usuario => usuario.Email == email && usuario.Senha == senha);
    }
}