package tads;

public class ColaSE<T> {
    private NodoSE<T> m_frente;
    private NodoSE<T> m_fondo;
 
    public ColaSE() {
        this.m_frente = null;
        this.m_fondo = null;
    }

    public void encolar(T dato) {
      NodoSE<T> nuevoNodo = new NodoSE<>(dato);
      if (this.estaVacia()) this.m_frente = nuevoNodo;
      else this.m_fondo.setSiguiente(nuevoNodo);
      this.m_fondo = nuevoNodo;
    }
       
    public T desencolar() {
      T dato = null;
      if (!this.estaVacia()){
          dato = this.m_frente.getDato();
          this.m_frente = this.m_frente.getSiguiente();
          if (this.m_frente == null) this.m_fondo = null;
      }
      return dato;
    }

    public boolean estaVacia() {
        return this.m_frente == null && this.m_fondo == null;
    }

    public T getFrente() {
       return this.m_frente != null ? this.m_frente.getDato() : null;
       
    }
    
    public T getFondo() {
        return this.m_fondo != null ? this.m_fondo.getDato() : null;
    }
    
    public ILista<T> obtenerElementos() {
        ILista<T> elementos = new ListaSE<>();
        NodoSE<T> actual = this.m_frente;
        while (actual != null) {
            elementos.adicionar(actual.getDato());
            actual = actual.getSiguiente();
        }
        return elementos;
    }
    
    public ColaSE<T> copiarCola(){
        ColaSE<T> aux = new ColaSE<>();
        ColaSE<T> resultado = new ColaSE<>();
        while (!this.estaVacia()) 
           aux.encolar(this.desencolar());
        while (!aux.estaVacia()){
           T dato = aux.desencolar();
           resultado.encolar(dato);
           this.encolar(dato);
        }
        return resultado;
    }
}

