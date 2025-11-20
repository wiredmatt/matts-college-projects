package tads;

import java.util.Comparator;

public class ListaDE<T> implements ILista<T> {

    protected NodoDE<T> m_cabeza;
    protected int m_longitud;

    public ListaDE() {
        this.m_cabeza = null;
        this.m_longitud = 0;
    }

    @Override
    public void adicionar(T x) {
        NodoDE<T> nodo = new NodoDE<>(x);
        if (this.estaVacia()) {
            this.m_cabeza = nodo;
        }
        else {
            NodoDE<T> cursor = this.m_cabeza;
            while (cursor.getSiguiente() != null) {
                cursor = cursor.getSiguiente();
            }
            
            nodo.setAnterior(cursor);
            cursor.setSiguiente(nodo);
        }
        this.m_longitud++;
    }

    // `AdicionarOrdenado` insertara el nodo basandose en el criterio
    // establecido por `comparador`.
    @Override
    public void adicionarOrdenado(T x, Comparator<T> comparador) {
        NodoDE<T> nuevoNodo = new NodoDE<>(x);

        // Caso lista vacia
        if (this.estaVacia()) {
            this.m_cabeza = nuevoNodo;
            this.m_longitud++;
            return;
        }

        NodoDE<T> actual = this.m_cabeza;
        NodoDE<T> anterior = null;

        // Buscar la posicion segun el comparador
        while (actual != null && comparador.compare(actual.getDato(), x) < 0) {
            anterior = actual;
            actual = actual.getSiguiente();
        }

        // Insertar al inicio
        if (anterior == null) {
            nuevoNodo.setSiguiente(this.m_cabeza);
            this.m_cabeza.setAnterior(nuevoNodo);
            this.m_cabeza = nuevoNodo;
        }
        // Insertar entre anterior y actual o al final
        else {
            nuevoNodo.setSiguiente(actual);
            nuevoNodo.setAnterior(anterior);
            anterior.setSiguiente(nuevoNodo);
            if (actual != null) actual.setAnterior(nuevoNodo);
        }

        this.m_longitud++;
    }

    @Override
    public void insertar(T x, int pos) {
        if ((pos < 0) || (pos >= m_longitud)) throw new PosFueraDeRangoException();

        NodoDE<T> nodo = new NodoDE<>(x);
        if (pos == 0) {
            nodo.setSiguiente(this.m_cabeza);
            if (this.m_cabeza != null) this.m_cabeza.setAnterior(nodo);
            this.m_cabeza = nodo;
        } else {
            NodoDE<T> cursor = this.m_cabeza;
            int i = 0;
            while (i < pos - 1) {
                i++;
                cursor = cursor.getSiguiente();
            }
            nodo.setSiguiente(cursor.getSiguiente());
            nodo.setAnterior(cursor);
            cursor.getSiguiente().setAnterior(nodo);
            cursor.setSiguiente(nodo);
        }
        this.m_longitud++;
    }

    @Override
    public T obtener(int pos) throws PosFueraDeRangoException {
        if ((pos < 0) || (pos >= this.m_longitud)) throw new PosFueraDeRangoException();

        NodoDE<T> cursor = this.m_cabeza;
        for (int i = 0; i < pos; i++) {
            cursor = cursor.getSiguiente();
        }
        
        return cursor.getDato();
    }

    @Override
    public void eliminar(int pos)
        throws PosFueraDeRangoException, ListaVaciaException {
        if (this.estaVacia()) throw new ListaVaciaException();
        if ((pos < 0) || (pos >= this.m_longitud)) throw new PosFueraDeRangoException();

        NodoDE<T> cursor = this.m_cabeza;
        if (pos == 0) {
            if (this.m_cabeza.getSiguiente() != null) this.m_cabeza.getSiguiente().setAnterior(null);
            
            this.m_cabeza = cursor.getSiguiente();
        } else {
            int i = 0;
            while (i < pos - 1) {
                i++;
                cursor = cursor.getSiguiente();
            }
            cursor.setSiguiente(cursor.getSiguiente().getSiguiente());
            if (cursor.getSiguiente() != null) cursor.getSiguiente().setAnterior(cursor);
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
        NodoDE<T> actual = this.m_cabeza;
        while (actual != null) {
            fmt += actual.getDato().toString();
            actual = actual.getSiguiente();
            if (actual != null) fmt += "|";
        }
        return fmt;
    }
}
