package tads;

import java.util.Comparator;

public class ListaSE<T> implements ILista<T> {
    protected NodoSE<T> m_cabeza;
    protected int m_longitud;

    public ListaSE() {
        this.m_cabeza = null;
        this.m_longitud = 0;
    }
    
    @Override
    public void adicionar(T x) {
        NodoSE<T> nodoNuevo = new NodoSE<>(x);
        if (this.m_cabeza == null) {
            this.m_cabeza = nodoNuevo;
        }
        else {
            NodoSE<T> actual = this.m_cabeza; 
            while (actual.getSiguiente()!= null) {
                actual = actual.getSiguiente();
            }
            actual.setSiguiente(nodoNuevo);
        }
        this.m_longitud++;    
    }

    // `adicionarOrdenado` insertara el nodo basandose en el criterio
    // establecido por `comparador`. 
    @Override
    public void adicionarOrdenado(T x, Comparator<T> comparador) {
        NodoSE<T> nuevo = new NodoSE<>(x);

        // Caso 1: lista vacía o el nuevo elemento debe ir al inicio
        if (this.m_cabeza == null || comparador.compare(x, this.m_cabeza.getDato()) < 0) {
            nuevo.setSiguiente(this.m_cabeza);
            this.m_cabeza = nuevo;
        } else {
            // Caso 2: buscar la posición correcta en la lista
            NodoSE<T> actual = this.m_cabeza;
            while (actual.getSiguiente() != null && comparador.compare(x, actual.getSiguiente().getDato()) > 0) {
                actual = actual.getSiguiente();
            }

            // Insertar entre `actual` y `siguiente`
            nuevo.setSiguiente(actual.getSiguiente());
            actual.setSiguiente(nuevo);
        }
        this.m_longitud++;
    }

    @Override
    public void insertar(T x, int pos) throws PosFueraDeRangoException {
        if (pos < 0 || pos >= this.m_longitud) throw new PosFueraDeRangoException();
        NodoSE<T> nuevo;
        if (pos == 0) {
            nuevo = new NodoSE<>(x, this.m_cabeza);
            this.m_cabeza = nuevo;
        } else {
            NodoSE<T> actual = this.m_cabeza;
            nuevo = new NodoSE<>(x);
            for (int i = 0; i < pos - 1; i++) actual = actual.getSiguiente();
            nuevo.setSiguiente(actual.getSiguiente());
            actual.setSiguiente(nuevo);
        }
        this.m_longitud++;
    }

    @Override
    public T obtener(int pos) {
        if (this.estaVacia()) throw new ListaVaciaException();
        NodoSE<T> actual = this.m_cabeza;
        if (pos < this.m_longitud && pos >= 0) {
            for (int i = 0; i < pos; i++) {
                actual = actual.getSiguiente();
            }
            return actual.getDato();
        } else throw new PosFueraDeRangoException();
    }

    @Override
    public void eliminar(int pos) throws PosFueraDeRangoException, ListaVaciaException {
        if (pos < 0 || pos >= this.m_longitud) throw new PosFueraDeRangoException();

        if (pos == 0) this.m_cabeza = this.m_cabeza.getSiguiente();
        else {
            NodoSE<T> actual = this.m_cabeza;
            for (int i = 0; i < pos - 1; i++) {
                actual = actual.getSiguiente();
            }
            actual.setSiguiente(actual.getSiguiente().getSiguiente());
        }
        this.m_longitud--;
    }

    @Override
    public int longitud() {
        return this.m_longitud;
    }

    @Override
    public boolean estaVacia() {
        return (this.m_longitud == 0);
    }

    @Override
    public String toString() {
        String fmt = "";
        NodoSE<T> actual = this.m_cabeza;
        while (actual != null) {
            fmt += actual.toString();
            actual = actual.getSiguiente();
            if (actual != null) fmt += "|";
        }
        
        return fmt;
    }
}
