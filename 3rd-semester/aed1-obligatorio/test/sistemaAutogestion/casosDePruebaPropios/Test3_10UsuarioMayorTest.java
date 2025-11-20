package sistemaAutogestion.casosDePruebaPropios;

import dominio.Validadores;
import static org.junit.Assert.assertEquals;
import org.junit.Before;
import org.junit.Test;
import sistemaAutogestion.IObligatorio;
import sistemaAutogestion.Retorno;
import sistemaAutogestion.Sistema;

public class Test3_10UsuarioMayorTest {
    private Retorno retorno;
    private final IObligatorio s = new Sistema();

    @Before
    public void setUp() {
        s.crearSistemaDeGestion();
    }

    @Test
    public void testSistemaVacio() {
        // En un sistema sin usuarios, debe retornar string vacío
        retorno = s.usuarioMayor();
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        assertEquals("", retorno.getValorString());
    }

    @Test
    public void testUnSoloUsuario() {
        // Sistema con un único usuario
        s.registrarUsuario("55555555", "Juan");
        
        // Sin alquileres
        retorno = s.usuarioMayor();
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        assertEquals("55555555", retorno.getValorString());

        // Agregar estación y bicicleta para hacer un alquiler
        s.registrarEstacion("Estacion1", "Barrio1", 10);
        s.registrarBicicleta("B00001", "URBANA");
        s.asignarBicicletaAEstacion("B00001", "Estacion1");
        
        // Con un alquiler
        s.alquilarBicicleta("55555555", "Estacion1");
        retorno = s.usuarioMayor();
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        assertEquals("55555555", retorno.getValorString());
    }

    @Test
    public void testVariosUsuariosDiferentesAlquileres() {
        // Registrar usuarios
        s.registrarUsuario("11111111", "Ana");
        s.registrarUsuario("22222222", "Bob");
        s.registrarUsuario("33333333", "Carlos");
        
        // Registrar estación y bicicletas
        s.registrarEstacion("Estacion1", "Barrio1", 10);
        s.registrarBicicleta("B00001", "URBANA");
        s.registrarBicicleta("B00002", "URBANA");
        s.registrarBicicleta("B00003", "URBANA");
        
        // Preparar tres bicicletas disponibles
        s.asignarBicicletaAEstacion("B00001", "Estacion1");
        s.asignarBicicletaAEstacion("B00002", "Estacion1");
        s.asignarBicicletaAEstacion("B00003", "Estacion1");

        // Ana: 1 alquiler
        s.alquilarBicicleta("11111111", "Estacion1");
        s.devolverBicicleta("11111111", "Estacion1");
        
        // Bob: 2 alquileres
        s.alquilarBicicleta("22222222", "Estacion1");
        s.devolverBicicleta("22222222", "Estacion1");
        s.alquilarBicicleta("22222222", "Estacion1");
        s.devolverBicicleta("22222222", "Estacion1");
        
        // Carlos: 3 alquileres
        s.alquilarBicicleta("33333333", "Estacion1");
        s.devolverBicicleta("33333333", "Estacion1");
        s.alquilarBicicleta("33333333", "Estacion1");
        s.devolverBicicleta("33333333", "Estacion1");
        s.alquilarBicicleta("33333333", "Estacion1");
        s.devolverBicicleta("33333333", "Estacion1");
        
        // Carlos debe ser el usuario con más alquileres
        retorno = s.usuarioMayor();
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        assertEquals("33333333", retorno.getValorString());
    }

    @Test
    public void testDesempatesPorCedula() {
        // Registrar usuarios con cédulas en diferente orden
        s.registrarUsuario("55555555", "Ana");    // Mayor cédula
        s.registrarUsuario("44444444", "Bob");    // Cédula del medio
        s.registrarUsuario("33333333", "Carlos"); // Menor cédula
        
        // Registrar estación y bicicletas
        s.registrarEstacion("Estacion1", "Barrio1", 10);
        s.registrarBicicleta("B00001", "URBANA");
        s.registrarBicicleta("B00002", "URBANA");
        s.asignarBicicletaAEstacion("B00001", "Estacion1");
        s.asignarBicicletaAEstacion("B00002", "Estacion1");
        
        // Hacer que todos tengan 2 alquileres
        String[] cedulas = {"33333333", "44444444", "55555555"};
        for (String cedula : cedulas) {
            // Dos alquileres para cada usuario
            s.alquilarBicicleta(cedula, "Estacion1");
            s.devolverBicicleta(cedula, "Estacion1");
            s.alquilarBicicleta(cedula, "Estacion1");
            s.devolverBicicleta(cedula, "Estacion1");
        }
        
        // Al tener todos el mismo número de alquileres,
        // debe retornar el de menor cédula
        retorno = s.usuarioMayor();
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        assertEquals("33333333", retorno.getValorString());
    }
}
