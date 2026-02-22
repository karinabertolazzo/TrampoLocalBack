using Microsoft.EntityFrameworkCore;
using TrampoLocal.API.Models;

namespace TrampoLocal.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Profissional> Profissionais { get; set; }
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<Avaliacao> Avaliacoes { get; set; }
        public DbSet<Categoria> Categorias { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // CPF único
            modelBuilder.Entity<Profissional>()
                .HasIndex(p => p.CPF)
                .IsUnique();

            //  Definindo precisão do decimal Preco
            modelBuilder.Entity<Servico>()
                .Property(s => s.Preco)
                .HasPrecision(10, 2);

            // Relacionamento Profissional -> Servicos
            modelBuilder.Entity<Servico>()
                .HasOne(s => s.Profissional)
                .WithMany(p => p.Servicos)
                .HasForeignKey(s => s.ProfissionalId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacionamento Profissional -> Avaliacoes
            modelBuilder.Entity<Avaliacao>()
                .HasOne(a => a.Profissional)
                .WithMany(p => p.Avaliacoes)
                .HasForeignKey(a => a.ProfissionalId)
                .OnDelete(DeleteBehavior.Cascade);


            // Relacionamento Profissional -> Categoria
            modelBuilder.Entity<Profissional>()
                .HasOne(p => p.Categoria)
                .WithMany(c => c.Profissionais)
                .HasForeignKey(p => p.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}