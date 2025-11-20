package sistemaAutogestion.casosDePruebaPropios;

import static org.junit.Assert.assertEquals;
import org.junit.Before;
import org.junit.Test;
import sistemaAutogestion.IObligatorio;
import sistemaAutogestion.Retorno;
import sistemaAutogestion.Sistema;

public class Test2_06RepararBicicletaTest {
    private Retorno retorno;
    private final IObligatorio s = new Sistema();

    @Before
    public void setUp() {
        s.crearSistemaDeGestion();
    }
    
    @Test
    public void repararBicicleta_Ok() {
        String codigo = "M00001";
        
        s.registrarBicicleta(codigo, "URBANA");
        
        retorno = s.marcarEnMantenimiento(codigo, "Rueda pinchada");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        
        retorno = s.repararBicicleta(codigo);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
    }
    
    @Test
    public void repararBicicleta_Error01_Vacio() {
        retorno = s.repararBicicleta("");
        assertEquals(Retorno.Resultado.ERROR_1, retorno.getResultado());
        retorno = s.repararBicicleta("       ");
        assertEquals(Retorno.Resultado.ERROR_1, retorno.getResultado());
        retorno = s.repararBicicleta(null);
        assertEquals(Retorno.Resultado.ERROR_1, retorno.getResultado());
    }
    
    @Test
    public void repararBicicleta_Error02_NoExisteONoEstaEnDeposito() {
        // inexistente
        retorno = s.repararBicicleta("A00001");
        assertEquals(Retorno.Resultado.ERROR_2, retorno.getResultado());
        
        // existe, pero no esta en el deposito
        String codigo = "M00001",
                nombreEstacion = "unaEstacion";
        s.registrarBicicleta(codigo, "URBANA");
        s.registrarEstacion(nombreEstacion, "unbarrio", 10);
        s.asignarBicicletaAEstacion(codigo, nombreEstacion);
        retorno = s.repararBicicleta(codigo);
        assertEquals(Retorno.Resultado.ERROR_2, retorno.getResultado());   
    }
    
    @Test
    public void repararBicicleta_Error03_NoEnMantenimiento() {
        String codigo2 = "M00001";
        s.registrarBicicleta(codigo2, "ELECTRICA");
        retorno = s.repararBicicleta(codigo2);
        
        assertEquals(Retorno.Resultado.ERROR_3, retorno.getResultado());
    }
}
