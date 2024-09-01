// #region variables globales
/**
 * @type Array<{id: number, nombre: string, ciudades: Array<{id: number, nombre: string}>}>
 */
let departamentosYCiudades = []; // [ {"id": 1, nombre: "Flores", ciudades: [] } ]
let categorias = [];

let map = null;
let plazas = [];
let markerUsuario = null;
let markerPlazas = [];

let posicionUsuario = {
  latitude: -34.903816878014354,
  longitude: -56.19059048108193,
};
const posicionUsuarioIcon = L.icon({
  iconUrl: "img/usuario.png",
  iconSize: [48, 48],
});
const posicionPlazaIcon = L.icon({
  iconUrl: "img/plaza.png",
  iconSize: [48, 48],
});

// #endregion

// #region utilidades y constantes
function qSlc(selector) {
  return document.querySelector(selector);
}

const MENU = qSlc("#menu");
const ROUTER = qSlc("#ruteo");
const NAV = qSlc("#nav");

const PANTALLA_HOME = qSlc("#pantalla-home");
const PANTALLA_LOGIN = qSlc("#pantalla-login");
const PANTALLA_REGISTRO = qSlc("#pantalla-registro");

const PANTALLA_PLAZAS = qSlc("#pantalla-plazas");
const PANTALLA_EVENTOS = qSlc("#pantalla-eventos");
const PANTALLA_INFORMES = qSlc("#pantalla-informes");

const PAGE_LOGIN = "page-login";
const PAGE_REGISTER = "page-register";
const PAGE_HOME = "page-home";
const PAGE_PLAZAS = "page-plazas";
const PAGE_EVENTOS = "page-eventos";
const PAGE_INFORMES = "page-informes";

const COMBO_FILTRO_DEPARTAMENTOS = qSlc(
  "#pantalla-registro-combo-departamentos"
);
const COMBO_FILTRO_CIUDADES = qSlc("#pantalla-registro-combo-ciudades");

const COMBO_CATEGORIAS = qSlc("#pantalla-eventos-combo-categorias");

const MENU_BUTTONS = {
  btnLogin: qSlc("#btnMenuLogin"),
  btnRegistro: qSlc("#btnMenuRegistro"),
  btnCerrarSesion: qSlc("#btnMenuCerrarSesion"),

  btnPlazas: qSlc("#btnMenuPlazas"),
  btnEventos: qSlc("#btnMenuEventos"),
  btnInformes: qSlc("#btnMenuInformes"),
};

const MODAL_CREAR_EVENTO = qSlc("#modal-crear-evento");

async function mostrarToast(tipo, titulo, mensaje) {
  const toast = document.createElement("ion-toast");
  toast.header = titulo;
  toast.message = mensaje;
  toast.position = "bottom";
  toast.duration = 2000;
  if (tipo === "ERROR") {
    toast.color = "danger";
  } else if (tipo === "SUCCESS") {
    toast.color = "success";
  } else if (tipo === "WARNING") {
    toast.color = "warning";
  }

  document.body.appendChild(toast);
  return toast.present();
}

/**
 * @param {Object} _campos
 */
function limpiarCampos(_campos) {
  const campos = Object.values(_campos);

  for (let i = 0; i < campos.length; i++) {
    let campo = campos[i];
    qSlc(campo).value = "";
  }
}

function cerrarMenu() {
  MENU.close();
}

// #endregion

// #region Authorization
function cerrarSesion() {
  cerrarMenu();
  Store.flush();
  API.flush();
  NAV.setRoot(PAGE_LOGIN);
  mostrarPantalla(PANTALLA_LOGIN);
  NAV.popToRoot();
}

function cerrarSesionPorFaltaDeToken() {
  mostrarToast("ERROR", "No autorizado", "Se ha cerrado sesión por seguridad");
  cerrarSesion();
}

function verificarLogueadoYNavegar(page, pantalla, pop = false) {
  if (Store.usuarioLogueado) {
    NAV.setRoot(page);
    if (pop) NAV.popToRoot();
    mostrarPantalla(pantalla);
    return true;
  } else {
    NAV.setRoot(PAGE_LOGIN);
    return false;
  }
}

function actualizarUsuarioLogueado() {
  Store.getUsuarioLogueado();
}

// #endregion

// #region ruteo y navegacion

function ocultarOpcionesMenu() {
  MENU_BUTTONS.btnLogin.style.display = "none";
  MENU_BUTTONS.btnRegistro.style.display = "none";
  MENU_BUTTONS.btnCerrarSesion.style.display = "none";
  MENU_BUTTONS.btnPlazas.style.display = "none";
  MENU_BUTTONS.btnEventos.style.display = "none";
  MENU_BUTTONS.btnInformes.style.display = "none";
}
function actualizarMenu() {
  ocultarOpcionesMenu();
  if (Store.usuarioLogueado) {
    MENU_BUTTONS.btnPlazas.style.display = "block";
    MENU_BUTTONS.btnEventos.style.display = "block";
    MENU_BUTTONS.btnInformes.style.display = "block";
    MENU_BUTTONS.btnCerrarSesion.style.display = "block";
  } else {
    MENU_BUTTONS.btnLogin.style.display = "block";
    MENU_BUTTONS.btnRegistro.style.display = "block";
  }
}

function ocultarPantallas() {
  ocultarPantalla(PANTALLA_HOME);
  ocultarPantalla(PANTALLA_LOGIN);
  ocultarPantalla(PANTALLA_REGISTRO);
  ocultarPantalla(PANTALLA_PLAZAS);
  ocultarPantalla(PANTALLA_EVENTOS);
  ocultarPantalla(PANTALLA_INFORMES);
}

function ocultarPantalla(pantalla) {
  pantalla.style.display = "none";
}

function mostrarPantalla(pantalla) {
  pantalla.style.display = "block";
}

function navegar(evt) {
  actualizarUsuarioLogueado();
  actualizarMenu();
  ocultarPantallas();
  const pantallaDestino = evt.detail.to;
  switch (pantallaDestino) {
    // /home y /plazas son la misma ruta.
    case "/plazas":
      if (verificarLogueadoYNavegar(PAGE_PLAZAS, PANTALLA_PLAZAS)) {
        inicializarMapa();
        cargarPlazas();
      }
      break;
    case "/eventos":
      if (verificarLogueadoYNavegar(PAGE_EVENTOS, PANTALLA_EVENTOS)) {
        cargarCategorias();
        cargarEventos();
      }
      break;
    case "/":

    case "/informes":
      if (verificarLogueadoYNavegar(PAGE_INFORMES, PANTALLA_INFORMES)) {
        cargarCategorias();
        const tabPrevia = qSlc("#segment-informes")?.value;
        cargarInformes(tabPrevia ?? "Biberón");
      }
      break;
    case "/login":
      mostrarPantalla(PANTALLA_LOGIN);
      break;
    case "/registro":
      mostrarPantalla(PANTALLA_REGISTRO);
      break;
  }
}
// #endregion

// #region eventos
function suscribirmeAEventos() {
  // Login
  qSlc("#btnLoginIngresar").addEventListener("click", btnLoginIngresarHandler);
  // Registro
  qSlc("#btnRegistroRegistrarse").addEventListener(
    "click",
    btnRegistroRegistrarseHandler
  );
  COMBO_FILTRO_DEPARTAMENTOS.addEventListener(
    "ionChange",
    comboDepartamentosChangeHandler
  );

  // Ruteo
  ROUTER.addEventListener("ionRouteDidChange", navegar);
}
// #endregion

// #region Handlers de autenticacion - Registro & Login

function btnRegistroRegistrarseHandler() {
  const campos = {
    usuario: "#txtRegistroUsuario",
    password: "#txtRegistroPassword",
    passwordVerificacion: "#txtRegistroVerificacionPassword",
    departamento: "#pantalla-registro-combo-departamentos",
    ciudad: "#pantalla-registro-combo-ciudades",
  };

  const usuarioIngresado = qSlc(campos.usuario).value.trim();
  const passwordIngresado = qSlc(campos.password).value.trim();
  const verificacionPasswordIngresada = qSlc(
    campos.passwordVerificacion
  ).value.trim();
  const idDepartamento = Number(qSlc(campos.departamento).value);
  const idCiudad = Number(qSlc(campos.ciudad).value);

  const formCompleto =
    usuarioIngresado &&
    passwordIngresado &&
    verificacionPasswordIngresada &&
    idDepartamento &&
    idCiudad;
  if (!formCompleto) {
    mostrarToast("ERROR", "Error", "Todos los campos son obligatorios.");
    return;
  }

  const passwordsCoinciden =
    passwordIngresado === verificacionPasswordIngresada;
  if (!passwordsCoinciden) {
    mostrarToast(
      "ERROR",
      "Error",
      "El password y la verificación no coinciden."
    );
    return;
  }

  const departamentoExiste = departamentosYCiudades.find(
    (dep) => dep.id === idDepartamento
  );
  if (!departamentoExiste) {
    mostrarToast("ERROR", "Error", "El departamento no existe.");
    return;
  }
  const ciudadExiste = departamentoExiste.ciudades.find(
    (ciudad) => ciudad.id === idCiudad
  );
  if (!ciudadExiste) {
    mostrarToast("ERROR", "Error", "La ciudad no existe.");
    return;
  }

  API.registrar(
    usuarioIngresado,
    passwordIngresado,
    idDepartamento,
    idCiudad
  ).then((resultado) => {
    if (resultado.error) return mostrarToast("ERROR", "Error", resultado.error);
    if (resultado.data) {
      limpiarCampos(campos);
      mostrarToast("SUCCESS", "Registro exitoso", "Ya puede usar BabyTracker.");

      // NOTE(matt): los endpoints de login / registro no retornan
      // los datos del usuario, de hacerlo, se podrian almacenar
      // en esta clase que se va a encargar de persistir sus datos.
      Store.setUsuarioLogueado(resultado.data.apiKey, resultado.data.id);
      API.setAuth(resultado.data.apiKey, resultado.data.id);
      return verificarLogueadoYNavegar(PAGE_EVENTOS, PANTALLA_EVENTOS, true);
    }

    // Las funciones de la clase API siempre van a resolver con
    // `error` o `data`. El codigo nunca deberia llegar a este punto.
    // Adicionalmente, la clase API *nunca* va a tirar un error, por
    // lo que no es necesario emplear `.catch` en este punto.
    return;
  });
}

function btnLoginIngresarHandler() {
  const campos = {
    usuario: "#txtLoginUsuario",
    password: "#txtLoginPassword",
  };

  const usuarioIngresado = qSlc(campos.usuario).value;
  const passwordIngresado = qSlc(campos.password).value;

  const formCompleto = usuarioIngresado && passwordIngresado;

  if (!formCompleto) {
    mostrarToast("ERROR", "Error", "Todos los campos son obligatorios.");
    return;
  }

  API.login(usuarioIngresado, passwordIngresado).then((resultado) => {
    if (resultado.error) {
      if (resultado.error === "UNAUTHORIZED")
        return cerrarSesionPorFaltaDeToken();
      return mostrarToast("ERROR", "Error", resultado.error);
    }

    if (resultado.data) {
      limpiarCampos(campos);
      mostrarToast("SUCCESS", "Ingreso exitoso", "Se ha iniciado sesión.");

      Store.setUsuarioLogueado(resultado.data.apiKey, resultado.data.id);
      API.setAuth(resultado.data.apiKey, resultado.data.id);
      return verificarLogueadoYNavegar(PAGE_EVENTOS, PANTALLA_EVENTOS, true);
    }
  });
}

// #region Handlers - Eventos

function cargarComboCategorias() {
  COMBO_CATEGORIAS.querySelectorAll("ion-select-option").forEach((elm) =>
    elm.remove()
  );

  for (let i = 0; i < Store.categorias.length; i++) {
    const categoria = Store.categorias[i];
    COMBO_CATEGORIAS.innerHTML += `<ion-select-option value="${categoria.id}">
      ${categoria.tipo}
    </ion-select-option>`;
  }
}

function cargarCategorias() {
  categorias = Store.getCategorias() ?? [];

  if (!categorias.length) {
    return API.getCategorias().then((resultado) => {
      if (resultado.error) {
        if (resultado.error === "UNAUTHORIZED")
          return cerrarSesionPorFaltaDeToken();
        return mostrarToast("ERROR", "Error", resultado.error);
      }

      if (resultado.data) {
        categorias = resultado.data.categorias.map((c) => ({
          ...c,
          imagen: `${API.imgsURL}/${c.imagen}.png`, // por que la api no retorna la url entera?
        }));
        Store.setCategorias(categorias);

        return cargarComboCategorias();
      }
    });
  }

  return cargarComboCategorias();
}

function cancelCrearEvento() {
  MODAL_CREAR_EVENTO.dismiss(null, "cancel");
}
function confirmarCrearEvento() {
  const campos = {
    idCategoria: "#pantalla-eventos-combo-categorias",
    fecha: "#pantalla-eventos-fecha",
    detalle: "#txtCrearEventoDetalle",
  };

  const idCategoria = Number(qSlc(campos.idCategoria).value);
  const fecha = qSlc(campos.fecha).value;
  const detalle = qSlc(campos.detalle).value;

  if (!idCategoria)
    return mostrarToast("ERROR", "Error", "Debe seleccionar una categoria");

  if (!getCategoriaPorId(idCategoria))
    return mostrarToast("ERROR", "Error", "La categoria no es valida.");

  if (new Date(qSlc(campos.fecha).value) > new Date())
    return mostrarToast("ERROR", "Error", "La fecha no es valida.");

  return API.crearEvento(idCategoria, fecha, detalle).then((resultado) => {
    if (resultado.error) {
      if (resultado.error === "UNAUTHORIZED")
        return cerrarSesionPorFaltaDeToken();
      return mostrarToast("ERROR", "Error", resultado.error);
    }
    if (resultado.data) {
      limpiarCampos(campos);
      MODAL_CREAR_EVENTO.dismiss(null, "confirm");
      mostrarToast("SUCCESS", "Evento Creado", "");
      cargarEventos(); // recargar los eventos luego de crear este.
      return;
    }
  });
}

function getCategoriaPorId(id) {
  return categorias.find((c) => c.id === id);
}

function cargarEventos() {
  return API.getEventos().then((resultado) => {
    if (resultado.error) {
      if (resultado.error === "UNAUTHORIZED")
        return cerrarSesionPorFaltaDeToken();
      return mostrarToast("ERROR", "Error", resultado.error);
    }

    if (resultado?.data?.eventos) {
      const hoy = new Date();
      const eventos = resultado.data.eventos;
      const eventosDeHoy = [];
      const eventosAnteriores = [];

      let eventosDeHoyMostrar = [];
      let eventosAnterioresMostrar = [];

      for (const evento of eventos) {
        const fechaEvento = new Date(evento.fecha);
        const categoria = getCategoriaPorId(evento.idCategoria);

        const elementoEvento = `
<ion-list>
  <ion-item>
    <ion-thumbnail>
      <img alt="${categoria.tipo}" src="${categoria.imagen}" />
    </ion-thumbnail>
    <ion-label>${categoria.tipo}</ion-label>
    <ion-label>${fechaEvento.toLocaleString()}</ion-label>
    <ion-icon name="trash-outline" style="cursor: pointer;" onclick="borrarEvento(${
      evento.id
    })"></ion-icon>
  </ion-item>
  <ion-item>
    > ${evento.detalle}
  </ion-item>
</ion-list>
        `;

        if (
          fechaEvento.getFullYear() === hoy.getFullYear() &&
          fechaEvento.getMonth() === hoy.getMonth() &&
          fechaEvento.getDate() === hoy.getDate()
        ) {
          eventosDeHoy.push(evento);
          eventosDeHoyMostrar.push(elementoEvento);
        } else {
          eventosAnteriores.push(evento);
          eventosAnterioresMostrar.push(elementoEvento);
        }
      }

      qSlc("#pantalla-eventos-accordion-hoy").innerHTML = `
<ion-list lines="inset">
      ${eventosDeHoyMostrar.join("\n")}
</ion-list>
      `;

      qSlc("#pantalla-eventos-accordion-anteriores").innerHTML = `
<ion-list lines="inset">
      ${eventosAnterioresMostrar.join("\n")}
</ion-list>
      `;

      return {
        eventos,
        eventosDeHoy,
        eventosAnteriores,
      };
    }
  });
}

function borrarEvento(id) {
  return API.borrarEvento(id).then((resultado) => {
    if (resultado.error) {
      if (resultado.error === "UNAUTHORIZED")
        return cerrarSesionPorFaltaDeToken();
      return mostrarToast("ERROR", "Error", resultado.error);
    }

    if (resultado.data) {
      mostrarToast("SUCCESS", "Evento Borrado", "");
      return cargarEventos();
    }
  });
}

function cargarInformes(tipo = "Biberón") {
  cargarEventos().then(({ eventos, eventosDeHoy }) => {
    let consumidosHoy = 0;
    let horasDesdeUltimoConsumo;
    let eventoMasReciente = null;

    for (const evento of eventosDeHoy) {
      const categoria = getCategoriaPorId(Number(evento.idCategoria));
      if (categoria.tipo === tipo) consumidosHoy++;
    }

    for (const evento of eventos) {
      const categoria = getCategoriaPorId(Number(evento.idCategoria));
      if (categoria.tipo === tipo) {
        const fEvtActual = new Date(evento.fecha);

        if (fEvtActual > new Date(eventoMasReciente?.fecha ?? null)) {
          eventoMasReciente = evento;
        }
      }
    }

    if (!eventoMasReciente?.fecha) {
      horasDesdeUltimoConsumo = "N/A";
    } else {
      const horasNum =
        Math.abs(
          new Date().getTime() - new Date(eventoMasReciente.fecha).getTime()
        ) /
        1000 /
        60 /
        60;

      horasDesdeUltimoConsumo = `${horasNum.toFixed(2)}h`;
    }

    const singular = tipo === "Biberón" ? "biberón" : "pañal";
    const plural = tipo === "Biberón" ? "biberones" : "pañales";
    const accion = tipo === "Biberón" ? "ingeridos" : "cambiados";

    qSlc("#contenido-informes").innerHTML = `
    <ion-item>Total de ${plural} ${accion} en el día: ${consumidosHoy}</ion-item>
    
    <ion-item>Tiempo transcurrido desde el último ${singular}: ${horasDesdeUltimoConsumo}</ion-item>
          `;
  });
}

// #endregion

// #region mapita
function actualizarMarkerUsuario() {
  if (!map) return;

  if (markerUsuario) markerUsuario.remove();
  markerUsuario = L.marker(
    [posicionUsuario.latitude, posicionUsuario.longitude],
    { icon: posicionUsuarioIcon }
  ).addTo(map);

  map.setView([posicionUsuario.latitude, posicionUsuario.longitude], 18);
}

function inicializarMapa() {
  if (!map) {
    map = L.map("mapa-plazas").setView(
      [posicionUsuario.latitude, posicionUsuario.longitude],
      18
    );
    L.tileLayer("https://tile.openstreetmap.org/{z}/{x}/{y}.png").addTo(map);
    actualizarMarkerUsuario();

    // some weird bug with leaflet, without this
    // the map will not display correctly
    const _t = setTimeout(function () {
      window.dispatchEvent(new Event("resize"));

      clearTimeout(_t);
    }, 100);
  }
}

function cargarPosicionUsuario() {
  if (Capacitor.isNativePlatform()) {
    // Cargo la posición del usuario desde el dispositivo.
    const loadCurrentPosition = async () => {
      const resultado = await Capacitor.Plugins.Geolocation.getCurrentPosition({
        timeout: 3000,
      });
      if (resultado.coords && resultado.coords.latitude) {
        posicionUsuario = {
          latitude: resultado.coords.latitude,
          longitude: resultado.coords.longitude,
        };
      }

      actualizarMarkerUsuario();
    };
    loadCurrentPosition();
  } else {
    // Cargo la posición del usuario desde el navegador web.
    window.navigator.geolocation.getCurrentPosition(
      // Callback de éxito.
      function (pos) {
        if (pos && pos.coords && pos.coords.latitude) {
          posicionUsuario = {
            latitude: pos.coords.latitude,
            longitude: pos.coords.longitude,
          };

          actualizarMarkerUsuario();
        }
      },
      // Callback de error.
      function () {
        // No necesito hacer nada, ya asumí que el usuario estaba en ORT.
      }
    );
  }
}

function actualizarMarkersPlazas() {
  if (markerPlazas.length) return;
  if (!map) return;

  for (const plaza of plazas) {
    const distancia = (
      map.distance(
        [posicionUsuario.latitude, posicionUsuario.longitude],
        [plaza.latitud, plaza.longitud]
      ) / 1000
    ).toFixed(2);

    const m = L.marker([plaza.latitud, plaza.longitud], {
      icon: posicionPlazaIcon,
    }).addTo(map);

    m.bindPopup(
      `Distancia: ${distancia}km.<br />
Acepta mascotas: ${plaza.aceptaMascotas === 1 ? "✅" : "❌"}<br />
Es accesible: ${plaza.accesible === 1 ? "✅" : "❌"}`
    );

    markerPlazas.push(m);
  }
}

function cargarPlazas() {
  plazas = Store.getPlazas() ?? [];
  if (!plazas.length) {
    return API.getPlazas()
      .then((resultado) => {
        if (resultado.error) {
          if (resultado.error === "UNAUTHORIZED")
            return cerrarSesionPorFaltaDeToken();
          return mostrarToast("ERROR", "Error", resultado.error);
        }
        if (resultado.data) {
          plazas = resultado.data.plazas;
          Store.setPlazas(plazas);
        }
      })
      .finally(() => {
        actualizarMarkersPlazas();
      });
  }

  actualizarMarkersPlazas();
}

// #endregion

// #region inicializar
function cargarComboDepartamentos() {
  COMBO_FILTRO_DEPARTAMENTOS.querySelectorAll("ion-select-option").forEach(
    (elm) => elm.remove()
  );

  for (let i = 0; i < Store.departamentosYCiudades.length; i++) {
    const dep = Store.departamentosYCiudades[i];
    COMBO_FILTRO_DEPARTAMENTOS.innerHTML += `<ion-select-option value="${dep.id}">${dep.nombre}</ion-select-option>`;
  }
}

function comboDepartamentosChangeHandler(evt) {
  const idDepartamento = Number(evt.detail.value);

  let dep = departamentosYCiudades.find((dep) => dep.id === idDepartamento);

  if (!dep) return;

  COMBO_FILTRO_CIUDADES.querySelectorAll("ion-select-option").forEach((elm) =>
    elm.remove()
  );

  for (let i = 0; i < dep.ciudades.length; i++) {
    const ciudad = dep.ciudades[i];
    COMBO_FILTRO_CIUDADES.innerHTML += `<ion-select-option value="${ciudad.id}">${ciudad.nombre}</ion-select-option>`;
  }
}

function precargarDepartamentosYCiudades() {
  departamentosYCiudades = Store.getDepartamentosYCiudades() ?? [];
  if (!departamentosYCiudades.length) {
    return API.getDepartamentosYCiudades().then((data) => {
      departamentosYCiudades = data;
      Store.setDepartamentosYCiudades(departamentosYCiudades);
      cargarComboDepartamentos();
    });
  }

  cargarComboDepartamentos();
}

function restringirFechaCalendario() {
  qSlc("#pantalla-eventos-fecha").max = new Date().toISOString();
}

function inicializar() {
  Store.restore(); // intentará cargar los datos cacheados.
  API.setBaseUrl("https://babytracker.develotion.com");
  API.restore(); // Intentará cargar la sesión anterior del usuario.

  precargarDepartamentosYCiudades();
  suscribirmeAEventos();
  cargarPosicionUsuario();
  restringirFechaCalendario();
}

inicializar();

// #endregion
