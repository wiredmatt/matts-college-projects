package dominio;

import tads.*;

public class Deposito {

    private final ILista<Bicicleta> m_bicicletas;

    public Deposito() {
        this.m_bicicletas = new ListaDE<>();
    }

    public void agregarBicicleta(Bicicleta b) {
        this.m_bicicletas.adicionar(b);
    }
    
    public ILista<Bicicleta> getBicicletas() {
        return this.m_bicicletas;
    }

    public Bicicleta buscarBicicleta(String codigo) {
        for (int i = 0; i < this.m_bicicletas.longitud(); i++) {
            try {
                Bicicleta b = this.m_bicicletas.obtener(i);
                if (codigo.equals(b.getCodigo())) return b;
            } catch (Exception e) {
                return null;
            }
        }

        return null;
    }
}
