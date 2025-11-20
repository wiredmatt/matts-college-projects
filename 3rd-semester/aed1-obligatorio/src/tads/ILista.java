package tads;

import java.util.Comparator;

public interface ILista<T> {

    void adicionar(T x);
    
    void adicionarOrdenado(T x, Comparator<T> comparador);

    void insertar(T x, int pos) throws Exception;

    T obtener(int pos) throws Exception;

    void eliminar(int pos) throws Exception;

    int longitud();

    boolean estaVacia();
    
    @Override
    String toString();
}
