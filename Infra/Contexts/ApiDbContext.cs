using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infra.Contexts;

public class ApiDbContext : IdentityDbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }
    public DbSet<Tarefa> Tarefas { get; set; }
    public DbSet<Perfil> Perfis { get; set; }
}