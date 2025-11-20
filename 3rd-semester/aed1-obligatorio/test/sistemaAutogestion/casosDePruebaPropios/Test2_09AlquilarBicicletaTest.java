package sistemaAutogestion.casosDePruebaPropios;

import static org.junit.Assert.*;
import org.junit.Before;
import org.junit.Test;
import sistemaAutogestion.IObligatorio;
import sistemaAutogestion.Retorno;
import sistemaAutogestion.Sistema;

public class Test2_09AlquilarBicicletaTest {
    private Retorno retorno;
    private final IObligatorio s = new Sistema();

    @Before
    public void setUp() {
        s.crearSistemaDeGestion();
    }
    
    @Test
    public void alquilarBicicleta_Ok() {
        String cedula = "12345678";
        retorno = s.registrarUsuario(cedula, "Juan");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());

        String nombreEstacion = "Estacion1";
        retorno = s.registrarEstacion(nombreEstacion, "Centro", 10);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());

        String codigoBici = "A00001";
        retorno = s.registrarBicicleta(codigoBici, "URBANA");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        retorno = s.asignarBicicletaAEstacion(codigoBici, nombreEstacion);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());

        retorno = s.alquilarBicicleta(cedula, nombreEstacion);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
    }

    @Test
    public void alquilarBicicleta_Error01_Vacio() {
        retorno = s.alquilarBicicleta(null, "Estacion1");
        assertEquals(Retorno.Resultado.ERROR_1, retorno.getResultado());
        
        retorno = s.alquilarBicicleta("", "Estacion1");
        assertEquals(Retorno.Resultado.ERROR_1, retorno.getResultado());
        
        retorno = s.alquilarBicicleta("12345678", null);
        assertEquals(Retorno.Resultado.ERROR_1, retorno.getResultado());
        
        retorno = s.alquilarBicicleta("12345678", "");
        assertEquals(Retorno.Resultado.ERROR_1, retorno.getResultado());
        
        retorno = s.alquilarBicicleta("      ", "Estacion1");
        assertEquals(Retorno.Resultado.ERROR_1, retorno.getResultado());
        
        retorno = s.alquilarBicicleta("12345678", "      ");
        assertEquals(Retorno.Resultado.ERROR_1, retorno.getResultado());
    }
    
    @Test
    public void alquilarBicicleta_Error02_UsuarioInexistente() {
        String nombreEstacion = "Estacion1";
        retorno = s.registrarEstacion(nombreEstacion, "Centro", 10);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());

        String codigoBici = "A00001";
        retorno = s.registrarBicicleta(codigoBici, "URBANA");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        retorno = s.asignarBicicletaAEstacion(codigoBici, nombreEstacion);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());

        retorno = s.alquilarBicicleta("11111111", nombreEstacion);
        assertEquals(Retorno.Resultado.ERROR_2, retorno.getResultado());
    }
    
    @Test
    public void alquilarBicicleta_Error03_EstacionInexistente() {
        String cedula = "12345678";
        retorno = s.registrarUsuario(cedula, "Juan");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());

        retorno = s.alquilarBicicleta(cedula, "EstacionInexistente");
        assertEquals(Retorno.Resultado.ERROR_3, retorno.getResultado());
    }
}
