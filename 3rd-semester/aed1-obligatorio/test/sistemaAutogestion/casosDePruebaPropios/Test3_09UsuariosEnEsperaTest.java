package sistemaAutogestion.casosDePruebaPropios;

import dominio.Validadores;
import static org.junit.Assert.assertEquals;
import org.junit.Before;
import org.junit.Test;
import sistemaAutogestion.IObligatorio;
import sistemaAutogestion.Retorno;
import sistemaAutogestion.Sistema;

public class Test3_09UsuariosEnEsperaTest {
    private Retorno retorno;
    private final IObligatorio s = new Sistema();

    @Before
    public void setUp() {
        s.crearSistemaDeGestion();
    }

    @Test
    public void usuariosEnEspera_Error1_EstacionVacia() {
        retorno = s.usuariosEnEspera("");
        assertEquals(Retorno.Resultado.ERROR_1, retorno.getResultado());
        
        retorno = s.usuariosEnEspera(null);
        assertEquals(Retorno.Resultado.ERROR_1, retorno.getResultado());
    }

    @Test
    public void usuariosEnEspera_Error2_EstacionInexistente() {
        retorno = s.usuariosEnEspera("EstacionInexistente");
        assertEquals(Retorno.Resultado.ERROR_2, retorno.getResultado());
    }

    @Test
    public void usuariosEnEspera_Ok_SinUsuariosEsperando() {
        // Registrar una estación
        retorno = s.registrarEstacion("Est1", "Centro", 5);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());

        // Verificar cola vacía
        retorno = s.usuariosEnEspera("Est1");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        assertEquals("", retorno.getValorString());
    }

    @Test
    public void usuariosEnEspera_Ok_ConUsuariosEsperando() {
        // Registrar una estación con capacidad 1
        retorno = s.registrarEstacion("Est1", "Centro", 1);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());

        // Registrar una bicicleta y asignarla a la estación
        retorno = s.registrarBicicleta("B00001", "URBANA");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        retorno = s.asignarBicicletaAEstacion("B00001", "Est1");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());

        // Registrar usuarios
        retorno = s.registrarUsuario("11111111", "Usuario1");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        retorno = s.registrarUsuario("22222222", "Usuario2");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        retorno = s.registrarUsuario("33333333", "Usuario3");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());

        // Usuario1 alquila la única bici
        retorno = s.alquilarBicicleta("11111111", "Est1");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());

        // No hay más bicis, estos usuarios deberían quedar en espera
        retorno = s.alquilarBicicleta("22222222", "Est1");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        retorno = s.alquilarBicicleta("33333333", "Est1");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());

        // Verificar cola de espera
        retorno = s.usuariosEnEspera("Est1");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        assertEquals("22222222|33333333", retorno.getValorString());
    }

    @Test
    public void usuariosEnEspera_Ok_DespuesDeDesalojarCola() {
        // Registrar una estación con capacidad 1
        retorno = s.registrarEstacion("Est1", "Centro", 1);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());

        // Registrar una bicicleta y asignarla a la estación
        retorno = s.registrarBicicleta("B00001", "URBANA");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        retorno = s.asignarBicicletaAEstacion("B00001", "Est1");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());

        // Registrar usuarios
        retorno = s.registrarUsuario("11111111", "Usuario1");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        retorno = s.registrarUsuario("22222222", "Usuario2");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());

        // Usuario1 alquila la única bici
        retorno = s.alquilarBicicleta("11111111", "Est1");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());

        // Usuario2 queda en espera
        retorno = s.alquilarBicicleta("22222222", "Est1");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());

        // Verificar que Usuario2 está en la cola
        retorno = s.usuariosEnEspera("Est1");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        assertEquals("22222222", retorno.getValorString());

        // Usuario1 devuelve la bici
        retorno = s.devolverBicicleta("11111111", "Est1");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());

        // La bici debería asignarse automáticamente a Usuario2, dejando la cola vacía
        retorno = s.usuariosEnEspera("Est1");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        assertEquals("", retorno.getValorString());
    }
}
