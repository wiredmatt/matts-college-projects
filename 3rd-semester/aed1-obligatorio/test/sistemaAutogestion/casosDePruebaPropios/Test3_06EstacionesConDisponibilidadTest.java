package sistemaAutogestion.casosDePruebaPropios;

import dominio.Validadores;
import static org.junit.Assert.assertEquals;
import org.junit.Before;
import org.junit.Test;
import sistemaAutogestion.IObligatorio;
import sistemaAutogestion.Retorno;
import sistemaAutogestion.Sistema;

public class Test3_06EstacionesConDisponibilidadTest {
    private Retorno retorno;
    private final IObligatorio s = new Sistema();

    @Before
    public void setUp() {
        s.crearSistemaDeGestion();
    }

    @Test
    public void estacionesConDisponibilidad_Error1_NMenorOIgualA1() {
        retorno = s.estacionesConDisponibilidad(1);
        assertEquals(Retorno.Resultado.ERROR_1, retorno.getResultado());

        retorno = s.estacionesConDisponibilidad(0);
        assertEquals(Retorno.Resultado.ERROR_1, retorno.getResultado());

        retorno = s.estacionesConDisponibilidad(-1);
        assertEquals(Retorno.Resultado.ERROR_1, retorno.getResultado());
    }

    @Test
    public void estacionesConDisponibilidad_Ok_SinEstaciones() {
        retorno = s.estacionesConDisponibilidad(2);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        assertEquals(0, retorno.getValorEntero());
    }

    @Test
    public void estacionesConDisponibilidad_Ok_EstacionesVacias() {
        // Registrar varias estaciones sin bicis
        s.registrarEstacion("Estacion1", "Centro", 5);
        s.registrarEstacion("Estacion2", "Centro", 10);
        s.registrarEstacion("Estacion3", "Centro", 15);

        retorno = s.estacionesConDisponibilidad(2);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        assertEquals(0, retorno.getValorEntero());
    }

    @Test
    public void estacionesConDisponibilidad_Ok_AlgunasEstacionesSuperanN() {
        // Registrar estaciones y bicis
        String[] estaciones = {"Estacion1", "Estacion2", "Estacion3", "Estacion4"};
        int[] capacidades = {5, 10, 15, 20};
        int[] cantidades = {3, 4, 8, 8 };
        String[] charIds = { "A", "B", "C", "D" };

        // Registrar todas las estaciones
        for (int i = 0; i < estaciones.length; i++) {
            retorno = s.registrarEstacion(estaciones[i], "Centro", capacidades[i]);
            assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        }

        for (int i = 0; i < estaciones.length; i++) {
            int cantBicis = cantidades[i];
            
            for (int j = 0; j < cantBicis; j++) {
                String codigo = TestUtils.BicicletaTestUtils.generarCodigo(charIds[i], j);
                retorno = s.registrarBicicleta(codigo, "URBANA");
                assertEquals(Retorno.Resultado.OK, retorno.getResultado());
                retorno = s.asignarBicicletaAEstacion(codigo, estaciones[i]);
                assertEquals(Retorno.Resultado.OK, retorno.getResultado());
            }
        }

        retorno = s.estacionesConDisponibilidad(3);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        assertEquals(3, retorno.getValorEntero()); // E2,E3,34

        retorno = s.estacionesConDisponibilidad(5);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        assertEquals(2, retorno.getValorEntero()); // E3,34

        retorno = s.estacionesConDisponibilidad(8);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        assertEquals(0, retorno.getValorEntero()); // ninguna
    }

    @Test
    public void estacionesConDisponibilidad_Ok_BicicletasEnMantenimiento() {
        // Registrar una estación
        String estacion = "Estacion1";
        retorno = s.registrarEstacion(estacion, "Centro", 10);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());

        // Agregar 5 bicis
        for (int i = 0; i < 5; i++) {
            String codigo = TestUtils.BicicletaTestUtils.generarCodigo("X", i);
            retorno = s.registrarBicicleta(codigo, "URBANA");
            assertEquals(Retorno.Resultado.OK, retorno.getResultado());
            retorno = s.asignarBicicletaAEstacion(codigo, estacion);
            assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        }

        // Verificar inicialmente (5 bicis > 2)
        retorno = s.estacionesConDisponibilidad(2);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        assertEquals(1, retorno.getValorEntero());

        // Poner 3 de las bicis existentes en mantenimiento
        for (int i = 0; i < 3; i++) {
            String codigo = TestUtils.BicicletaTestUtils.generarCodigo("X", i);
            retorno = s.marcarEnMantenimiento(codigo, "Mantenimiento");
            assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        }

        // Verificar después de mantenimiento (2 bicis no superan n=2)
        retorno = s.estacionesConDisponibilidad(2);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        assertEquals(0, retorno.getValorEntero());
    }
}