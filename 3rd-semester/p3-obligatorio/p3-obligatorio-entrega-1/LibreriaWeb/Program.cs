using LogicaAccesoDatos;

using LogicaAccesoDatos.Repositorios;
using LogicaNegocio.InterfacesRepositorio;

using LogicaAplicacion.CasosDeUso.ImplementacionCasosDeUso.Disciplinas;
using LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Disciplinas;

using LogicaAplicacion.CasosDeUso.ImplementacionCasosDeUso.Usuarios;
using LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Usuarios;

using LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Paises;
using LogicaAplicacion.CasosDeUso.ImplementacionCasosDeUso.Paises;

using Microsoft.EntityFrameworkCore;
using LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Atletas;
using LogicaAplicacion.CasosDeUso.ImplementacionCasosDeUso.Atletas;
using LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Eventos;
using LogicaAplicacion.CasosDeUso.ImplementacionCasosDeUso.Eventos;

namespace LibreriaWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddSession();

            builder.Services.AddScoped<IRepositorioDisciplina, RepositorioDisciplinaEF>();
            builder.Services.AddScoped<IDisciplinaAlta, DisciplinaAlta>();
            builder.Services.AddScoped<IDisciplinaListado, DisciplinaListado>();

            builder.Services.AddScoped<IRepositorioUsuario, RepositorioUsuarioEF>();
            builder.Services.AddScoped<IUsuarioLogin, UsuarioLogin>();
            builder.Services.AddScoped<IUsuarioAlta, UsuarioAlta>();
            builder.Services.AddScoped<IUsuarioListado, UsuarioListado>();
            builder.Services.AddScoped<IUsuarioBaja, UsuarioBaja>();
            builder.Services.AddScoped<IUsuarioBuscar, UsuarioBuscar>();
            builder.Services.AddScoped<IUsuarioEditar, UsuarioEditar>();

            builder.Services.AddScoped<IRepositorioPais, RepositorioPaisEF>();
            builder.Services.AddScoped<IPaisAlta, PaisAlta>();
            builder.Services.AddScoped<IPaisListado, PaisListado>();

            builder.Services.AddScoped<IRepositorioAtleta, RepositorioAtletaEF>();
            builder.Services.AddScoped<IAtletaAlta, AtletaAlta>();
            builder.Services.AddScoped<IAtletaEditar, AtletaEditar>();
            builder.Services.AddScoped<IAtletaListado, AtletaListado>();
            builder.Services.AddScoped<IAtletaBuscar, AtletaBuscar>();

            builder.Services.AddScoped<IRepositorioEvento, RepositorioEventoEF>();
            builder.Services.AddScoped<IEventoAlta, EventoAlta>();
            builder.Services.AddScoped<IEventoBuscarId, EventoBuscarId>();
            builder.Services.AddScoped<IEventoBuscarFecha, EventoBuscarFecha>();
            builder.Services.AddScoped<IEventoBuscarIdAtleta, EventoBuscarIdAtleta>();
            builder.Services.AddScoped<IAtletaEventoBuscar, AtletaEventoBuscar>();
            builder.Services.AddScoped<IAtletaEventoPuntuar, AtletaEventoPuntuar>();

            string connString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ObligatorioContext>(opt => opt.UseSqlServer(connString));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            Precarga.Cargar(app.Services);

            app.Run();
        }
    }
}
