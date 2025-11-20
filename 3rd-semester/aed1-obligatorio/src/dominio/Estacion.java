package dominio;

import tads.*;

public class Estacion {
    private final String m_nombre;
    private final String m_barrio;
    private final int m_capacidad;
    private final ILista<Bicicleta> m_bicicletas;         // bicicletas ancladas (orden FIFO de anclaje)
    private final ColaSE<Bicicleta> m_colaEsperaAnclajes; // bicicletas en espera de anclaje cuando estacion llena (FIFO)
    private final ColaSE<Usuario> m_colaEsperaUsuarios;   // usuarios en espera de alquiler (FIFO)

    public Estacion(String nombre, String barrio, int capacidad) {
        this.m_nombre = nombre;
        this.m_barrio = barrio;
        this.m_capacidad = capacidad;
        this.m_bicicletas = new ListaSE<>();
        this.m_colaEsperaAnclajes = new ColaSE<>();
        this.m_colaEsperaUsuarios = new ColaSE<>();
    }

    public String getNombre() { return this.m_nombre; }
    public String getBarrio() { return this.m_barrio; }
    public int getCapacidad() { return this.m_capacidad; }
    public ILista<Bicicleta> getBicicletas() { return this.m_bicicletas; }
    public ColaSE<Bicicleta> getColaEsperaAnclajes() { return this.m_colaEsperaAnclajes; }
    public ColaSE<Usuario> getColaEsperaUsuarios() { return this.m_colaEsperaUsuarios; }

    public boolean hayEspacio() {
        return this.m_bicicletas.longitud() < this.m_capacidad;
    }

    public int cantidadBicicletas() {
        return this.m_bicicletas.longitud();
    }

    public boolean hayUsuariosEnEspera() {
        return !this.m_colaEsperaUsuarios.estaVacia();
    }
    
    public Bicicleta buscarBicicleta(String codigo) {
        // buscar primero entre anclajes
        for (int i = 0; i < this.m_bicicletas.longitud(); i++) {
            try {
                Bicicleta b = this.m_bicicletas.obtener(i);
                if (codigo.equals(b.getCodigo())) return b;
            } catch (Exception e) {
                return null;
            }
        }
        
        // luego buscar en cola de espera
        ILista<Bicicleta> colaEspera = this.m_colaEsperaAnclajes.obtenerElementos();
        for (int i = 0; i < colaEspera.longitud(); i++) {
            try {
                Bicicleta b = colaEspera.obtener(i);
                if (codigo.equals(b.getCodigo())) return b;
            } catch (Exception e) {
                return null;
            }
        }

        return null;
    }

    public void recibirBicicleta(Bicicleta b) {
        // Si hay usuarios esperando, entregar directo
        if (!this.m_colaEsperaUsuarios.estaVacia()) {
            Usuario u = this.m_colaEsperaUsuarios.desencolar();
            u.setBicicletaActual(b);
            b.setEstado(BicicletaEstado.Alquilada);
            return;
        }
        
        b.setEstado(BicicletaEstado.Disponible);

        // Si hay lugar, anclar la bicicleta
        if (this.hayEspacio()) this.m_bicicletas.adicionar(b);
        else this.m_colaEsperaAnclajes.encolar(b); // Estacion llena, dejar en espera.
    }

    public Bicicleta retirarBicicleta() {
        if (m_bicicletas.estaVacia()) return null;

        Bicicleta salida = null;
        try {
            salida = this.m_bicicletas.obtener(0);
            this.m_bicicletas.eliminar(0);
        } catch (Exception e) {}

        if (salida != null) salida.setEstado(BicicletaEstado.Alquilada);

        // anclar bicicletas en espera
        this.procesarColaAnclajes();

        return salida;
    }

    public void encolarUsuarioEspera(Usuario u) {
        this.m_colaEsperaUsuarios.encolar(u);
    }

    private void procesarColaAnclajes() {
        while (this.hayEspacio() && !this.m_colaEsperaAnclajes.estaVacia()) {
            Bicicleta b = this.m_colaEsperaAnclajes.desencolar();
            if (b == null) break;
            this.m_bicicletas.adicionar(b);
            b.setEstado(BicicletaEstado.Disponible);
        }
    }

    public Bicicleta solicitarAlquiler(Usuario u) {
        // Si hay bicicleta anclada, entrego la primera
        Bicicleta b = this.retirarBicicleta();
        if (b != null) {
            u.setBicicletaActual(b);
            b.setEstado(BicicletaEstado.Alquilada);
            return b;
        }

        // No hay bicicletas, encolar usuario en espera
        this.m_colaEsperaUsuarios.encolar(u);
        return null;
    }

    @Override
    public String toString() {
        return this.m_nombre + "#" + this.m_barrio + "#" + this.m_capacidad;
    }
}
