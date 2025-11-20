package tads;

public class PilaSE<T> {
    private NodoSE<T> m_tope;

    public PilaSE() {
        this.m_tope = null;
    }

    public void apilar(T dato) {
       NodoSE<T> nuevo_nodo = new NodoSE<>(dato);
       nuevo_nodo.setSiguiente(this.m_tope);
       this.m_tope = nuevo_nodo;
    }

    public T desapilar() {
       T dato = null;
       if (!this.estaVacia()){
           dato = this.m_tope.getDato();
           this.m_tope = this.m_tope.getSiguiente();
       }
       return dato;
    }

    public boolean estaVacia() {
        return this.m_tope == null;
    }

    public T getTope() {
       return this.m_tope != null ? this.m_tope.getDato() : null;
       
    }
    
    /*Metodo utilizado para la visualización de la Pila*/
    public ILista<T> obtenerElementos() {
        ILista<T> elementos = new ListaSE<>();
        NodoSE<T> actual = this.m_tope;
        while (actual != null) {
            elementos.adicionar(actual.getDato());
            actual = actual.getSiguiente();
        }
        return elementos;
    }
    
    public int getCantidadElementos(){
        int cont = 0;
        PilaSE<T> aux = new PilaSE<>();
        while (!this.estaVacia()) {
            cont ++;
            aux.apilar(this.desapilar());
        }
        while (!aux.estaVacia()) this.apilar(aux.desapilar());
        
        return cont;  
    }
    
    public PilaSE<T> copiarPila(){
        PilaSE<T> aux = new PilaSE<>();
        PilaSE<T> resultado = new PilaSE<>();
        while (!this.estaVacia()) 
           aux.apilar(this.desapilar());
        while (!aux.estaVacia()){
           T dato = aux.desapilar();
           resultado.apilar(dato);
           this.apilar(dato);
        }
        return resultado;
    }
    
    public void intercambiarTope(){
        T tope = this.desapilar();
        T subTope = this.desapilar();
        this.apilar(tope);
        this.apilar(subTope);
    } 
    
    public void concatenar (PilaSE<T> otraPila){
       PilaSE<T> aux = new PilaSE<>();
       while (!this.estaVacia()) 
         aux.apilar(this.desapilar());
       while (!otraPila.estaVacia()) 
         aux.apilar(otraPila.desapilar());
       while (!aux.estaVacia()) 
         this.apilar(aux.desapilar()); 
    }
    
    public void invertir(){
        ColaSE<T> aux = new ColaSE<>();
        while (!this.estaVacia())
          aux.encolar(this.desapilar());
        while (!aux.estaVacia()) 
          apilar(aux.desencolar());
    }  
}

