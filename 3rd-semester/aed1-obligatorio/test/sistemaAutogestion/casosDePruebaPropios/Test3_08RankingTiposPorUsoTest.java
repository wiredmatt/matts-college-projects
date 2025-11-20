package sistemaAutogestion.casosDePruebaPropios;

import dominio.Validadores;
import static org.junit.Assert.assertEquals;
import org.junit.Before;
import org.junit.Test;
import sistemaAutogestion.IObligatorio;
import sistemaAutogestion.Retorno;
import sistemaAutogestion.Sistema;

public class Test3_08RankingTiposPorUsoTest {
    private Retorno retorno;
    private final IObligatorio s = new Sistema();

    @Before
    public void setUp() {
        s.crearSistemaDeGestion();
    }

    @Test
    public void rankingTiposPorUso_Ok_SinAlquileres() {
        retorno = s.rankingTiposPorUso();
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        assertEquals("ELECTRICA#0|MOUNTAIN#0|URBANA#0", retorno.getValorString());
    }

    @Test
    public void rankingTiposPorUso_Ok_AlgunasUsadas() {
        String[] tipos = Validadores.BicicletaV.getTiposValidos();
        String[] codigos = new String[tipos.length];
        
        for (int i = 0; i < tipos.length; i++) {
            String codigo = TestUtils.BicicletaTestUtils.generarCodigo("A", i);
            
            codigos[i] = codigo;
            retorno = s.registrarBicicleta(codigo, tipos[i]);
            assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        }

        retorno = s.registrarEstacion("Est1", "Centro", 10);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        
        retorno = s.registrarUsuario("12345678", "Usuario1");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());

        for (String codigo : codigos) {
            retorno = s.asignarBicicletaAEstacion(codigo, "Est1");
            assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        }

        for (int i = 0; i < 3; i++) {
            retorno = s.alquilarBicicleta("12345678", "Est1");
            assertEquals(Retorno.Resultado.OK, retorno.getResultado());
            retorno = s.devolverBicicleta("12345678", "Est1");
            assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        }

        for (int i = 0; i < 2; i++) {
            retorno = s.alquilarBicicleta("12345678", "Est1");
            assertEquals(Retorno.Resultado.OK, retorno.getResultado());
            retorno = s.devolverBicicleta("12345678", "Est1");
            assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        }

        for (int i = 0; i < 2; i++) {
            retorno = s.alquilarBicicleta("12345678", "Est1");
            assertEquals(Retorno.Resultado.OK, retorno.getResultado());
            retorno = s.devolverBicicleta("12345678", "Est1");
            assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        }

        retorno = s.rankingTiposPorUso();
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        assertEquals("URBANA#3|ELECTRICA#2|MOUNTAIN#2", retorno.getValorString());
    }

    @Test
    public void rankingTiposPorUso_Ok_TodasIguales() {
        String[] tipos = Validadores.BicicletaV.getTiposValidos();
        String[] codigos = new String[tipos.length];
        
        for (int i = 0; i < tipos.length; i++) {
            String codigo = TestUtils.BicicletaTestUtils.generarCodigo("A", i);
            
            codigos[i] = codigo;
            retorno = s.registrarBicicleta(codigo, tipos[i]);
            assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        }

        retorno = s.registrarEstacion("Est1", "Centro", 10);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        
        retorno = s.registrarUsuario("12345678", "Usuario1");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());

        for (String codigo : codigos) {
            retorno = s.asignarBicicletaAEstacion(codigo, "Est1");
            assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        }

        for (int i = 0; i < tipos.length; i++) {
            retorno = s.alquilarBicicleta("12345678", "Est1");
            assertEquals(Retorno.Resultado.OK, retorno.getResultado());
            retorno = s.devolverBicicleta("12345678", "Est1");
            assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        }

        retorno = s.rankingTiposPorUso();
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        assertEquals("ELECTRICA#1|MOUNTAIN#1|URBANA#1", retorno.getValorString());
    }

    @Test
    public void rankingTiposPorUso_Ok_DespuesDeDeshacer() {
        retorno = s.registrarBicicleta("A00001", "URBANA");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        
        retorno = s.registrarBicicleta("A00002", "ELECTRICA");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());

        retorno = s.registrarEstacion("Est1", "Centro", 10);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        
        retorno = s.registrarUsuario("12345678", "Usuario1");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());

        retorno = s.asignarBicicletaAEstacion("A00001", "Est1");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        
        retorno = s.asignarBicicletaAEstacion("A00002", "Est1");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());

        retorno = s.alquilarBicicleta("12345678", "Est1");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        retorno = s.devolverBicicleta("12345678", "Est1");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());

        retorno = s.alquilarBicicleta("12345678", "Est1");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        retorno = s.devolverBicicleta("12345678", "Est1");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());

        retorno = s.alquilarBicicleta("12345678", "Est1");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        retorno = s.devolverBicicleta("12345678", "Est1");
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());

        retorno = s.rankingTiposPorUso();
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        assertEquals("URBANA#2|ELECTRICA#1|MOUNTAIN#0", retorno.getValorString());
    }
}
