package sistemaAutogestion.casosDePruebaPropios;

import org.junit.Before;
import org.junit.Test;
import sistemaAutogestion.IObligatorio;
import sistemaAutogestion.Retorno;
import sistemaAutogestion.Sistema;
import static org.junit.Assert.*;

public class Test2_02RegistrarEstacionTest {
    private Retorno retorno;
    private final IObligatorio s = new Sistema();

    @Before
    public void setUp() {
        s.crearSistemaDeGestion();
    }

    @Test
    public void registrarEstacion_Ok() {
        retorno = s.registrarEstacion("Estacion01", "Centro", 5);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        
        retorno = s.registrarEstacion("Estacion02", "Aguada", 8);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        
        retorno = s.registrarEstacion("Estacion03", "Ciudad Vieja", 10);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
    }

    @Test
    public void registrarEstacion_Error01_Vacio() {
        retorno = s.registrarEstacion("", "Centro", 5);
        assertEquals(Retorno.Resultado.ERROR_1, retorno.getResultado());

        retorno = s.registrarEstacion("Estacion01", "", 5);
        assertEquals(Retorno.Resultado.ERROR_1, retorno.getResultado());

        retorno = s.registrarEstacion("   ", "Centro", 5);
        assertEquals(Retorno.Resultado.ERROR_1, retorno.getResultado());

        retorno = s.registrarEstacion("Estacion01", "   ", 5);
        assertEquals(Retorno.Resultado.ERROR_1, retorno.getResultado());

        retorno = s.registrarEstacion(null, "Centro", 5);
        assertEquals(Retorno.Resultado.ERROR_1, retorno.getResultado());

        retorno = s.registrarEstacion("Estacion01", null, 5);
        assertEquals(Retorno.Resultado.ERROR_1, retorno.getResultado());
    }

    @Test
    public void registrarEstacion_Error02_CapacidadInvalida() {
        retorno = s.registrarEstacion("Estacion01", "Centro", 0);
        assertEquals(Retorno.Resultado.ERROR_2, retorno.getResultado());

        retorno = s.registrarEstacion("Estacion01", "Centro", -10);
        assertEquals(Retorno.Resultado.ERROR_2, retorno.getResultado());
    }

    @Test
    public void registrarEstacion_Error03_Duplicado() {
        s.registrarEstacion("Estacion01", "Centro", 5);
        retorno = s.registrarEstacion("Estacion01", "Centro", 5);
        assertEquals(Retorno.Resultado.ERROR_3, retorno.getResultado());

    }

}
