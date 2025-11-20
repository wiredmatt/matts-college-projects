package tads;

public class Diccionario<K, V> {

    private static final int CAPACIDAD_DEFAULT = 16;

    private EntradaDiccionario<K, V>[] m_entradas;

    private int m_tamano = 0;

    public Diccionario() {
        this(Diccionario.CAPACIDAD_DEFAULT);
    }

    public Diccionario(int capacidad) {
        this.m_entradas = new EntradaDiccionario[capacidad];
    }

    public void setValor(K clave, V valor) {
        if (clave == null) return;

        int index = hashear(clave);

        EntradaDiccionario<K, V> nuevaEntrada = new EntradaDiccionario<>(clave, valor);

        if (this.m_entradas[index] == null) this.m_entradas[index] = nuevaEntrada;
        else {
            EntradaDiccionario<K, V> actual = this.m_entradas[index];
            while (actual.getSiguiente() != null) {
                if (actual.getClave().equals(clave)) {
                    actual.setValor(valor);
                    return;
                }
                actual = actual.getSiguiente();
            }
            if (actual.getClave().equals(clave)) actual.setValor(valor);
            else actual.setSiguiente(nuevaEntrada);
        }
        this.m_tamano++;
    }

    public V obtener(K clave, V valorDefault) {
        int index = hashear(clave);
        if (this.m_entradas[index] != null) {
            EntradaDiccionario<K, V> actual = this.m_entradas[index];
            while (actual != null) {
                if (actual.getClave().equals(clave)) return actual.getValor();
                actual = actual.getSiguiente();
            }
        }
        return valorDefault;
    }

    public boolean eliminar(K clave) {
        int index = hashear(clave);
        EntradaDiccionario<K, V> anterior = null;
        EntradaDiccionario<K, V> actual = this.m_entradas[index];

        while (actual != null) {
            if (actual.getClave().equals(clave)) {
                if (anterior == null) this.m_entradas[index] = actual.getSiguiente();
                else anterior.setSiguiente(actual.getSiguiente());
                this.m_tamano--;
                return true;
            }
            anterior = actual;
            actual = actual.getSiguiente();
        }
        return false;
    }
    
    public int getTamano() {
        return this.m_tamano;
    }
    
    @Override
    public String toString() {
        String s = "{\n";
        boolean primero = true;
        for (EntradaDiccionario<K, V> actual : m_entradas) {
            while (actual != null) {
                if (!primero) s += ", ";
                
                s += actual.getClave() + "=" + actual.getValor();
                primero = false;
                actual = actual.getSiguiente();
            }
        }
        s += "}";
        return s;
    }
    
    private int hashear(K clave) {
        return Math.abs(clave.hashCode()) % m_entradas.length;
    }
}