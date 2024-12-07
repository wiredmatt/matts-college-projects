using LogicaNegocio.Enums;
using LogicaNegocio.Entidades;
using LogicaNegocio.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LogicaAccesoDatos
{
    public class ObligatorioContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Atleta> Atletas { get; set; }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<AtletaEvento> AtletaEvento { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        public ObligatorioContext(DbContextOptions opt) : base(opt) { }

        // NOTE(matt): notice that we're not overriding this method, but rather
        // creating a new one with the same name, but with a different signature.
        // This allows me to call `SaveChanges(email)` wherever I want to audit
        // a change, just and `SaveChanges()` where I don't care about auditing.
        public int SaveChanges(string? userEmail)
        {
            if (userEmail != null) {
                AddAuditLogs(userEmail);
            }

            return base.SaveChanges();
        }

        private void AddAuditLogs(string userEmail)
        {
            var entries = ChangeTracker.Entries();

            var auditLogs = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted)
                .Select(entry => CreateAuditLog(entry, userEmail))
                .Where(auditLog => auditLog != null)
                .ToList() ?? [];

            if (auditLogs != null)
                AuditLogs.AddRange(auditLogs);
        }

        private AuditLog? CreateAuditLog(EntityEntry? entry, string emailUsuario)
        {
            if (entry == null) return null;
            if (entry.Entity is AuditLog) return null;

            // NOTE(matt): IAuditable is an interface implemented by all entities that we care to audit
            // if you wanted to audit ALL entities, this check can be removed!
            if (entry.Entity is not IAuditable) return null;

            var entityName = entry.Entity.GetType().Name;
            // NOTE(matt): Not necessarily all entities have an `Id` property.
            // An improvement to how this work would be to use reflection to get 
            // the primary key property of a given entity.
            var entityId = entry.Property("Id").CurrentValue;

            return new AuditLog
            {
                TipoEntidad = entityName,
                IdEntidad = (int)entityId!,
                Operacion = entry.State switch
                {
                    EntityState.Added => "Creado",
                    EntityState.Modified => "Editado",
                    EntityState.Deleted => "Eliminado",
                    _ => throw new InvalidOperationException()
                },
                Fecha = DateTime.UtcNow,
                EmailUsuario = emailUsuario,
            };
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Usuario>()
                .Property(u => u.Email)
                .HasConversion(
                    e => e.Valor,
                    v => new EmailUsuario(v));
            modelBuilder
                .Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique();
            modelBuilder
                .Entity<Usuario>()
                .Property(u => u.Contrasena)
                .HasConversion(
                    c => c.Valor,
                    v => new ContrasenaUsuario(v));
            // see https://learn.microsoft.com/en-us/ef/core/modeling/value-conversions?tabs=data-annotations#configuring-a-value-converter
            modelBuilder.Entity<Usuario>()
                .Property(u => u.Rol)
                .HasConversion(
                    n => n.ToString(),
                    v => (RolUsuario)Enum.Parse(typeof(RolUsuario), v));
            // A user can be created by another user
            modelBuilder
                .Entity<Usuario>()
                .HasOne(u => u.Creador)
                .WithMany()
                .HasForeignKey(u => u.IdCreador);

            modelBuilder
                .Entity<Disciplina>()
                .Property(d => d.Id)
                .ValueGeneratedNever(); // necesario para poder insertar un Id manualmente (codigo).
            modelBuilder
                .Entity<Disciplina>()
                .Property(d => d.Nombre)
                .HasConversion(
                    n => n.Valor,
                    v => new NombreDisciplina(v));
            modelBuilder
                .Entity<Disciplina>()
                .HasIndex(d => d.Nombre)
                .IsUnique();

            modelBuilder
                .Entity<Pais>()
                .Property(p => p.Nombre)
                .HasConversion(
                    n => n.Valor,
                    v => new NombrePais(v));
            modelBuilder
                .Entity<Pais>()
                .HasIndex(p => p.Nombre)
                .IsUnique();

            modelBuilder
                .Entity<Atleta>()
                .Property(a => a.Sexo)
                .HasConversion(
                    n => n.ToString(),
                    v => (Sexo)Enum.Parse(typeof(Sexo), v));
            modelBuilder
                .Entity<Atleta>()
                .HasOne(a => a.Pais)
                .WithMany()
                .HasForeignKey(a => a.IdPais);
            modelBuilder
                .Entity<Atleta>()
                .HasMany(a => a.Disciplinas)
                .WithMany(d => d.Atletas)
                .UsingEntity<AtletaDisciplina>(
                    j => j.HasOne<Disciplina>().WithMany().HasForeignKey(ad => ad.IdDisciplina),
                    j => j.HasOne<Atleta>().WithMany().HasForeignKey(ad => ad.IdAtleta),
                    j => j.HasKey(ad => new { ad.IdAtleta, ad.IdDisciplina })
                );
            modelBuilder.Entity<Atleta>()
                .HasMany(a => a.Eventos)
                .WithMany(e => e.Atletas)
                .UsingEntity<AtletaEvento>(
                    j => j.HasOne<Evento>().WithMany(e => e.AtletaEventos).HasForeignKey(ae => ae.IdEvento),
                    j => j.HasOne<Atleta>().WithMany(a => a.AtletaEventos).HasForeignKey(ae => ae.IdAtleta),
                    j =>
                    {
                        j.HasKey(ae => new { ae.IdAtleta, ae.IdEvento });
                        j.Property(ae => ae.Puntaje).HasDefaultValue(0.0);
                    }
                );

            modelBuilder
                .Entity<Evento>()
                .Property(e => e.Nombre)
                .HasConversion(
                    n => n.Valor,
                    v => new NombreEvento(v));
            modelBuilder
                .Entity<Evento>()
                .HasIndex(e => e.Nombre)
                .IsUnique();
            modelBuilder
                .Entity<Evento>()
                .HasOne(e => e.Disciplina)
                .WithMany()
                .HasForeignKey(e => e.IdDisciplina);

            modelBuilder.Entity<AtletaEvento>()
                .Property(ae => ae.Puntaje)
                .HasDefaultValue(0.0);
        }
    }
}