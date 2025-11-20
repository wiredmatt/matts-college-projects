package sistemaAutogestion.casosDePruebaPropios;

import static org.junit.Assert.*;
import org.junit.Before;
import org.junit.Test;
import sistemaAutogestion.IObligatorio;
import sistemaAutogestion.Retorno;
import sistemaAutogestion.Sistema;

public class Test2_11DeshacerUltimosRetirosTest {
    private Retorno retorno;
    private final IObligatorio s = new Sistema();

    @Before
    public void setUp() {
        s.crearSistemaDeGestion();
    }

    @Test
    public void deshacerUltimosRetiros_Ok() {
        String cedula1 = "11111111", cedula2 = "22222222";
        retorno = s.registrarUsuario(cedula1, "Usuario1");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        retorno = s.registrarUsuario(cedula2, "Usuario2");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());

        String estacion = "Estacion1";
        retorno = s.registrarEstacion(estacion, "Centro", 10);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());

        String bici1 = "A00001", bici2 = "A00002";
        retorno = s.registrarBicicleta(bici1, "URBANA");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        retorno = s.registrarBicicleta(bici2, "URBANA");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());

        retorno = s.asignarBicicletaAEstacion(bici1, estacion);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        retorno = s.asignarBicicletaAEstacion(bici2, estacion);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());

        retorno = s.alquilarBicicleta(cedula1, estacion);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        retorno = s.alquilarBicicleta(cedula2, estacion);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());

        retorno = s.deshacerUltimosRetiros(1);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        assertTrue(retorno.getValorString().contains(cedula2 + "#" + bici2 + "#" + estacion));

        retorno = s.deshacerUltimosRetiros(1);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        assertTrue(retorno.getValorString().contains(cedula1 + "#" + bici1 + "#" + estacion));
    }

    @Test
    public void deshacerUltimosRetiros_Error01_NInvalido() {
        retorno = s.deshacerUltimosRetiros(0);
        assertEquals(Retorno.Resultado.ERROR_1, retorno.getResultado());

        retorno = s.deshacerUltimosRetiros(-1);
        assertEquals(Retorno.Resultado.ERROR_1, retorno.getResultado());

        retorno = s.deshacerUltimosRetiros(-100);
        assertEquals(Retorno.Resultado.ERROR_1, retorno.getResultado());
    }

    @Test
    public void deshacerUltimosRetiros_Ok_DeshacerMasQueDisponibles() {
        String cedula = "11111111";
        retorno = s.registrarUsuario(cedula, "Usuario1");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());

        String estacion = "Estacion1";
        retorno = s.registrarEstacion(estacion, "Centro", 10);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());

        String bici = "A00001";
        retorno = s.registrarBicicleta(bici, "URBANA");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        retorno = s.asignarBicicletaAEstacion(bici, estacion);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());

        retorno = s.alquilarBicicleta(cedula, estacion);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());

        retorno = s.deshacerUltimosRetiros(5);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        assertTrue(retorno.getValorString().contains(cedula + "#" + bici + "#" + estacion));
    }
}
