using LogicaNegocio.Entidades;
using LogicaNegocio.ExcepcionesEntidades.Eventos;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.EntityFrameworkCore;

namespace LogicaAccesoDatos.Repositorios
{
    public class RepositorioEventoEF : IRepositorioEvento
    {
        public ObligatorioContext DbCtx { get; set; }

        public RepositorioEventoEF(ObligatorioContext dbCtx)
        {
            DbCtx = dbCtx;
        }

        public Evento Add(Evento item)
        {
            var eventoYaExiste = DbCtx.Eventos.Any(e => e.Nombre == item.Nombre);

            if (eventoYaExiste)
                throw new ExceptionEvento("Ya existe un Evento con ese Nombre.");

            DbCtx.Eventos.Add(item);
            DbCtx.SaveChanges();

            return item;
        }

        public Evento FindById(int id)
        {
            return DbCtx.Eventos
                        .Include(e => e.Atletas)
                            .ThenInclude(a => a.Pais)
                        .Include(e => e.Atletas)
                            .ThenInclude(a => a.Disciplinas)
                        .Include(e => e.Disciplina)
                        .AsEnumerable()
                        .FirstOrDefault(u => u.Id == id) ?? throw new ExceptionEvento("No existe un Evento con ese Id");
        }

        public AtletaEvento FindParticipacionByEventoIdAndAtletaId(int idEvento, int idAtleta)
        {
            return DbCtx.AtletaEvento
                           .AsEnumerable()
                           .FirstOrDefault(ae => ae.IdEvento == idEvento && ae.IdAtleta == idAtleta) ?? throw new ExceptionEvento("No se encontró la Participación solicitada");
        }

        public IEnumerable<Evento> FindByDate(DateOnly date)
        {
            return DbCtx.Eventos
                        .Include(e => e.Atletas)
                            .ThenInclude(a => a.Pais)
                        .Include(e => e.Atletas)
                            .ThenInclude(a => a.Disciplinas)
                        .Include(e => e.Disciplina)
                        .AsEnumerable()
                        .Where(e => e.FechaInicio == date).ToList();
        }

        public IEnumerable<Evento> FindAll()
        {
            return DbCtx.Eventos
                        .Include(e => e.Atletas)
                            .ThenInclude(a => a.Pais)
                        .Include(e => e.Atletas)
                            .ThenInclude(a => a.Disciplinas)
                        .Include(e => e.Disciplina)
                        .AsEnumerable();
        }

        public void SetAtletaEventoPuntaje(int idEvento, int idAtleta, double puntaje)
        {
            if (puntaje < 0.0)
                throw new ExceptionEvento("El puntaje debe ser mayor o igual a 0");

            var atletaEvento = DbCtx.AtletaEvento.FirstOrDefault(ae => ae.IdEvento == idEvento && ae.IdAtleta == idAtleta) ?? throw new ExceptionEvento("El Atleta no está asignado a ese Evento");

            atletaEvento.Puntaje = puntaje;

            DbCtx.SaveChanges();
        }

        public IEnumerable<Evento> FindAllByIdAtleta(int idAtleta)
        {
            var atletaEventos = DbCtx.AtletaEvento.Where(ae => ae.IdAtleta == idAtleta).ToList();
            var eventos = DbCtx.Eventos
                             .Include(e => e.Disciplina)
                             .AsEnumerable()
                             .Where(e => atletaEventos.Any(ae => ae.IdEvento == e.Id))
                             .OrderBy(e => e.Disciplina.Nombre)
                             .ToList();

            return eventos;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Evento Update(Evento item, int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Evento> FindByDateRange(DateOnly? inicio, DateOnly? fin)
        {
            var query = DbCtx.Eventos
                            .Include(e => e.Atletas)
                                .ThenInclude(a => a.Pais)
                            .Include(e => e.Atletas)
                            .ThenInclude(a => a.Disciplinas)
                            .Include(e => e.Disciplina)
                            .AsQueryable();

            // NOTE(matt): c# tira errores si intentamos comparar un type con null
            if (inicio != null && fin != null)
            {
                query = query.Where(e => e.FechaInicio >= inicio && e.FechaFin <= fin);
            }
            else if (inicio != null)
            {
                query = query.Where(e => e.FechaInicio >= inicio);
            }
            else if (fin != null)
            {
                query = query.Where(e => e.FechaFin <= fin);
            }

            return query.AsEnumerable();
        }

        public IEnumerable<Evento> FindByIdDisciplina(int idDisciplina)
        {
            return DbCtx.Eventos
                        .Include(e => e.Atletas)
                            .ThenInclude(a => a.Pais)
                        .Include(e => e.Atletas)
                            .ThenInclude(a => a.Disciplinas)
                        .Include(e => e.Disciplina)
                        .AsEnumerable()
                        .Where(e => e.IdDisciplina == idDisciplina);
        }

        public IEnumerable<Evento> FindByNameLike(string name)
        {
            return DbCtx.Eventos
                        .Include(e => e.Atletas)
                            .ThenInclude(a => a.Pais)
                        .Include(e => e.Atletas)
                            .ThenInclude(a => a.Disciplinas)
                        .Include(e => e.Disciplina)
                        .AsEnumerable()
                        .Where(e => e.Nombre.Valor.ToLower().Contains(name.ToLower()));
        }

        public IEnumerable<Evento> FindByPuntajeRange(double? minimo, double? maximo)
        {
            var query = DbCtx.Eventos
                            .Include(e => e.Atletas)
                                .ThenInclude(a => a.Pais)
                            .Include(e => e.Atletas)
                                .ThenInclude(a => a.Disciplinas)
                            .Include(e => e.Disciplina)
                            .Include(e => e.AtletaEventos)
                            .AsQueryable();

            if (minimo != null && maximo != null)
            {
                query = query.Where(e => e.AtletaEventos.Any(ae => ae.Puntaje >= minimo && ae.Puntaje <= maximo));
            }
            else if (minimo != null)
            {
                query = query.Where(e => e.AtletaEventos.Any(ae => ae.Puntaje >= minimo));
            }
            else if (maximo != null)
            {
                query = query.Where(e => e.AtletaEventos.Any(ae => ae.Puntaje <= maximo));
            }

            return query.AsEnumerable();
        }
    }
}