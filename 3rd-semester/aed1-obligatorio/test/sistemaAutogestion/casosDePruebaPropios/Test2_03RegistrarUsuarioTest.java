package sistemaAutogestion.casosDePruebaPropios;

import static org.junit.Assert.assertEquals;
import org.junit.Before;
import org.junit.Test;
import sistemaAutogestion.IObligatorio;
import sistemaAutogestion.Retorno;
import sistemaAutogestion.Sistema;

public class Test2_03RegistrarUsuarioTest {
    private Retorno retorno;
    private final IObligatorio s = new Sistema();
    
    @Before
    public void setUp() {
        s.crearSistemaDeGestion();
    }
    
    @Test
    public void registrarUsuario_Ok() {
        retorno = s.registrarUsuario("12345678", "Juan");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        retorno = s.registrarUsuario("87654321", "Pedro");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        retorno = s.registrarUsuario("12700111", "Diego");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
    }
    
    @Test
    public void registrarUsuario_Error01_Vacio() {
        retorno = s.registrarUsuario("12345678", "Juan");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
    }
    
    @Test
    public void registrarUsuario_Error02_FormatoCedula() {
        retorno = s.registrarUsuario("123", "Juan"); // < 8 digitos
        assertEquals(Retorno.Resultado.ERROR_2, retorno.getResultado());
        retorno = s.registrarUsuario("123456789", "Pedro");   // > 8 digitos
        assertEquals(Retorno.Resultado.ERROR_2, retorno.getResultado());
        retorno = s.registrarUsuario("123test1", "Diego");    // no numerico
        assertEquals(Retorno.Resultado.ERROR_2, retorno.getResultado());
    }
    
    @Test
    public void registrarUsuario_Error03_Duplicado() {
        s.registrarUsuario("11111111", "Juan");
        retorno = s.registrarUsuario("11111111", "Pedro");
        assertEquals(Retorno.Resultado.ERROR_3, retorno.getResultado());
    }
}
