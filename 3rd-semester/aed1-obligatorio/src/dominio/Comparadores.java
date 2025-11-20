package dominio;

import java.util.Comparator;

public class Comparadores {
    public static class UsuarioCmp {
        public static Comparator porNombreAscendente = Comparator.comparing(Usuario::getNombre);
    }
    
    public static class EstacionCmp {
        public static Comparator porNombreAscendente = Comparator.comparing(Estacion::getNombre);
    }
}
