using ExoApi.Models;
using Microsoft.EntityFrameworkCore;


namespace ExoApi.Contexts;

public class ExoContext : DbContext
{
    public ExoContext()
    {
    }

    public ExoContext(DbContextOptions<ExoContext> options) : base(options)
    {
    }

    // vamos utilizar esse metodo para configurar o banco de dados
    protected override void
        OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            //cada provedor tem sua sintaxe para especificaco
            optionsBuilder.UseSqlServer(
                "Server=localhost; Database=exoApi; User Id=sa; Password=ADMIN@admin; Trusted_Connection=false; MultipleActiveResultSets=true;");
        }
    }

    //dbset representa as entidades que serao utilizadas
    public DbSet<Projeto>? Projeto { get; set; }

    public DbSet<Usuario> Usuario { get; set; }
}