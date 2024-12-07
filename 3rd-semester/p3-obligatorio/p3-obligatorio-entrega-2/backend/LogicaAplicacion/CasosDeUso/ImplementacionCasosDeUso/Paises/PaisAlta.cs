using Compartido.DTOs.Paises;
using Compartido.Mappers;
using LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Paises;
using LogicaNegocio.InterfacesRepositorio;


namespace LogicaAplicacion.CasosDeUso.ImplementacionCasosDeUso.Paises
{
    public class PaisAlta : IPaisAlta
    {
        public IRepositorioPais RepoPais { get; set; }

        public PaisAlta(IRepositorioPais repoPais)
        {
            RepoPais = repoPais;
        }

        public void Ejecutar(DTOPaisAlta dtoPais)
        {
            RepoPais.Add(MapperPais.DTOPaisAltaToPais(dtoPais));
        }
    }
}
