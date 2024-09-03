using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infra.Contexts;

public class ApiDbContext : IdentityDbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }

    //public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Tarefa> Tarefas { get; set; }
    public DbSet<Perfil> Perfis { get; set; }

    /*protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Tarefa>(entity =>
        {
            entity.HasKey(t => t.Id);

            //entity.Property(t => t.UsuarioId)
                //.IsRequired();

            entity.Property(t => t.Titulo)
                .IsRequired()
                .HasColumnName("Titulo")
                .HasMaxLength(100);

            entity.Property(t => t.Descricao)
                .IsRequired(false)
                .HasColumnName("Descricao")
                .HasMaxLength(1000);
            
            entity.Property(t => t.PrevistaPara)
                .IsRequired()
                .HasColumnName("PrevistaPara")
                .HasColumnType("timestamp");
            
            entity.Property(t => t.Prioridade)
                .IsRequired()
                .HasColumnName("Prioridade")
                .HasConversion<int>();
            
            entity.Property(t => t.FinalizadaEm)
                .IsRequired(false)
                .HasColumnName("FinalizadaEm")
                .HasColumnType("timestamp");

            //entity.HasOne(t => t.Usuario)
                //.WithMany(u => u.Tarefas)
                //.HasForeignKey(t => t.UsuarioId)
                //.OnDelete(DeleteBehavior.Cascade);
        });*/

        /*modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(u => u.Id);

            entity.Property(u => u.Nome)
                .IsRequired()
                .HasColumnName("Nome");
            
            entity.Property(u => u.Ativo)
                .IsRequired()
                .HasColumnName("Ativo")
                .HasColumnType("boolean");

            entity.Property(u => u.DataNascimento)
                .IsRequired()
                .HasColumnName("DataNascimento")
                .HasColumnType("date");

            entity.Property(u => u.Senha)
                .IsRequired()
                .HasColumnName("Senha");

            entity.Property(u => u.Email)
                .IsRequired()
                .HasColumnName("Email");
            
            entity.HasIndex(u => u.Email)
                .IsUnique();
            
            entity.HasMany<Tarefa>(u => u.Tarefas)
                .WithOne(t => t.Usuario)
                .HasForeignKey(t => t.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    } */
}