using Compartido.DTOs.Paises;
using Compartido.Mappers;
using LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Paises;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosDeUso.ImplementacionCasosDeUso.Paises
{
    public class PaisListado : IPaisListado
    {
        public IRepositorioPais RepoPais { get; set; }

        public PaisListado(IRepositorioPais repoPais)
        {
            RepoPais = repoPais;
        }

        public IEnumerable<DTOPaisListado> Ejecutar()
        {
            List<Pais> Paises = RepoPais.FindAllOrdered().ToList();
            return MapperPais.ListPaisToListDTOPaisListado(Paises);
        }
    }
}