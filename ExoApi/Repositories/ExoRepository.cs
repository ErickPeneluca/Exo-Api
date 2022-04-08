using ExoApi.Contexts;
using ExoApi.Models;

namespace ExoApi.Repositories;

public class ExoRepository
{
    private readonly ExoContext _context;

    public ExoRepository(ExoContext context)
    {
        _context = context;
    }

    public List<Projeto> Listar()
    {
        return _context.Projeto.ToList();
    }

    public Projeto BuscarPorId(int id)
    {
        return _context.Projeto.Find(id);
    }

    public void Criar(Projeto projeto)
    {
        _context.Projeto.Add(projeto);

        _context.SaveChanges();
    }

    public void Atualizar(int id, Projeto projeto)
    {
        Projeto projetoBuscado = _context.Projeto.Find(id);

        if (projetoBuscado != null)
        {
            projetoBuscado.Titulo = projeto.Titulo;
            projetoBuscado.Status = projeto.Status;
            projetoBuscado.DataInicio = projeto.DataInicio;
            projetoBuscado.Tecnologias = projeto.Tecnologias;
            projetoBuscado.Requisitos = projeto.Requisitos;
        }

        _context.Projeto.Update(projetoBuscado);

        _context.SaveChanges();
    }

    public void Deletar(int id)
    {
        Projeto projetoBuscado = _context.Projeto.Find(id);

        _context.Projeto.Remove(projetoBuscado);

        _context.SaveChanges();
    }
}