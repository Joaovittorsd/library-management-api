using LibraryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace LibraryManagement.Infrastructure.Persistence;

public class LibraryDbContext : DbContext
{
    public DbSet<Livro> Livros {  get; set; }
    public DbSet<Emprestimo> Emprestimos {  get; set; }

    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Livro>(entity =>
        {
            entity.HasKey(l => l.Id);
            entity.Property(l => l.Titulo)
                .IsRequired()
                .HasMaxLength(200);
            entity.Property(l => l.Autor)
                .IsRequired()
                .HasMaxLength(200);
            entity.Property(l => l.AnoPublicacao);
            entity.Property(l => l.QuantidadeDisponivel);
        });

        builder.Entity<Emprestimo>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.DataEmprestimo).IsRequired();
            entity.Property(x => x.Status).IsRequired();
            entity.HasOne<Livro>()
                .WithMany()
                .HasForeignKey(x => x.LivroId)
                .OnDelete(DeleteBehavior.Restrict);
        });

    }
}
