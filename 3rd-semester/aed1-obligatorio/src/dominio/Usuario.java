package dominio;

public class Usuario {
    private final String m_cedula;
    private final String m_nombre;
    private Bicicleta m_bicicletaActual;
    private int m_cantidadAlquileres;
    
    public Usuario(String cedula, String nombre) {
        this.m_nombre = nombre;
        this.m_cedula = cedula;
        this.m_bicicletaActual = null;
        this.m_cantidadAlquileres = 0;
    }
    
    public String getNombre() {
        return this.m_nombre;
    }
    
    public String getCedula() {
        return this.m_cedula;
    }
    
    public Bicicleta getBicicletaActual() {
        return this.m_bicicletaActual;
    }
    
    public void setBicicletaActual(Bicicleta b) {
        this.m_bicicletaActual = b;
    }
    
    public int getCantidadAlquileres() {
        return this.m_cantidadAlquileres;
    }
    
    public void setCantidadAlquileres(int cantidad) {
        this.m_cantidadAlquileres = cantidad;
    }
    
    @Override
    public String toString() {
        return this.m_nombre + "#" + this.m_cedula;
    }
}
