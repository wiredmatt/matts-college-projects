class Usuario {
  constructor() {
    this.usuario = null;
    this.password = null;
    this.idDepartamento = null;
    this.idCiudad = null;
  }

  static parse(data) {
    let instancia = new Usuario();

    if (data.usuario) {
      instancia.usuario = data.usuario;
    }
    if (data.password) {
      instancia.password = data.password;
    }
    if (data.idDepartamento) {
      instancia.idDepartamento = data.idDepartamento;
    }
    if (data.idCiudad) {
      instancia.idCiudad = data.idCiudad;
    }

    return instancia;
  }
}
