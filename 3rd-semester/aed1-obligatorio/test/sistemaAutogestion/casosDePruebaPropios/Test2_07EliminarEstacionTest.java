package sistemaAutogestion.casosDePruebaPropios;

import dominio.Validadores;
import static org.junit.Assert.*;
import org.junit.Before;
import org.junit.Test;
import sistemaAutogestion.IObligatorio;
import sistemaAutogestion.Retorno;
import sistemaAutogestion.Sistema;

public class Test2_07EliminarEstacionTest {
    private Retorno retorno;
    private final IObligatorio s = new Sistema();

    @Before
    public void setUp() {
        s.crearSistemaDeGestion();
    }
    
    @Test
    public void eliminarEstacion_Ok() {
        String nombreEstacion = "E1";
        
        retorno = s.registrarEstacion(nombreEstacion, "alguno", 10);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());

        retorno = s.eliminarEstacion(nombreEstacion);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
    }
    
    @Test
    public void eliminarEstacion_Error01_Vacio() {
        retorno = s.eliminarEstacion("");
        assertEquals(Retorno.Resultado.ERROR_1, retorno.getResultado());
        retorno = s.eliminarEstacion("       ");
        assertEquals(Retorno.Resultado.ERROR_1, retorno.getResultado());
        retorno = s.eliminarEstacion(null);
        assertEquals(Retorno.Resultado.ERROR_1, retorno.getResultado());
    }
    
    @Test
    public void eliminarEstacion_Error02_NoExiste() {
        retorno = s.eliminarEstacion("noexiste");
        assertEquals(Retorno.Resultado.ERROR_2, retorno.getResultado());
    }
    
    @Test
    public void eliminarEstacion_Error03_TienePendientes() {
        String nombreEstacion1 = "E1";
        String nombreEstacion2 = "E2";
        int capacidadEstacion = 10;
        
        retorno = s.registrarEstacion(nombreEstacion1, "alguno", capacidadEstacion);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        retorno = s.registrarEstacion(nombreEstacion2, "alguno", capacidadEstacion);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        
        for (int i = 0; i < capacidadEstacion; i++) {
            String codigo = TestUtils.BicicletaTestUtils.generarCodigo("A", i);
            retorno = s.registrarBicicleta(codigo, "URBANA");
            assertEquals(Retorno.Resultado.OK, retorno.getResultado());

            retorno = s.asignarBicicletaAEstacion(codigo, nombreEstacion1);
            assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        }
        
        // la estacion tieneBicis (llenadas en el for de arriba)
        retorno = s.eliminarEstacion(nombreEstacion1);
        assertEquals(Retorno.Resultado.ERROR_3, retorno.getResultado());

        // registrar mas usuarios que bicis, para vaciar la lista de bicis
        // pero generar una cola de usuarios en espera
        for (int i = 0; i < (capacidadEstacion*2); i++) {
            String cedula = TestUtils.UsuarioTestUtils.generarCedula(i);
            
            retorno = s.registrarUsuario(cedula, "usuario");
            assertEquals(Retorno.Resultado.OK, retorno.getResultado());
            retorno = s.alquilarBicicleta(cedula, nombreEstacion1);
            assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        }
        
        // la estacion tieneUsuariosEspera (llenados en el for de arriba)
        retorno = s.eliminarEstacion(nombreEstacion1);
        assertEquals(Retorno.Resultado.ERROR_3, retorno.getResultado());
        
        // llenar estacion con nuevas bicis, usuarios que hayan quedado 
        // en espera del paso anterior, recibiran su bici y la cola de usuarios
        // en espera quedara vacia.
        for (int i = 0; i < capacidadEstacion; i++) {
            String codigo = TestUtils.BicicletaTestUtils.generarCodigo("B", i);
            retorno = s.registrarBicicleta(codigo, "URBANA");
            assertEquals(Retorno.Resultado.OK, retorno.getResultado());

            retorno = s.asignarBicicletaAEstacion(codigo, nombreEstacion1);
            assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        }
        
        // crear otra estacion, nuevos usuarios que alquilen bicis,
        // y que las devuelvan a la estacion1, generando una cola de bicis
        // a devolver pendientes
        for (int i = 0; i < (capacidadEstacion*2); i++) {
            String codigo = TestUtils.BicicletaTestUtils.generarCodigo("C", i);
            
            retorno = s.registrarBicicleta(codigo, "URBANA");
            assertEquals(Retorno.Resultado.OK, retorno.getResultado());

            retorno = s.asignarBicicletaAEstacion(codigo, nombreEstacion2);
            assertEquals(Retorno.Resultado.OK, retorno.getResultado());
            
            String cedula = TestUtils.UsuarioTestUtils.generarCedula(i);
            
            retorno = s.alquilarBicicleta(cedula, nombreEstacion2);
            assertEquals(Retorno.Resultado.OK, retorno.getResultado());
            
            retorno = s.devolverBicicleta(cedula, nombreEstacion1);
            assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        }
        
        // la estacion tieneBicisEsperaAnclaje (llenados en el for de arriba)
        retorno = s.eliminarEstacion(nombreEstacion1);
        assertEquals(Retorno.Resultado.ERROR_3, retorno.getResultado());
    }
}
