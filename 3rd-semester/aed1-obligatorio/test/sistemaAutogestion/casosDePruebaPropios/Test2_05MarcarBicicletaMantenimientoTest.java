package sistemaAutogestion.casosDePruebaPropios;

import org.junit.Before;
import org.junit.Test;
import sistemaAutogestion.IObligatorio;
import sistemaAutogestion.Retorno;
import sistemaAutogestion.Sistema;
import static org.junit.Assert.*;

public class Test2_05MarcarBicicletaMantenimientoTest {
    private Retorno retorno;
    private final IObligatorio s = new Sistema();

    @Before
    public void setUp() {
        s.crearSistemaDeGestion();
    }
    
    @Test
    public void marcarEnMantenimiento_Ok() {
        s.registrarBicicleta("M00001", "URBANA");
        retorno = s.marcarEnMantenimiento("M00001", "Rueda pinchada");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
    }
    
    @Test
    public void marcarEnMantenimiento_Error01_Vacio() {
        retorno = s.marcarEnMantenimiento("", "Rueda pinchada");
        assertEquals(Retorno.Resultado.ERROR_1, retorno.getResultado());
        
        retorno = s.marcarEnMantenimiento("M00001", "");
        assertEquals(Retorno.Resultado.ERROR_1, retorno.getResultado());
        
        retorno = s.marcarEnMantenimiento("         ", "    ");
        assertEquals(Retorno.Resultado.ERROR_1, retorno.getResultado());
        
        retorno = s.marcarEnMantenimiento(null, null);
        assertEquals(Retorno.Resultado.ERROR_1, retorno.getResultado());
        
        retorno = s.marcarEnMantenimiento(null, "Rueda pinchada");
        assertEquals(Retorno.Resultado.ERROR_1, retorno.getResultado());
        
        retorno = s.marcarEnMantenimiento("M00001", null);
        assertEquals(Retorno.Resultado.ERROR_1, retorno.getResultado());
    }
    
    @Test
    public void marcarEnMantenimiento_Error02_NoExiste() {
        retorno = s.marcarEnMantenimiento("NOEXIS", "Rueda pinchada");
        assertEquals(Retorno.Resultado.ERROR_2, retorno.getResultado());
    }
    
    @Test
    public void marcarEnMantenimiento_Error03_Alquilada() {
        String nomEstacion = "una estacion",
               codigoBicicleta = "M00003",
               ciUsuario = "12345678";
        
        s.registrarEstacion(nomEstacion, "cualquiera", 5);

        s.registrarBicicleta(codigoBicicleta, "URBANA");
        retorno = s.asignarBicicletaAEstacion(codigoBicicleta, nomEstacion);
        
        s.registrarUsuario(ciUsuario, "yo");
        
        s.alquilarBicicleta(ciUsuario, nomEstacion);
        
        retorno = s.marcarEnMantenimiento(codigoBicicleta, "Rueda pinchada");
        
        assertEquals(Retorno.Resultado.ERROR_3, retorno.getResultado());
    }
    
    @Test
    public void marcarEnMantenimiento_Error04_YaEnMantenimiento() {
        s.registrarBicicleta("M00002", "URBANA");
        retorno = s.marcarEnMantenimiento("M00002", "Rueda pinchada");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        
        retorno = s.marcarEnMantenimiento("M00002", "Rueda pinchada");
        assertEquals(Retorno.Resultado.ERROR_4, retorno.getResultado());
    }
}
