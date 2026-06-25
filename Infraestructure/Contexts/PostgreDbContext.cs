using Microsoft.EntityFrameworkCore;
using domain.Entities;
using Infraestructure.Contexts.Interceptors;

namespace Infraestructure.Contexts
{
    public class PostgreDbContext : DbContext
    {
        #region Entidades
        public DbSet<NegocioEntity> negocio { get; set; } // TODO: Criar DbSet para cada entidade do domínio
        public DbSet<ClienteEntity> cliente { get; set; }
        #endregion

        #region Construtores
        private readonly RlsInterceptor _rlsInterceptor;
        public PostgreDbContext(DbContextOptions<PostgreDbContext> options, RlsInterceptor rlsInterceptor) : base(options)
        {
            _rlsInterceptor = rlsInterceptor;
        }
        #endregion

        #region Configuracao
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_rlsInterceptor);
            base.OnConfiguring(optionsBuilder);
        }
        #endregion

        #region Modelos

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("order");
            modelBuilder.Entity<NegocioEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ComplexProperty(e => e.Email, emailBuilder =>
                {
                    emailBuilder.Property(e => e.Valor).HasColumnName("Email").IsRequired().HasMaxLength(255);
                });

                entity.Property(e => e.Nome).IsRequired().HasMaxLength(150);
                entity.Property(e => e.Documento).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Telefone).IsRequired().HasMaxLength(20);

                entity.Property(e => e.DeletadoEm).HasColumnName("DeletadoEm");
                entity.HasQueryFilter(e => e.DeletadoEm == null);

            });
            modelBuilder.Entity<ClienteEntity>(entity => { 
                entity.HasKey(e => e.Id);
                entity.ComplexProperty(e => e.Email, emailBuilder =>
                {
                    emailBuilder.Property(e => e.Valor).HasColumnName("Email").IsRequired().HasMaxLength(255);
                });
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(150);
                entity.Property(e => e.Documento).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Telefone).IsRequired().HasMaxLength(20);
                entity.Property(e => e.DeletadoEm).HasColumnName("DeletadoEm");
                entity.HasOne(c => c.Negocio)
                    .WithMany()
                    .HasForeignKey(c => c.NegocioId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasQueryFilter(e => e.DeletadoEm == null);
            });
            base.OnModelCreating(modelBuilder);
        }

        #endregion

    }
}
