using LogicaAccesoDatos;

using LogicaAccesoDatos.Repositorios;
using LogicaNegocio.InterfacesRepositorio;

using LogicaAplicacion.CasosDeUso.ImplementacionCasosDeUso.Disciplinas;
using LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Disciplinas;

using LogicaAplicacion.CasosDeUso.ImplementacionCasosDeUso.Usuarios;
using LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Usuarios;

using LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Paises;
using LogicaAplicacion.CasosDeUso.ImplementacionCasosDeUso.Paises;

using LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Atletas;
using LogicaAplicacion.CasosDeUso.ImplementacionCasosDeUso.Atletas;

using LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Eventos;
using LogicaAplicacion.CasosDeUso.ImplementacionCasosDeUso.Eventos;

using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;


namespace WebApi
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
            builder.Services.AddScoped<IDisciplinaBuscar, DisciplinaBuscar>();
            builder.Services.AddScoped<IDisciplinaBaja, DisciplinaBaja>();
            builder.Services.AddScoped<IDisciplinaEditar, DisciplinaEditar>();


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
            builder.Services.AddScoped<IAtletaBuscarXIdDisciplina, AtletaBuscarXIdDisciplina>();

            builder.Services.AddScoped<IRepositorioEvento, RepositorioEventoEF>();
            builder.Services.AddScoped<IEventoAlta, EventoAlta>();
            builder.Services.AddScoped<IEventoBuscarId, EventoBuscarId>();
            builder.Services.AddScoped<IEventoBuscarFecha, EventoBuscarFecha>();
            builder.Services.AddScoped<IEventoBuscarIdAtleta, EventoBuscarIdAtleta>();
            builder.Services.AddScoped<IAtletaEventoBuscar, AtletaEventoBuscar>();
            builder.Services.AddScoped<IAtletaEventoPuntuar, AtletaEventoPuntuar>();
            builder.Services.AddScoped<IEventoBuscarFiltros, EventoBuscarFiltros>();

            string connString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ObligatorioContext>(opt => opt.UseSqlServer(connString));

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options => options.IncludeXmlComments("WebApi.xml"));

            var claveSecreta = "ZWRpw6fDo28gZW0gY29tcHV0YWRvcmE=";

            builder.Services.AddAuthentication(aut =>
            {
                aut.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                aut.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(aut =>
            {
                aut.RequireHttpsMetadata = false;
                aut.SaveToken = true;
                aut.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(claveSecreta)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            // trucaso para agregar un botoncito de 'Authorize' en swagger
            // y poder agregar el token antes de hacer las pruebas
            // https://medium.com/@deidra108/oauth-bearer-token-with-swagger-ui-net-6-0-86835e616deb
            builder.Services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Ingrese el token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });

            var app = builder.Build();
            app.UseAuthentication();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            app.MapControllers();

            Precarga.Cargar(app.Services);

            app.Run();
        }
    }
}
