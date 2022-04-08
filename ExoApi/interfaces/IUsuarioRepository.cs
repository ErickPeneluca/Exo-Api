using ExoApi.Models;

namespace ExoApi.interfaces;

public interface IUsuarioRepository
{
    Usuario Login(string email, string senha);
}