package sistemaAutogestion.casosDePruebaPropios;

import static org.junit.Assert.*;
import org.junit.Before;
import org.junit.Test;
import sistemaAutogestion.IObligatorio;
import sistemaAutogestion.Retorno;
import sistemaAutogestion.Sistema;

public class Test2_10DevolverBicicletaTest {
    private Retorno retorno;
    private final IObligatorio s = new Sistema();

    @Before
    public void setUp() {
        s.crearSistemaDeGestion();
    }
    
    @Test
    public void devolverBicicleta_Ok() {
        String cedula = "12345678";
        retorno = s.registrarUsuario(cedula, "Juan");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());

        String estacionOrigen = "EstacionOrigen";
        retorno = s.registrarEstacion(estacionOrigen, "Centro", 10);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());

        String estacionDestino = "EstacionDestino";
        retorno = s.registrarEstacion(estacionDestino, "Centro", 10);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());

        String codigoBici = "A00001";
        retorno = s.registrarBicicleta(codigoBici, "URBANA");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        retorno = s.asignarBicicletaAEstacion(codigoBici, estacionOrigen);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());

        retorno = s.alquilarBicicleta(cedula, estacionOrigen);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());

        retorno = s.devolverBicicleta(cedula, estacionDestino);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
    }

    @Test
    public void devolverBicicleta_Error01_Vacio() {
        retorno = s.devolverBicicleta(null, "Estacion1");
        assertEquals(Retorno.Resultado.ERROR_1, retorno.getResultado());
        
        retorno = s.devolverBicicleta("", "Estacion1");
        assertEquals(Retorno.Resultado.ERROR_1, retorno.getResultado());
        
        retorno = s.devolverBicicleta("12345678", null);
        assertEquals(Retorno.Resultado.ERROR_1, retorno.getResultado());
        
        retorno = s.devolverBicicleta("12345678", "");
        assertEquals(Retorno.Resultado.ERROR_1, retorno.getResultado());
        
        retorno = s.devolverBicicleta("      ", "Estacion1");
        assertEquals(Retorno.Resultado.ERROR_1, retorno.getResultado());
        
        retorno = s.devolverBicicleta("12345678", "      ");
        assertEquals(Retorno.Resultado.ERROR_1, retorno.getResultado());
    }
    
    @Test
    public void devolverBicicleta_Error02_UsuarioInexistenteOSinBici() {
        retorno = s.devolverBicicleta("11111111", "Estacion1");
        assertEquals(Retorno.Resultado.ERROR_2, retorno.getResultado());

        String cedula = "12345678";
        retorno = s.registrarUsuario(cedula, "Juan");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        
        retorno = s.devolverBicicleta(cedula, "Estacion1");
        assertEquals(Retorno.Resultado.ERROR_2, retorno.getResultado());
    }
    
    @Test
    public void devolverBicicleta_Error03_EstacionInexistente() {
        String cedula = "12345678";
        retorno = s.registrarUsuario(cedula, "Juan");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());

        String estacionOrigen = "EstacionOrigen";
        retorno = s.registrarEstacion(estacionOrigen, "Centro", 10);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());

        String codigoBici = "A00001";
        retorno = s.registrarBicicleta(codigoBici, "URBANA");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        retorno = s.asignarBicicletaAEstacion(codigoBici, estacionOrigen);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());

        retorno = s.alquilarBicicleta(cedula, estacionOrigen);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());

        retorno = s.devolverBicicleta(cedula, "EstacionInexistente");
        assertEquals(Retorno.Resultado.ERROR_3, retorno.getResultado());
    }
}
