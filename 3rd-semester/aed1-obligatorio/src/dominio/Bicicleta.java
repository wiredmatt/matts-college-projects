package dominio;

public class Bicicleta {
    private final String m_codigo;
    private final String m_tipo;
    private BicicletaEstado m_estado;
    
    public Bicicleta(String codigo, String tipo) {
        this.m_codigo = codigo;
        this.m_tipo = tipo;
        this.m_estado = BicicletaEstado.Disponible;
    }
    
    public String getCodigo() {
        return this.m_codigo;
    }
    
    public String getTipo() {
        return this.m_tipo;
    }
    
    public BicicletaEstado getEstado() {
        return this.m_estado;
    }
    
    public void setEstado(BicicletaEstado estado) {
        this.m_estado = estado;
    }
    
    @Override()
    public String toString() {
        return this.m_codigo + "#" + this.m_tipo + "#" + this.m_estado;
    }
}
