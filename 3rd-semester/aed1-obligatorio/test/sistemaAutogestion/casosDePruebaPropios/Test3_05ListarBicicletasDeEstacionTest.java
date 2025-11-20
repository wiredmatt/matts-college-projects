package sistemaAutogestion.casosDePruebaPropios;

import static org.junit.Assert.assertEquals;
import org.junit.Before;
import org.junit.Test;
import sistemaAutogestion.IObligatorio;
import sistemaAutogestion.Retorno;
import sistemaAutogestion.Sistema;

public class Test3_05ListarBicicletasDeEstacionTest {
    private Retorno retorno;
    private final IObligatorio s = new Sistema();

    @Before
    public void setUp() {
        s.crearSistemaDeGestion();
    }

    @Test
    public void listarBicicletasDeEstacion_Ok_EstacionVacia() {
        String nombreEstacion = "EstacionVacia";
        retorno = s.registrarEstacion(nombreEstacion, "Centro", 10);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());

        retorno = s.listarBicicletasDeEstacion(nombreEstacion);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        assertEquals("", retorno.getValorString());
    }

    @Test
    public void listarBicicletasDeEstacion_Ok_UnaBicicleta() {
        String nombreEstacion = "Estacion1";
        retorno = s.registrarEstacion(nombreEstacion, "Centro", 10);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());

        String codigoBici = "A00001";
        retorno = s.registrarBicicleta(codigoBici, "URBANA");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());

        retorno = s.asignarBicicletaAEstacion(codigoBici, nombreEstacion);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());

        retorno = s.listarBicicletasDeEstacion(nombreEstacion);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        assertEquals(codigoBici, retorno.getValorString());
    }

    @Test
    public void listarBicicletasDeEstacion_Ok_MultipleBicicletasOrdenadas() {
        String nombreEstacion = "Estacion1";
        retorno = s.registrarEstacion(nombreEstacion, "Centro", 10);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());

        String[] codigosBicis = {"C00003", "A00001", "B00002"};
        for (String codigo : codigosBicis) {
            retorno = s.registrarBicicleta(codigo, "URBANA");
            assertEquals(Retorno.Resultado.OK, retorno.getResultado());
            retorno = s.asignarBicicletaAEstacion(codigo, nombreEstacion);
            assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        }

        retorno = s.listarBicicletasDeEstacion(nombreEstacion);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        assertEquals("A00001|B00002|C00003", retorno.getValorString());
    }

    @Test
    public void listarBicicletasDeEstacion_Error1_NombreVacio() {
        retorno = s.listarBicicletasDeEstacion(null);
        assertEquals(Retorno.Resultado.ERROR_1, retorno.getResultado());

        retorno = s.listarBicicletasDeEstacion("");
        assertEquals(Retorno.Resultado.ERROR_1, retorno.getResultado());

        retorno = s.listarBicicletasDeEstacion("    ");
        assertEquals(Retorno.Resultado.ERROR_1, retorno.getResultado());
    }

    @Test
    public void listarBicicletasDeEstacion_Error2_EstacionInexistente() {
        retorno = s.listarBicicletasDeEstacion("EstacionInexistente");
        assertEquals(Retorno.Resultado.ERROR_2, retorno.getResultado());
    }

    @Test
    public void listarBicicletasDeEstacion_Ok_BicicletasConDistintosEstados() {
        String nombreEstacion = "Estacion1";
        retorno = s.registrarEstacion(nombreEstacion, "Centro", 10);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());

        String[] codigosBicis = {"A00001", "B00002", "C00003"};
        for (String codigo : codigosBicis) {
            retorno = s.registrarBicicleta(codigo, "URBANA");
            assertEquals(Retorno.Resultado.OK, retorno.getResultado());
            retorno = s.asignarBicicletaAEstacion(codigo, nombreEstacion);
            assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        }

        retorno = s.marcarEnMantenimiento("B00002", "Mantenimiento preventivo");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());

        retorno = s.listarBicicletasDeEstacion(nombreEstacion);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        assertEquals("A00001|C00003", retorno.getValorString());
    }
}
