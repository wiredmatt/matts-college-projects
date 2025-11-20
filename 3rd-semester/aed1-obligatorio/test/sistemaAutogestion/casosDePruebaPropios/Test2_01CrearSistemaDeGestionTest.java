package sistemaAutogestion.casosDePruebaPropios;

import org.junit.Test;
import sistemaAutogestion.IObligatorio;
import sistemaAutogestion.Retorno;
import sistemaAutogestion.Sistema;
import static org.junit.Assert.*;

public class Test2_01CrearSistemaDeGestionTest {

    private Retorno retorno;
    private final IObligatorio s = new Sistema();

    @Test
    public void testCrearSistemaDeGestion() {
        retorno = s.crearSistemaDeGestion();
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
    }

}
