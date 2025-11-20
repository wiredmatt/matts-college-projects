package dominio;

public class Validadores {
    
   public static boolean esNulo(Object o) {
       return o == null;
   }
   
   public static boolean esStringVacio(String o) {
       return Validadores.esNulo(o) || o.isEmpty() || o.isBlank();
   }

   public static class UsuarioV {
       public final static int CANTIDAD_DIGITOS = 8;
       
       public static boolean esCedulaValida(String cedula) {
            if (Validadores.esStringVacio(cedula)) return false;
            if (cedula.length() != CANTIDAD_DIGITOS) return false;
            try { Integer.valueOf(cedula); } catch(NumberFormatException e) { return false; }
            return true;
       }
   }
   
   public static class BicicletaV {
       public final static int CODIGO_LONGITUD = 6;
       public final static String[] TIPOS_VALIDOS = new String[]{"URBANA", "MOUNTAIN", "ELECTRICA"};
       
       public static boolean esCodigoValido(String codigo) {
           return codigo.length() == Validadores.BicicletaV.CODIGO_LONGITUD;
       }
       
       public static boolean esTipoValido(String tipo) {
           for (String t : Validadores.BicicletaV.TIPOS_VALIDOS)
               if (t.equals(tipo)) return true;
           
           return false;
        }
       
       public static String[] getTiposValidos() {
           return Validadores.BicicletaV.TIPOS_VALIDOS;
       }
   }
}
