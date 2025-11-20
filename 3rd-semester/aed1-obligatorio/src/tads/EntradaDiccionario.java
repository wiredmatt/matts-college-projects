package tads;

public class EntradaDiccionario<K, V> {
    public final K m_clave;
    private V m_valor;
    private EntradaDiccionario<K, V> m_siguiente;

    public EntradaDiccionario(K clave, V valor) {
        this.m_clave = clave;
        this.m_valor = valor;
        this.m_siguiente = null;
    }
    
    public K getClave() {
        return this.m_clave;
    }

    public V getValor() {
        return this.m_valor;
    }

    public void setValor(V valor) {
        this.m_valor = valor;
    }
    
    public EntradaDiccionario<K, V> getSiguiente() {
        return this.m_siguiente;
    }
    
    public void setSiguiente(EntradaDiccionario<K, V> siguiente) {
        this.m_siguiente = siguiente;
    }
 }