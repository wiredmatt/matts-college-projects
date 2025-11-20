package sistemaAutogestion.casosDePruebaPropios;

import dominio.Validadores;
import static org.junit.Assert.assertEquals;
import org.junit.Before;
import org.junit.Test;
import sistemaAutogestion.IObligatorio;
import sistemaAutogestion.Retorno;
import sistemaAutogestion.Sistema;

public class Test3_07OcupacionPromedioXBarrioTest {
    private Retorno retorno;
    private final IObligatorio s = new Sistema();

    @Before
    public void setUp() {
        s.crearSistemaDeGestion();
    }

    @Test
    public void ocupacionPromedioXBarrio_Ok_SinEstaciones() {
        retorno = s.ocupacionPromedioXBarrio();
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        assertEquals("", retorno.getValorString());
    }

    @Test
    public void ocupacionPromedioXBarrio_Ok_EstacionesVacias() {
        // Registrar estaciones sin bicis en diferentes barrios
        retorno = s.registrarEstacion("Est1", "Centro", 10);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        
        retorno = s.registrarEstacion("Est2", "Centro", 5);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        
        retorno = s.registrarEstacion("Est3", "Pocitos", 8);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        
        retorno = s.registrarEstacion("Est4", "Malvin", 12);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        
        retorno = s.ocupacionPromedioXBarrio();
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        assertEquals("Centro#0|Malvin#0|Pocitos#0", retorno.getValorString());
    }

    @Test
    public void ocupacionPromedioXBarrio_Ok_EstacionesConBicis() {
        // Registrar estaciones
        s.registrarEstacion("Est1", "Centro", 10);    // 5/10 = 50%
        s.registrarEstacion("Est2", "Centro", 10);    // 2/10 = 20%  -> Centro: 7/20 = 35%
        s.registrarEstacion("Est3", "Pocitos", 8);    // 8/8 = 100%  -> Pocitos: 100%
        s.registrarEstacion("Est4", "Malvin", 12);    // 6/12 = 50%  -> Malvin: 50%
        
        // Registrar y asignar bicis a Est1 (Centro)
        for (int i = 0; i < 5; i++) {
            String codigo = TestUtils.BicicletaTestUtils.generarCodigo("A", i);
            retorno = s.registrarBicicleta(codigo, "URBANA");
            assertEquals(Retorno.Resultado.OK, retorno.getResultado());
            retorno = s.asignarBicicletaAEstacion(codigo, "Est1");
            assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        }

        // Registrar y asignar bicis a Est2 (Centro)
        for (int i = 0; i < 2; i++) {
            String codigo = TestUtils.BicicletaTestUtils.generarCodigo("B", i);
            retorno = s.registrarBicicleta(codigo, "URBANA");
            assertEquals(Retorno.Resultado.OK, retorno.getResultado());
            retorno = s.asignarBicicletaAEstacion(codigo, "Est2");
            assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        }

        // Registrar y asignar bicis a Est3 (Pocitos)
        for (int i = 0; i < 8; i++) {
            String codigo = TestUtils.BicicletaTestUtils.generarCodigo("C", i);
            retorno = s.registrarBicicleta(codigo, "URBANA");
            assertEquals(Retorno.Resultado.OK, retorno.getResultado());
            retorno = s.asignarBicicletaAEstacion(codigo, "Est3");
            assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        }

        // Registrar y asignar bicis a Est4 (Malvin)
        for (int i = 0; i < 6; i++) {
            String codigo = TestUtils.BicicletaTestUtils.generarCodigo("D", i);
            retorno = s.registrarBicicleta(codigo, "URBANA");
            assertEquals(Retorno.Resultado.OK, retorno.getResultado());
            retorno = s.asignarBicicletaAEstacion(codigo, "Est4");
            assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        }
        
        retorno = s.ocupacionPromedioXBarrio();
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        assertEquals("Centro#35|Malvin#50|Pocitos#100", retorno.getValorString());
    }

    @Test
    public void ocupacionPromedioXBarrio_Ok_AlgunosBarriosVacios() {
        // Registrar estaciones
        s.registrarEstacion("Est1", "Centro", 10);    // 5/10 = 50%  -> Centro: 50%
        s.registrarEstacion("Est2", "Pocitos", 8);    // 0/8 = 0%    -> Pocitos: 0%
        s.registrarEstacion("Est3", "Malvin", 12);    // 12/12=100%  -> Malvin: 100%
        
        // Registrar y asignar bicis a Est1 (Centro)
        for (int i = 0; i < 5; i++) {
            String codigo = TestUtils.BicicletaTestUtils.generarCodigo("X", i);
            retorno = s.registrarBicicleta(codigo, "URBANA");
            assertEquals(Retorno.Resultado.OK, retorno.getResultado());
            retorno = s.asignarBicicletaAEstacion(codigo, "Est1");
            assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        }

        // Registrar y asignar bicis a Est3 (Malvin)
        for (int i = 0; i < 12; i++) {
            String codigo = TestUtils.BicicletaTestUtils.generarCodigo("Z", i);
            retorno = s.registrarBicicleta(codigo, "URBANA");
            assertEquals(Retorno.Resultado.OK, retorno.getResultado());
            retorno = s.asignarBicicletaAEstacion(codigo, "Est3");
            assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        }
        
        retorno = s.ocupacionPromedioXBarrio();
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        assertEquals("Centro#50|Malvin#100|Pocitos#0", retorno.getValorString());
    }
}
