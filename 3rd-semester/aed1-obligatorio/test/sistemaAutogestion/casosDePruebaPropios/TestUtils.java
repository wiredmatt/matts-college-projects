package sistemaAutogestion.casosDePruebaPropios;

import dominio.Validadores;

public class TestUtils {
    public static class UsuarioTestUtils {
        private static final int MIN8DIGITOS = (int) Math.pow(10, Validadores.UsuarioV.CANTIDAD_DIGITOS - 1); // 10_000_000
        private static final int MAX8DIGITOS = (int) Math.pow(10, Validadores.UsuarioV.CANTIDAD_DIGITOS) - 1;  // 99_999_999

        public static String generarCedula(int idx) {
            String cedula = String.valueOf(Math.min(UsuarioTestUtils.MIN8DIGITOS + (idx+1), UsuarioTestUtils.MAX8DIGITOS));
            return cedula;
        }

        public static String[] generarNCedulas(int n, int idx) {
            String[] cedulas = new String[n];

            for (int i = 0; i < n; i++) {
                cedulas[i] = UsuarioTestUtils.generarCedula(i);
            }

            return cedulas;
        }
    }
    
    public static class BicicletaTestUtils {
        public static String generarCodigo(String prefix, int idx) {
            String iStr = String.valueOf(idx+1);
            int caracteresRestantes = Validadores.BicicletaV.CODIGO_LONGITUD - prefix.length() - iStr.length();
            String codigo = prefix + "0".repeat(caracteresRestantes) + iStr;
            return codigo;
        }
        
        public static String[] generarNCodigos(String prefix, int n, int idx) {
            String[] codigos = new String[n];

            for (int i = 0; i < n; i++) {
                codigos[i] = BicicletaTestUtils.generarCodigo(prefix, i);
            }

            return codigos;
        }
    }
}
