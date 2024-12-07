using LogicaNegocio.Enums;

using Compartido.DTOs.Usuarios;
using LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Usuarios;

using Compartido.DTOs.Disciplinas;
using LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Disciplinas;

using Compartido.DTOs.Paises;

using LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Paises;

using Compartido.DTOs.Atletas;
using LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Atletas;

using Compartido.DTOs.Eventos;
using LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Eventos;

namespace WebApi
{
    public class Precarga
    {
        public static void Cargar(IServiceProvider serviceProvider)
        {
            Console.WriteLine("Comienzo de Precarga...\n");
            CargarUsuarios(serviceProvider);
            CargarDisciplinas(serviceProvider);
            CargarPaises(serviceProvider);
            CargarAtletas(serviceProvider);
            CargarEventos(serviceProvider);
            Console.WriteLine("\nFinal de Precarga, ignorar errores de duplicados.");
        }

        private static void CargarUsuarios(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            var usuarioAlta = scope.ServiceProvider.GetRequiredService<IUsuarioAlta>();

            var dtosUAlta = new List<DTOUsuarioAlta>
                {
                    new DTOUsuarioAlta
                    {
                        Email = "admin@obligatorio.com",
                        Contrasena = "Admin.123",
                        Rol = RolUsuario.Administrador
                    },
                    new DTOUsuarioAlta
                    {
                        Email = "digitador@obligatorio.com",
                        Contrasena = "Digit.123",
                        Rol = RolUsuario.Digitador
                    },
                };

            foreach (var dtoUAlta in dtosUAlta)
            {
                try
                {
                    usuarioAlta.Ejecutar(dtoUAlta);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private static void CargarDisciplinas(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            var disciplinaAlta = scope.ServiceProvider.GetRequiredService<IDisciplinaAlta>();

            var dtosDAlta = new List<DTODisciplinaAlta>
                {
                    new DTODisciplinaAlta
                    {
                        Id = 1234,
                        Nombre = "Atletismoo",
                        Ano = 1896
                    },
                    new DTODisciplinaAlta
                    {
                        Id = 4321,
                        Nombre = "Baloncesto",
                        Ano = 1936
                    },
                    new DTODisciplinaAlta
                    {
                        Id = 7878,
                        Nombre = "Boxeoooooo",
                        Ano = 1904
                    },
                     new DTODisciplinaAlta
                    {
                        Id = 4242,
                        Nombre = "Taekwondoo",
                        Ano = 2000
                    },
                    new DTODisciplinaAlta {
                        Id = 5555,
                        Nombre = "Natacionnn",
                        Ano = 1896
                    },
                    new DTODisciplinaAlta {
                        Id = 6677,
                        Nombre = "Ciclismooo",
                        Ano = 1896
                    },
                    new DTODisciplinaAlta {
                        Id = 1111,
                        Nombre = "Gimnasiaaa",
                        Ano = 1896
                    },
                    new DTODisciplinaAlta {
                        Id = 9999,
                        Nombre = "Voleibolll",
                        Ano = 1964
                    },
                    new DTODisciplinaAlta {
                        Id = 8888,
                        Nombre = "Hockeyyyyy",
                        Ano = 1908
                    },
                    new DTODisciplinaAlta {
                        Id = 7777,
                        Nombre = "Tenissssss",
                        Ano = 1896
                    }
                };

            foreach (var dtoDAlta in dtosDAlta)
            {
                try
                {
                    disciplinaAlta.Ejecutar(dtoDAlta, "admin@obligatorio.com");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private static void CargarPaises(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            var paisAlta = scope.ServiceProvider.GetRequiredService<IPaisAlta>();

            var dtosPAlta = new List<DTOPaisAlta>
                {
                    new DTOPaisAlta
                    {
                        Nombre = "Uruguay",
                        CantidadHabitantes = 3500000,
                        NombreDelegado = "Juan",
                        TelefonoDelegado = "+59899123456"
                    },
                    new DTOPaisAlta
                    {
                        Nombre = "Argentina",
                        CantidadHabitantes = 45000000,
                        NombreDelegado = "Pedro",
                        TelefonoDelegado = "+5493515123456"
                    },
                    new DTOPaisAlta
                    {
                        Nombre = "Brasil",
                        CantidadHabitantes = 200000000,
                        NombreDelegado = "Jose",
                        TelefonoDelegado = "+5511991234567"
                    },
                    new DTOPaisAlta
                    {
                        Nombre = "Paraguay",
                        CantidadHabitantes = 7000000,
                        NombreDelegado = "Luis",
                        TelefonoDelegado = "+595547250210"
                    },
                    new DTOPaisAlta
                    {
                        Nombre = "USA",
                        CantidadHabitantes = 345992498,
                        NombreDelegado = "Christian",
                        TelefonoDelegado = "+12025550123"
                    },
                    new DTOPaisAlta
                    {
                        Nombre = "China",
                        CantidadHabitantes = 1400000000,
                        NombreDelegado = "Xiao",
                        TelefonoDelegado = "+8612345678901"
                    },
                    new DTOPaisAlta
                    {
                        Nombre = "Rusia",
                        CantidadHabitantes = 144000000,
                        NombreDelegado = "Ivan",
                        TelefonoDelegado = "+74951234567"
                    },
                    new DTOPaisAlta
                    {
                        Nombre = "Alemania",
                        CantidadHabitantes = 83000000,
                        NombreDelegado = "Hans",
                        TelefonoDelegado = "+4912345678901"
                    },
                    new DTOPaisAlta
                    {
                        Nombre = "Japon",
                        CantidadHabitantes = 126000000,
                        NombreDelegado = "Takashi",
                        TelefonoDelegado = "+819012345678"
                    },
                    new DTOPaisAlta
                    {
                        Nombre = "Australia",
                        CantidadHabitantes = 25000000,
                        NombreDelegado = "Bruce",
                        TelefonoDelegado = "+61234567890"
                    }
                };

            foreach (var dtoPAlta in dtosPAlta)
            {
                try
                {
                    paisAlta.Ejecutar(dtoPAlta);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private static void CargarAtletas(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            var atletaListado = scope.ServiceProvider.GetRequiredService<IAtletaListado>();

            // note(matt): los atletas no tienen un atributo identificatorio (ademas de su Id) que los haga unicos. 
            if (atletaListado.Ejecutar().Any()) return; // si ya fueron cargados, no los cargamos de nuevo

            var atletaAlta = scope.ServiceProvider.GetRequiredService<IAtletaAlta>();

            var dtosAAlta = new List<DTOAtletaAlta> {
                new DTOAtletaAlta {
                    Nombre = "Juan",
                    Apellido = "Perez",
                    Sexo = Sexo.Masculino,
                    IdPais = 1,
                    IdsDisciplinas = new List<int> { 1234, 4321 }
                },
                new DTOAtletaAlta {
                    Nombre = "Juan",
                    Apellido = "Quino",
                    Sexo = Sexo.Masculino,
                    IdPais = 1,
                    IdsDisciplinas = new List<int> { 4242, 4321 }
                },
                new DTOAtletaAlta {
                    Nombre = "Juan",
                    Apellido = "Acosta",
                    Sexo = Sexo.Masculino,
                    IdPais = 1,
                    IdsDisciplinas = new List<int> { 1234, 7878 }
                },
                new DTOAtletaAlta {
                    Nombre = "Maria",
                    Apellido = "Gonzalez",
                    Sexo = Sexo.Femenino,
                    IdPais = 2,
                    IdsDisciplinas = new List<int> { 4242, 7878 }
                },
                new DTOAtletaAlta {
                    Nombre = "Maria",
                    Apellido = "Rodriguez",
                    Sexo = Sexo.Femenino,
                    IdPais = 2,
                    IdsDisciplinas = new List<int> { 1234, 7777 }
                },
                new DTOAtletaAlta {
                    Nombre = "Xun",
                    Apellido = "Xii",
                    Sexo = Sexo.Masculino,
                    IdPais = 6,
                    IdsDisciplinas = new List<int> { 1234, 4321 }
                },
                new DTOAtletaAlta {
                    Nombre = "Xun",
                    Apellido = "Lii",
                    Sexo = Sexo.Masculino,
                    IdPais = 6,
                    IdsDisciplinas = new List<int> { 4242, 4321 }
                },
                new DTOAtletaAlta {
                    Nombre = "Will",
                    Apellido = "Johnson",
                    Sexo = Sexo.Masculino,
                    IdPais = 5,
                    IdsDisciplinas = new List<int> { 7777, 7878, 4321 }
                },
                new DTOAtletaAlta {
                    Nombre = "Anastasia",
                    Apellido = "Sokolov",
                    Sexo = Sexo.Femenino,
                    IdPais = 7,
                    IdsDisciplinas = new List<int> { 1111, 7777, 7878 }
                },
                new DTOAtletaAlta {
                    Nombre = "Anastasia",
                    Apellido = "Ivanov",
                    Sexo = Sexo.Femenino,
                    IdPais = 7,
                    IdsDisciplinas = new List<int> { 1111, 6677 }
                },
                new DTOAtletaAlta {
                    Nombre = "Karina",
                    Apellido = "Kuznetsova",
                    Sexo = Sexo.Femenino,
                    IdPais = 7,
                    IdsDisciplinas = new List<int> { 1111 }
                },
                new DTOAtletaAlta {
                    Nombre = "Anya",
                    Apellido = "Romanova",
                    Sexo = Sexo.Femenino,
                    IdPais = 7,
                    IdsDisciplinas = new List<int> { 1111 }
                }
            };

            foreach (var dtoAAlta in dtosAAlta)
            {
                try
                {
                    atletaAlta.Ejecutar(dtoAAlta);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private static void CargarEventos(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            var eventoAlta = scope.ServiceProvider.GetRequiredService<IEventoAlta>();
            var puntuar = scope.ServiceProvider.GetRequiredService<IAtletaEventoPuntuar>();

            var dtosEvAlta = new List<DTOEventoAlta>() {
                new DTOEventoAlta {
                    IdDisciplina = 1234, // Atletismoo
                    FechaInicio = new DateOnly(2024, 10, 1),
                    FechaFin = new DateOnly(2024, 10, 5),
                    IdsAtletas = new List<int> { 4, 5, 6 },
                    Nombre = "Carrera mortal de 100km",
                },
                new DTOEventoAlta {
                    IdDisciplina = 1234, // Atletismoo
                    FechaInicio = new DateOnly(2024, 10, 5),
                    FechaFin = new DateOnly(2024, 10, 5),
                    IdsAtletas = new List<int> { 1, 3, 6 },
                    Nombre = "Carrera de 100m",
                },
                new DTOEventoAlta {
                    IdDisciplina = 7878, // Boxeoooooo
                    FechaInicio = new DateOnly(2024, 10, 3),
                    FechaFin = new DateOnly(2024, 10, 4),
                    IdsAtletas = new List<int> { 3, 4, 8 },
                    Nombre = "Combate de pesos pesados",
                },
                new DTOEventoAlta {
                    IdDisciplina = 7777, // Tenissssss
                    FechaInicio = new DateOnly(2024, 10, 4),
                    FechaFin = new DateOnly(2024, 10, 4),
                    IdsAtletas = new List<int> { 5, 6, 7 },
                    Nombre = "Partidito de tenis",
                },
                new DTOEventoAlta {
                    IdDisciplina = 7777, // Tenissssss
                    FechaInicio = new DateOnly(2024, 10, 5),
                    FechaFin = new DateOnly(2024, 10, 5),
                    IdsAtletas = new List<int> { 5, 6, 7 },
                    Nombre = "Partidito de tenis: la revancha",
                },
                new DTOEventoAlta {
                    IdDisciplina = 4321, // Balonceso
                    FechaInicio = new DateOnly(2024, 10, 8),
                    FechaFin = new DateOnly(2024, 10, 10),
                    IdsAtletas = new List<int> { 1, 2, 7, 8 },
                    Nombre = "Partidito de baloncesto",
                },
                new DTOEventoAlta {
                    IdDisciplina = 4321, // Baloncesto
                    FechaInicio = new DateOnly(2024, 10, 8),
                    FechaFin = new DateOnly(2024, 10, 10),
                    IdsAtletas = new List<int> { 1, 2, 7, 8 },
                    Nombre = "Partidito de baloncesto: la revancha",
                },
                new DTOEventoAlta {
                    IdDisciplina = 4242, // Taekwondoo
                    FechaInicio = new DateOnly(2024, 10, 10),
                    FechaFin = new DateOnly(2024, 10, 12),
                    IdsAtletas = new List<int> { 2, 4, 7 },
                    Nombre = "Alto combate de taekwondo",
                },
                new DTOEventoAlta {
                    IdDisciplina = 4242, // Taekwondoo
                    FechaInicio = new DateOnly(2024, 10, 10),
                    FechaFin = new DateOnly(2024, 10, 12),
                    IdsAtletas = new List<int> { 4, 7, 2 },
                    Nombre = "Alto combate de taekwondo: la final",
                },
                new DTOEventoAlta {
                    IdDisciplina = 1111, // Gimnasiaaa
                    FechaInicio = new DateOnly(2024, 10, 11),
                    FechaFin = new DateOnly(2024, 10, 14),
                    IdsAtletas = new List<int> { 9, 10, 11, 12 },
                    Nombre = "Gimnasia artistica",
                },
                new DTOEventoAlta {
                    IdDisciplina = 1111, // Gimnasiaaa
                    FechaInicio = new DateOnly(2024, 10, 11),
                    FechaFin = new DateOnly(2024, 10, 14),
                    IdsAtletas = new List<int> { 10, 11, 12 },
                    Nombre = "Gimnasia artistica: la final",
                },
            };

            // 11x36 = Cantidad de Eventos x Cantidad de participantes
            List<double> puntajes = GenerateRandomPuntajes(11 * 36);
            int i = 0;

            foreach (var dtoEvAlta in dtosEvAlta)
            {
                try
                {
                    DTOEventoListado ev = eventoAlta.Ejecutar(dtoEvAlta);
                    ev.Atletas.ToList().ForEach((a) =>
                    {
                        puntuar.Ejecutar(new DTOAtletaEventoPuntuar
                        {
                            IdAtleta = a.Id,
                            IdEvento = ev.Id,
                            Puntaje = puntajes[i]
                        });

                        i++;
                    });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private static List<double> GenerateRandomPuntajes(int cantidad)
        {
            Random random = new Random();
            List<double> puntajes = new List<double>();

            for (int i = 0; i < cantidad; i++)
            {
                double randomDouble = Math.Round(random.NextDouble() * 10, 1);
                puntajes.Add(randomDouble);
            }

            return puntajes;
        }
    }
}