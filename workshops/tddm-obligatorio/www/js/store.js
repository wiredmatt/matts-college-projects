class Store {
  static #baseKey = "ObligatorioMC";
  static usuarioLogueado = null; // { "idUser": 1, "apiKey": "asdasd" }
  static departamentosYCiudades = [];
  static plazas = [];
  static categorias = [];

  static #keys = {
    usuarioLogueado: `${Store.#baseKey}_usuario`,
    departamentosYCiudades: `${Store.#baseKey}_departamentosYCiudades`,
    plazas: `${Store.#baseKey}_plazas`,
    categorias: `${Store.#baseKey}_categorias`,
  };

  static restore() {
    this.getDepartamentosYCiudades();
    this.getUsuarioLogueado();
    this.getPlazas();
    this.getCategorias();
  }

  static flush() {
    Store.usuarioLogueado = null;
    Store.plazas = [];
    Store.categorias = [];
    localStorage.removeItem(Store.#keys.usuarioLogueado);
    localStorage.removeItem(Store.#keys.plazas);
    localStorage.removeItem(Store.#keys.categorias);
  }

  static getUsuarioLogueado() {
    Store.usuarioLogueado =
      JSON.parse(localStorage.getItem(this.#keys.usuarioLogueado)) ?? null;
    return Store.usuarioLogueado;
  }

  static setUsuarioLogueado(apiKey, idUser) {
    Store.usuarioLogueado = {
      apiKey,
      idUser,
    };

    localStorage.setItem(
      this.#keys.usuarioLogueado,
      JSON.stringify(Store.usuarioLogueado)
    );
  }

  static getDepartamentosYCiudades() {
    Store.departamentosYCiudades =
      JSON.parse(localStorage.getItem(this.#keys.departamentosYCiudades)) ?? [];
    return Store.departamentosYCiudades;
  }

  static setDepartamentosYCiudades(data) {
    Store.departamentosYCiudades = data;

    localStorage.setItem(
      Store.#keys.departamentosYCiudades,
      JSON.stringify(Store.departamentosYCiudades)
    );
  }

  static getPlazas() {
    Store.plazas = JSON.parse(localStorage.getItem(this.#keys.plazas)) ?? [];
    return Store.plazas;
  }

  static setPlazas(data) {
    Store.plazas = data;
    localStorage.setItem(Store.#keys.plazas, JSON.stringify(Store.plazas));
  }

  static getCategorias() {
    Store.categorias =
      JSON.parse(localStorage.getItem(this.#keys.categorias)) ?? [];
    return Store.categorias;
  }

  static setCategorias(data) {
    Store.categorias = data;
    localStorage.setItem(
      Store.#keys.categorias,
      JSON.stringify(Store.categorias)
    );
  }
}
