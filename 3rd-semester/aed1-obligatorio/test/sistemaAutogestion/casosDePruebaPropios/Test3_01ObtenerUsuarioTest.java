package sistemaAutogestion.casosDePruebaPropios;

import org.junit.Before;
import org.junit.Test;
import sistemaAutogestion.IObligatorio;
import sistemaAutogestion.Retorno;
import sistemaAutogestion.Sistema;
import static org.junit.Assert.*;

public class Test3_01ObtenerUsuarioTest {

    private Retorno retorno;
    private final IObligatorio s = new Sistema();

    @Before
    public void setUp() {
        s.crearSistemaDeGestion();
    }

    @Test
    public void obtenerUsuarioOk() {
        s.registrarUsuario("12345678", "Usuario01");
        retorno = s.obtenerUsuario("12345678");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        assertEquals("Usuario01#12345678", retorno.getValorString());
    }

    @Test
    public void obtenerUsuarioError01_Vacio() {
        retorno = s.obtenerUsuario(null);
        assertEquals(Retorno.Resultado.ERROR_1, retorno.getResultado());
        retorno = s.obtenerUsuario("");
        assertEquals(Retorno.Resultado.ERROR_1, retorno.getResultado());
        retorno = s.obtenerUsuario("       ");
        assertEquals(Retorno.Resultado.ERROR_1, retorno.getResultado());
    }

    @Test
    public void obtenerUsuarioError02_CedulaInvalida() {
        retorno = s.obtenerUsuario("1");
        assertEquals(Retorno.Resultado.ERROR_2, retorno.getResultado());
        retorno = s.obtenerUsuario("123456789101112");
        assertEquals(Retorno.Resultado.ERROR_2, retorno.getResultado());
        retorno = s.obtenerUsuario("123abcd456");
        assertEquals(Retorno.Resultado.ERROR_2, retorno.getResultado());
        retorno = s.obtenerUsuario("test1234");
        assertEquals(Retorno.Resultado.ERROR_2, retorno.getResultado());
        retorno = s.obtenerUsuario("127.0.0.1");
        assertEquals(Retorno.Resultado.ERROR_2, retorno.getResultado());
        retorno = s.obtenerUsuario("1.234.567-8");
        assertEquals(Retorno.Resultado.ERROR_2, retorno.getResultado());
    }

    @Test
    public void obtenerUsuarioError03_NoExiste() {
        retorno = s.obtenerUsuario("87654321");
        assertEquals(Retorno.Resultado.ERROR_3, retorno.getResultado());
    }

}
