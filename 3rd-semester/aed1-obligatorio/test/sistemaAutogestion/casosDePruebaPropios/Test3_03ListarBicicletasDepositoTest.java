package sistemaAutogestion.casosDePruebaPropios;

import static org.junit.Assert.assertEquals;
import org.junit.Before;
import org.junit.Test;
import sistemaAutogestion.IObligatorio;
import sistemaAutogestion.Retorno;
import sistemaAutogestion.Sistema;

public class Test3_03ListarBicicletasDepositoTest {
    private Retorno retorno;
    private final IObligatorio s = new Sistema();

    @Before
    public void setUp() {
        s.crearSistemaDeGestion();
    }

    @Test
    public void listarBicisEnDeposito_Ok() {
        s.registrarBicicleta("A12345", "URBANA");    // Mantenimiento
        s.registrarBicicleta("B12345", "ELECTRICA"); // Disponible
        s.registrarBicicleta("C12345", "MOUNTAIN");  // Disponible

        s.marcarEnMantenimiento("A12345", "batería");

        retorno = s.listarBicisEnDeposito();
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        assertEquals("A12345#URBANA#Mantenimiento|B12345#ELECTRICA#Disponible|C12345#MOUNTAIN#Disponible", retorno.getValorString());
    }
}
