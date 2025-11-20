package sistemaAutogestion.casosDePruebaPropios;

import org.junit.Before;
import org.junit.Test;
import sistemaAutogestion.IObligatorio;
import sistemaAutogestion.Retorno;
import sistemaAutogestion.Sistema;
import static org.junit.Assert.*;

public class Test2_04RegistrarBicicletaTest {
    private Retorno retorno;
    private final IObligatorio s = new Sistema();

    @Before
    public void setUp() {
        s.crearSistemaDeGestion();
    }
    
    @Test
    public void registrarBicicleta_Ok() {
        retorno = s.registrarBicicleta("A12345", "URBANA");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        retorno = s.registrarBicicleta("B12345", "MOUNTAIN");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        retorno = s.registrarBicicleta("C12345", "ELECTRICA");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
    }
    
    @Test
    public void registrarBicicleta_Error01_Vacio() {
        retorno = s.registrarBicicleta("", "URBANA");
        assertEquals(Retorno.Resultado.ERROR_1, retorno.getResultado());
        
        retorno = s.registrarBicicleta("A12345", "");
        assertEquals(Retorno.Resultado.ERROR_1, retorno.getResultado());
        
        retorno = s.registrarBicicleta("", "");
        assertEquals(Retorno.Resultado.ERROR_1, retorno.getResultado());
        
        retorno = s.registrarBicicleta(" ", "      ");
        assertEquals(Retorno.Resultado.ERROR_1, retorno.getResultado());
        
        retorno = s.registrarBicicleta(null, "URBANA");
        assertEquals(Retorno.Resultado.ERROR_1, retorno.getResultado());
        
        retorno = s.registrarBicicleta("A12345", null);
        assertEquals(Retorno.Resultado.ERROR_1, retorno.getResultado());
        
        retorno = s.registrarBicicleta(null, null);
        assertEquals(Retorno.Resultado.ERROR_1, retorno.getResultado());
    }

    @Test
    public void registrarBicicleta_Error02_CodigoInvalido() {
        retorno = s.registrarBicicleta("A", "URBANA");     // < 6
        assertEquals(Retorno.Resultado.ERROR_2, retorno.getResultado());
        retorno = s.registrarBicicleta("ABCDEFG", "URBANA"); // > 6
        assertEquals(Retorno.Resultado.ERROR_2, retorno.getResultado());
    }
    
    @Test
    public void registrarBicicleta_Error03_TipoInvalido() {
        retorno = s.registrarBicicleta("A12345", "UNTIPOQUENOEXISTE");
        assertEquals(Retorno.Resultado.ERROR_3, retorno.getResultado());
    }
    
    @Test
    public void registrarBicicleta_Error04_CodigoYaExiste() {
        retorno = s.registrarBicicleta("A12345", "URBANA");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        
        retorno = s.registrarBicicleta("A12345", "ELECTRICA");
        assertEquals(Retorno.Resultado.ERROR_4, retorno.getResultado());
    }
}
