package sistemaAutogestion.casosDePruebaPropios;

import dominio.Validadores;
import static org.junit.Assert.assertEquals;
import org.junit.Before;
import org.junit.Test;
import sistemaAutogestion.IObligatorio;
import sistemaAutogestion.Retorno;
import sistemaAutogestion.Sistema;

public class Test2_08AsignarBicicletaAEstacionTest {
    private Retorno retorno;
    private final IObligatorio s = new Sistema();

    @Before
    public void setUp() {
        s.crearSistemaDeGestion();
    }
    
    @Test
    public void asignarBicicletaAEstacion_Ok() {
        String nombreEstacion = "E1";
        
        retorno = s.registrarEstacion(nombreEstacion, "alguno", 10);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());

        String codigo = "A00001";
        retorno = s.registrarBicicleta(codigo, "URBANA");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());

        retorno = s.asignarBicicletaAEstacion(codigo, nombreEstacion);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
    }
    
    @Test
    public void asignarBicicletaAEstacion_Error01_Vacio() {
        retorno = s.asignarBicicletaAEstacion("", "unaestacion");
        assertEquals(Retorno.Resultado.ERROR_1, retorno.getResultado());
        retorno = s.asignarBicicletaAEstacion("A000002", "");
        assertEquals(Retorno.Resultado.ERROR_1, retorno.getResultado());
        retorno = s.asignarBicicletaAEstacion("   ", "  ");
        assertEquals(Retorno.Resultado.ERROR_1, retorno.getResultado());
        retorno = s.asignarBicicletaAEstacion(null, null);
        assertEquals(Retorno.Resultado.ERROR_1, retorno.getResultado());
    }
    
    @Test
    public void asignarBicicletaAEstacion_Error02_NoExisteONoDisponible() {
        String nombreEstacion = "E1";

        retorno = s.registrarEstacion(nombreEstacion, "alguno", 10);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        
        String codigo = "A00001";
        retorno = s.registrarBicicleta(codigo, "URBANA");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        
        retorno = s.marcarEnMantenimiento(codigo, "x");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());

        // la bicicleta existe pero esta en mantenimiento
        retorno = s.asignarBicicletaAEstacion(codigo, nombreEstacion);
        assertEquals(Retorno.Resultado.ERROR_2, retorno.getResultado());

        // la bicicleta no existe
        retorno = s.asignarBicicletaAEstacion("B00001", nombreEstacion);
        assertEquals(Retorno.Resultado.ERROR_2, retorno.getResultado());
    }
    
    @Test
    public void asignarBicicletaAEstacion_Error03_NoExisteEstacion() {
        String codigo = "C00001";
        retorno = s.registrarBicicleta(codigo, "URBANA");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        
        retorno = s.asignarBicicletaAEstacion(codigo, "noexiste");
        assertEquals(Retorno.Resultado.ERROR_3, retorno.getResultado());
    }
    
    @Test
    public void asignarBicicletaAEstacion_Error04_EstacionSinAnclajesLibres() {
        String nombreEstacion = "E1";
        int capacidadEstacion = 20;

        retorno = s.registrarEstacion(nombreEstacion, "alguno", capacidadEstacion);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        
        for (int i = 0; i < capacidadEstacion; i++) {
            String codigo = TestUtils.BicicletaTestUtils.generarCodigo("F", i);
            retorno = s.registrarBicicleta(codigo, "URBANA");
            assertEquals(Retorno.Resultado.OK, retorno.getResultado());
            retorno = s.asignarBicicletaAEstacion(codigo, nombreEstacion);
            assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        }
        
        retorno = s.registrarBicicleta("X00001", "URBANA");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());

        retorno = s.asignarBicicletaAEstacion("X00001", nombreEstacion);
        assertEquals(Retorno.Resultado.ERROR_4, retorno.getResultado());
    }
}
