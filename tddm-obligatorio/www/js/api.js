class API {
  static #baseURL = "https://babytracker.develotion.com";
  static imgsURL = "https://babytracker.develotion.com/imgs";
  static #apiKey = null;
  static #idUser = null;

  static setBaseUrl(baseUrl) {
    API.#baseURL = baseUrl;
  }

  static setAuth(apiKey, idUser) {
    API.#apiKey = apiKey;
    API.#idUser = idUser;
  }

  static flush() {
    API.#apiKey = null;
    API.#idUser = null;
  }

  static restore() {
    const usuarioGuardado = Store.getUsuarioLogueado();
    if (usuarioGuardado === null) {
      API.#apiKey = null;
      API.#idUser = null;
    } else {
      API.#apiKey = usuarioGuardado.apiKey;
      API.#idUser = usuarioGuardado.idUser;
    }
  }

  // esta funcion se llama luego de haber aplicado las debidas
  // validaciones de los datos previamente. Es para evitar
  // re-usar then y catch con las mismas excepciones constantemente.
  static #fetch(endpoint, method = "GET", data = null) {
    const url = `${API.#baseURL}${endpoint}`;

    const options = {
      method,
      headers: {
        "Content-Type": "application/json",
        apiKey: API.#apiKey,
        idUser: API.#idUser,
      },
    };

    if (data) options.body = JSON.stringify(data);

    return fetch(url, options)
      .then((response) => {
        // NOTE(matt): it'd be great if we could use an event bus
        // that handles receiving this specific error and triggers
        // cerrarSesionPorFaltaDeToken on its own, rather than
        // having to handle it every time manually.
        if (response.status === 401) {
          return {
            error: "UNAUTHORIZED",
          };
        } else {
          return response.json();
        }
      })
      .then((data) => {
        if (data?.error) {
          return {
            error: data.error,
          };
        }

        if (data) {
          return {
            data: data,
          };
        }

        return {
          error: "Por favor, intente nuevamente.",
        };
      })
      .catch((error) => {
        console.log(error);
        return {
          error: "Por favor, intente nuevamente.",
        };
      });
  }

  /**
   * @returns {Promise<Array<{id: number, nombre: string, ciudades: Array<{id: number, nombre: string}>}>>}
   */
  static getDepartamentosYCiudades() {
    return API.#fetch("/departamentos.php", "GET")
      .then((res) => {
        let departamentos = res.data.departamentos;
        let fetchCiudadesPromises = departamentos.map((departamento) => {
          return API.#fetch(
            `/ciudades.php?idDepartamento=${departamento.id}`,
            "GET"
          ).then((res) => {
            departamento.ciudades = res.data.ciudades;
            return departamento;
          });
        });

        return Promise.all(fetchCiudadesPromises);
      })
      .catch(() => {
        return [];
      });
  }

  static registrar(usuario, password, idDepartamento, idCiudad) {
    const registroBody = Usuario.parse({
      usuario,
      password,
      idDepartamento,
      idCiudad,
    });

    return API.#fetch("/usuarios.php", "POST", registroBody);
  }

  static login(usuario, password) {
    const loginBody = Usuario.parse({
      usuario,
      password,
    });

    return API.#fetch("/login.php", "POST", loginBody);
  }

  static getPlazas() {
    return API.#fetch("/plazas.php", "GET");
  }

  static getCategorias() {
    return API.#fetch("/categorias.php", "GET");
  }

  static crearEvento(idCategoria, fecha = "", detalle = "") {
    return API.#fetch("/eventos.php", "POST", {
      idUsuario: API.#idUser,
      idCategoria,
      fecha,
      detalle,
    });
  }

  static borrarEvento(id) {
    return API.#fetch(`/eventos.php?idEvento=${id}`, "DELETE");
  }

  static getEventos() {
    return API.#fetch(`/eventos.php?idUsuario=${API.#idUser}`, "GET");
  }
}
