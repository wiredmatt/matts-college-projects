package sistemaAutogestion.casosDePruebaPropios;

import static org.junit.Assert.*;
import org.junit.Before;
import org.junit.Test;
import sistemaAutogestion.IObligatorio;
import sistemaAutogestion.Retorno;
import sistemaAutogestion.Sistema;

public class Test3_04InformacionMapaTest {
    private Retorno retorno;
    private final IObligatorio s = new Sistema();

    @Before
    public void setUp() {
        s.crearSistemaDeGestion();
    }
    
    @Test
    public void informacionMapa_Ok_MaxColumnas_y_ExisteAscendente() {
        String[][] mapa = new String[][]{
            {""  ,"",""  ,""  ,""  ,""},
            {""  ,"",""  ,"E3",""  ,""},
            {""  ,"",""  ,""  ,""  ,""},
            {"E1","",""  ,""  ,"E5",""},
            {""  ,"",""  ,""  ,""  ,""},
            {""  ,"","E2",""  ,"E6",""},
            {""  ,"",""  ,""  ,"E7",""},
            {""  ,"",""  ,"E4",""  ,""},
        };

        retorno = s.informaciónMapa(mapa);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        assertTrue(retorno.getValorString().startsWith("3#columna"));
        assertTrue(retorno.getValorString().endsWith("|existe"));
    }
    
    @Test
    public void informacionMapa_Ok_MaxAmbas_y_ExisteAscendente() {
        String[][] mapa = new String[][]{
            {""  ,"",""  ,""  ,""  ,""},
            {""  ,"",""  ,"E3",""  ,""},
            {""  ,"",""  ,""  ,""  ,""},
            {"E1","",""  ,""  ,"E5",""},
            {""  ,"",""  ,""  ,""  ,""},
            {""  ,"","E2",""  ,"E6",""},
            {""  ,"",""  ,""  ,"",""},
            {""  ,"",""  ,"E4",""  ,""},
        };

        retorno = s.informaciónMapa(mapa);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        assertTrue(retorno.getValorString().startsWith("2#ambas"));
        assertTrue(retorno.getValorString().endsWith("|existe"));
    }
    
    @Test
    public void informacionMapa_Ok_MaxAmbas_y_NoExisteAscendente() {
        String[][] mapa = new String[][]{
            {""  , ""  , ""  , ""  , ""  , ""},
            {""  , ""  , ""  , "E3", ""  , ""},
            {""  , ""  , ""  , ""  , ""  , ""},
            {"E1", ""  , ""  , ""  , "E5", ""},
            {""  , ""  , ""  , ""  , ""  , ""},
            {""  , ""  , "E2", ""  , "E6", ""},
            {""  , "E7", ""  , ""  , ""  , ""},
            {""  , ""  , ""  , "E4", ""  , ""},
        };

        retorno = s.informaciónMapa(mapa);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        assertTrue(retorno.getValorString().startsWith("2#ambas"));
        assertTrue(retorno.getValorString().endsWith("|no existe"));
    }
    
    @Test
    public void informacionMapa_Ok_MapaVacio() {
        // Caso 1: Mapa null
        retorno = s.informaciónMapa(null);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        assertEquals("0#ambas", retorno.getValorString());
        
        // Caso 2: Mapa vacío
        retorno = s.informaciónMapa(new String[0][0]);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        assertEquals("0#ambas", retorno.getValorString());
        
        // Caso 3: Mapa con filas null
        String[][] mapaNull = new String[3][];
        retorno = s.informaciónMapa(mapaNull);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        assertEquals("0#ambas", retorno.getValorString());
    }
    
    @Test
    public void informacionMapa_Ok_CeldasVacias() {
        String[][] mapa = new String[][]{
            {"", "", "", ""},
            {"", "", "", ""},
            {"", "", "", ""}
        };

        retorno = s.informaciónMapa(mapa);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        assertEquals("0#ambas", retorno.getValorString());
    }
    
    @Test
    public void informacionMapa_Ok_MapaIrregular() {
        String[][] mapa = new String[][]{
            {"E1", "E2"},          // fila 0: longitud 2
            {"E3", "E4", "E5"},     // fila 1: longitud 3
            {"E6", "E7", "E8", "E9"} // fila 2: longitud 4
        };

        retorno = s.informaciónMapa(mapa);
        assertEquals(Retorno.Resultado.OK, retorno.getResultado());
        assertTrue(retorno.getValorString().startsWith("4#fila"));
        assertTrue(retorno.getValorString().endsWith("|no existe"));
    }
}
