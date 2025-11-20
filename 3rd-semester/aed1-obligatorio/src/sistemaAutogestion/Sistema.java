package sistemaAutogestion;

// Mateo C
import dominio.*;
import tads.*;

public class Sistema implements IObligatorio {

    private Deposito m_deposito;
    private ILista<Usuario> m_usuarios;
    private ILista<Estacion> m_estaciones;
    private PilaSE<String> m_historialAlquileres;

    public Sistema() {
        this.m_deposito = new Deposito();
        this.m_usuarios = new ListaSE<>();
        this.m_estaciones = new ListaSE<>();
        this.m_historialAlquileres = new PilaSE<>();
    }

    @Override
    public Retorno crearSistemaDeGestion() {
        this.m_deposito = new Deposito();
        this.m_usuarios = new ListaSE<>();
        this.m_estaciones = new ListaSE<>();
        this.m_historialAlquileres = new PilaSE<>();

        return Retorno.ok();
    }

    @Override
    public Retorno registrarEstacion(String nombre, String barrio, int capacidad) {
        // 1: Si alguno de los parámetros es null o vacío.
        if (
            Validadores.esNulo(nombre) ||
            Validadores.esNulo(barrio) ||
            Validadores.esStringVacio(nombre) ||
            Validadores.esStringVacio(barrio)
        ) return Retorno.error1();

        // 2: capacidad <= 0
        if (capacidad <= 0) return Retorno.error2();

        // 3: estación ya existe
        if (this.buscarEstacion(nombre) != null) return Retorno.error3();

        Estacion e = new Estacion(nombre, barrio, capacidad);
        this.m_estaciones.adicionarOrdenado(e, Comparadores.EstacionCmp.porNombreAscendente);
        return Retorno.ok();
    }

    private Estacion buscarEstacion(String nombre) {
        if (Validadores.esStringVacio(nombre)) return null;

        for (int i = 0; i < this.m_estaciones.longitud(); i++) {
            try {
                Estacion e = this.m_estaciones.obtener(i);
                if (nombre.equals(e.getNombre())) return e;
            } catch (Exception e) {
                return null;
            }
        }

        return null;
    }

    @Override
    public Retorno registrarUsuario(String cedula, String nombre) {
        // 1: Si alguno de los parámetros es null o vacío.
        if (
            Validadores.esNulo(cedula) ||
            Validadores.esNulo(nombre) ||
            Validadores.esStringVacio(cedula) ||
            Validadores.esStringVacio(nombre)
        ) return Retorno.error1();

        // 2: formato cédula inválido
        if (!Validadores.UsuarioV.esCedulaValida(cedula)) return Retorno.error2();

        // 3: ya existe usuario con esa cédula
        if (this.buscarUsuario(cedula) != null) return Retorno.error3();

        Usuario u = new Usuario(cedula, nombre);
        this.m_usuarios.adicionarOrdenado(u, Comparadores.UsuarioCmp.porNombreAscendente);
        return Retorno.ok();
    }

    private Usuario buscarUsuario(String cedula) {
        if (Validadores.esStringVacio(cedula)) return null;
        for (int i = 0; i < this.m_usuarios.longitud(); i++) {
            try {
                Usuario u = this.m_usuarios.obtener(i);
                if (cedula.equals(u.getCedula())) return u;
            } catch (Exception e) {
                return null;
            }
        }

        return null;
    }

    public Bicicleta buscarBicicleta(String codigo) {
        Bicicleta enDeposito = this.m_deposito.buscarBicicleta(codigo);
        if (enDeposito != null) return enDeposito;

        for (int i = 0; i < this.m_estaciones.longitud(); i++) {
            try {
                Estacion e = this.m_estaciones.obtener(i);
                Bicicleta b = e.buscarBicicleta(codigo);
                if (b != null) return b;
            } catch (Exception e) {
                return null;
            }
        }

        // Buscar en bicicletas alquiladas por usuarios
        for (int i = 0; i < this.m_usuarios.longitud(); i++) {
            try {
                Usuario u = this.m_usuarios.obtener(i);
                Bicicleta b = u.getBicicletaActual();
                if (b != null && codigo.equals(b.getCodigo())) return b;
            } catch (Exception e) {
                return null;
            }
        }

        return null;
    }

    @Override
    public Retorno registrarBicicleta(String codigo, String tipo) {
        // 1: Si alguno de los parámetros es null o vacío.
        if (
            Validadores.esNulo(codigo) ||
            Validadores.esNulo(tipo) ||
            Validadores.esStringVacio(codigo) ||
            Validadores.esStringVacio(tipo)
        ) return Retorno.error1();

        // 2: Formato incorrecto de código
        if (!Validadores.BicicletaV.esCodigoValido(codigo)) return Retorno.error2();

        // 3: El tipo no está dentro de los permitidos
        if (!Validadores.BicicletaV.esTipoValido(tipo)) return Retorno.error3();

        // 4: Ya existe bici con ese código
        if (this.buscarBicicleta(codigo) != null) return Retorno.error4();

        Bicicleta b = new Bicicleta(codigo, tipo);
        this.m_deposito.agregarBicicleta(b);
        return Retorno.ok();
    }

    @Override
    public Retorno marcarEnMantenimiento(String codigo, String motivo) {
        // 1: Si alguno de los parámetros es null o vacío.
        if (
            Validadores.esNulo(codigo) ||
            Validadores.esNulo(motivo) ||
            Validadores.esStringVacio(codigo) ||
            Validadores.esStringVacio(motivo)
        ) return Retorno.error1();

        // 2: bici inexistente
        Bicicleta b = this.buscarBicicleta(codigo);
        if (b == null) return Retorno.error2();

        // 3: bici actualmente alquilada
        if (b.getEstado() == BicicletaEstado.Alquilada) return Retorno.error3();

        // 4: bici ya en mantenimiento
        if (b.getEstado() == BicicletaEstado.Mantenimiento) return Retorno.error4();

        // Sacar de estacion si estaba en una y mover al deposito si no esta ya
        this.sacarBicicletaDeEstacion(b);
        if (this.m_deposito.buscarBicicleta(codigo) == null) this.m_deposito.agregarBicicleta(b);

        b.setEstado(BicicletaEstado.Mantenimiento);
        return Retorno.ok();
    }

    @Override
    public Retorno repararBicicleta(String codigo) {
        // 1: Si alguno de los parámetros es null o vacío.
        if (Validadores.esNulo(codigo) || Validadores.esStringVacio(codigo)) return Retorno.error1();

        // 2: bici inexistente, o no se encuentra en el deposito en este momento.
        Bicicleta b = this.m_deposito.buscarBicicleta(codigo);
        if (b == null) return Retorno.error2();

        // 3: bici no se encuentra en mantenimiento
        if (b.getEstado() != BicicletaEstado.Mantenimiento) return Retorno.error3();

        b.setEstado(BicicletaEstado.Disponible);
        return Retorno.ok();
    }

    @Override
    public Retorno eliminarEstacion(String nombre) {
        // 1: Si alguno de los parámetros es null o vacío
        if (Validadores.esStringVacio(nombre)) return Retorno.error1();

        // 2: no existe estación
        Estacion estacion = this.buscarEstacion(nombre);
        if (estacion == null) return Retorno.error2();

        // 3: tiene bicis ancladas/colas pendientes
        boolean tieneBicis = estacion.cantidadBicicletas() > 0;
        boolean tieneUsuariosEspera = estacion.hayUsuariosEnEspera();
        boolean tieneBicisEsperaAnclaje = !estacion.getColaEsperaAnclajes().estaVacia();

        if (tieneBicis || tieneUsuariosEspera || tieneBicisEsperaAnclaje) return Retorno.error3();

        // Eliminar estación
        for (int i = 0; i < this.m_estaciones.longitud(); i++) {
            try {
                Estacion e = this.m_estaciones.obtener(i);
                if (nombre.equals(e.getNombre())) {
                    this.m_estaciones.eliminar(i);
                    return Retorno.ok();
                }
            } catch (Exception e) {}
        }

        return Retorno.error2();
    }

    @Override
    public Retorno asignarBicicletaAEstacion(String codigo, String nombreEstacion) {
        // 1: Si alguno de los parámetros es null o vacío
        if (
            Validadores.esNulo(codigo) ||
            Validadores.esNulo(nombreEstacion) ||
            Validadores.esStringVacio(codigo) ||
            Validadores.esStringVacio(nombreEstacion)
        ) return Retorno.error1();

        // 2: bici no existe o no está "disponible"
        Bicicleta bici = this.buscarBicicleta(codigo);
        if (bici == null || bici.getEstado() != BicicletaEstado.Disponible) return Retorno.error2();

        // 3: estación no existe
        Estacion estacion = this.buscarEstacion(nombreEstacion);
        if (estacion == null) return Retorno.error3();

        // 4: estación sin anclajes libres
        if (!estacion.hayEspacio()) return Retorno.error4();

        // Sacar bici de donde sea que esté actualmente
        boolean fueraDeposito = this.sacarBicicletaDeDeposito(bici);
        if (!fueraDeposito) // si no esta en el deposito, esta en una estacion.
            this.sacarBicicletaDeEstacion(bici);

        // Asignar a la estación
        estacion.recibirBicicleta(bici);
        return Retorno.ok();
    }

    @Override
    public Retorno alquilarBicicleta(String cedula, String nombreEstacion) {
        // 1: Si alguno de los parámetros es null o vacío
        if (
            Validadores.esNulo(cedula) ||
            Validadores.esNulo(nombreEstacion) ||
            Validadores.esStringVacio(cedula) ||
            Validadores.esStringVacio(nombreEstacion)
        ) return Retorno.error1();

        // 2: usuario inexistente
        Usuario usuario = this.buscarUsuario(cedula);
        if (usuario == null) return Retorno.error2();

        // 3: estación inexistente
        Estacion estacion = this.buscarEstacion(nombreEstacion);
        if (estacion == null) return Retorno.error3();

        // Intentar alquiler
        Bicicleta bici = estacion.solicitarAlquiler(usuario);

        if (bici != null) {
            // Se pudo alquilar inmediatamente
            usuario.setCantidadAlquileres(usuario.getCantidadAlquileres() + 1);
            // Registrar en historial para poder deshacer
            this.m_historialAlquileres.apilar(cedula + "#" + bici.getCodigo() + "#" + nombreEstacion);
        }
        // Si no se pudo alquilar, el usuario quedó en cola de espera automáticamente
        return Retorno.ok();
    }

    @Override
    public Retorno devolverBicicleta(String cedula, String nombreEstacionDestino) {
        // 1: Si alguno de los parámetros es null o vacío
        if (
            Validadores.esNulo(cedula) ||
            Validadores.esNulo(nombreEstacionDestino) ||
            Validadores.esStringVacio(cedula) ||
            Validadores.esStringVacio(nombreEstacionDestino)
        ) return Retorno.error1();

        // 2: usuario inexistente o no tiene bici alquilada
        Usuario usuario = this.buscarUsuario(cedula);
        if (usuario == null || usuario.getBicicletaActual() == null) return Retorno.error2();

        // 3: estación destino inexistente
        Estacion estacion = this.buscarEstacion(nombreEstacionDestino);
        if (estacion == null) return Retorno.error3();

        Bicicleta bici = usuario.getBicicletaActual();
        usuario.setBicicletaActual(null);

        // Devolver bicicleta a la estación
        estacion.recibirBicicleta(bici);

        return Retorno.ok();
    }

    @Override
    public Retorno deshacerUltimosRetiros(int n) {
        // 1: n <= 0
        if (n <= 0) return Retorno.error1();

        String retirosDeshechos = "";
        int deshechos = 0;

        while (deshechos < n && !this.m_historialAlquileres.estaVacia()) {
            String registro = this.m_historialAlquileres.desapilar();
            String[] partes = registro.split("#");
            if (partes.length >= 3) {
                String cedulaUsuario = partes[0];
                String codigoBici = partes[1];
                String nombreEstacionOrigen = partes[2];

                Usuario usuario = this.buscarUsuario(cedulaUsuario);
                Bicicleta bici = this.buscarBicicleta(codigoBici);
                Estacion estacionOrigen = this.buscarEstacion(nombreEstacionOrigen);

                if (usuario != null && bici != null && usuario.getBicicletaActual() == bici) {
                    usuario.setBicicletaActual(null);

                    // Intentar devolver bici a estación original
                    if (estacionOrigen != null) { // este check es necesario porque la estacion pudo haber sido eliminada.
                        estacionOrigen.recibirBicicleta(bici);
                    } else {
                        this.m_deposito.agregarBicicleta(bici);
                    }

                    // Disminuir en 1 los alquileres del usuario
                    usuario.setCantidadAlquileres(Math.max(0, usuario.getCantidadAlquileres() - 1));

                    // incrementar retiros deshechos
                    retirosDeshechos += (deshechos > 0 ? "|" : "") + registro;
                    deshechos++;
                }
            }
        }

        return Retorno.ok(retirosDeshechos);
    }

    @Override
    public Retorno obtenerUsuario(String cedula) {
        // 1: Si alguno de los parámetros es null o vacío
        if (Validadores.esStringVacio(cedula)) return Retorno.error1();
        // 2: formato cédula inválido
        if (!Validadores.UsuarioV.esCedulaValida(cedula)) return Retorno.error2();
        // 3: usuario inexistente
        Usuario u = this.buscarUsuario(cedula);
        if (u == null) return Retorno.error3();

        return Retorno.ok(u.toString());
    }

    @Override
    public Retorno listarUsuarios() {
        String usuariosFmt = this.m_usuarios.toString();
        return Retorno.ok(usuariosFmt);
    }

    @Override
    public Retorno listarBicisEnDeposito() {
        String bicisFmt = this.listarBicisRecursivo(this.m_deposito.getBicicletas(), 0);
        return Retorno.ok(bicisFmt);
    }

    private String listarBicisRecursivo(ILista<Bicicleta> lista, int indice) {
        if (indice >= lista.longitud()) return "";

        try {
            Bicicleta bici = lista.obtener(indice);
            String actual = bici.toString();
            String siguiente = listarBicisRecursivo(lista, indice + 1);

            if (siguiente.isEmpty()) return actual;
            else return actual + "|" + siguiente;
        } catch (Exception e) {
            return "";
        }
    }

    // informacionMapa funciona con matrices cuadradas y rectangulares.
    @Override
    public Retorno informaciónMapa(String[][] mapa) {
        // 1) Indicar el máximo de estaciones en una misma fila o columna dentro
        //    del mapa. Adicionalmente al retorno agregar (separando por un #)
        //    si el máximo lo determinó una fila, columna o ambas. En caso de
        //    estar vacía indicar 0#ambas.
        if (mapa == null || mapa.length == 0) return Retorno.ok("0#ambas");

        int filas = mapa.length;
        int cols = 0;
        for (String[] fila : mapa) {
            if (fila != null) cols = Math.max(cols, fila.length);
        }
        
        if (cols == 0) return Retorno.ok("0#ambas");

        int[] estacionesEnFilas = new int[filas];
        int[] estacionesEnColumnas = new int[cols];
        boolean vacia = true;

        // Contar estaciones
        for (int i = 0; i < filas; i++) {
            if (mapa[i] == null) continue;
            for (int j = 0; j < mapa[i].length; j++) {
                if (mapa[i][j] != null && mapa[i][j].startsWith("E")) {
                    estacionesEnFilas[i]++;
                    estacionesEnColumnas[j]++;
                    vacia = false;
                }
            }
        }
        
        if (vacia) return Retorno.ok("0#ambas");

        // Maximos
        int maxFila = 0, maxCol = 0;
        for (int f : estacionesEnFilas) maxFila = Math.max(maxFila, f);
        for (int c : estacionesEnColumnas) maxCol = Math.max(maxCol, c);

        int max = Math.max(maxFila, maxCol);
        String parte1;

        if (max == 0) parte1 = "0#ambas";
        else if (maxFila == max && maxCol == max) parte1 = max + "#ambas";
        else if (maxFila == max) parte1 = max + "#fila";
        else parte1 = max + "#columna";

        // 2) Si existen en algún caso, 3 columnas consecutivas (de izquierda a
        //    derecha), cuya cantidad de estaciones es estrictamente ascendente
        //    (para el ejemplo dado, las columnas 1,2 y 3 cumplen y también las
        //    2,3 y 4 cumplen)
        boolean existe = false;
        for (int i = 0; i <= cols - 3; i++) {
            if (
                estacionesEnColumnas[i] < estacionesEnColumnas[i + 1] &&
                estacionesEnColumnas[i + 1] < estacionesEnColumnas[i + 2]
            ) {
                existe = true;
                break;
            }
        }
        String parte2 = existe ? "existe" : "no existe";

        return Retorno.ok(parte1 + "|" + parte2);
    }

    @Override
    public Retorno listarBicicletasDeEstacion(String nombreEstacion) {
        if (Validadores.esStringVacio(nombreEstacion)) return Retorno.error1();

        Estacion estacion = this.buscarEstacion(nombreEstacion);
        if (estacion == null) return Retorno.error2();

        // Obtener bicis de la estación y ordenar por código
        ILista<String> codigos = new ListaSE<>();
        for (int i = 0; i < estacion.getBicicletas().longitud(); i++) {
            try {
                Bicicleta bici = estacion.getBicicletas().obtener(i);
                codigos.adicionarOrdenado(bici.getCodigo(), String::compareTo);
            } catch (Exception e) {}
        }

        return Retorno.ok(codigos.toString());
    }

    @Override
    public Retorno estacionesConDisponibilidad(int n) {
        // 1. Si n <= 1
        if (n <= 1) return Retorno.error1();

        int contador = 0;
        for (int i = 0; i < this.m_estaciones.longitud(); i++) {
            try {
                Estacion estacion = this.m_estaciones.obtener(i);
                int bicicletasDisponibles = estacion.cantidadBicicletas();
                if (bicicletasDisponibles > n) contador++;
            } catch (Exception e) {}
        }

        return Retorno.ok(contador);
    }

    @Override
    public Retorno ocupacionPromedioXBarrio() {
        // Si no hay estaciones, retornar cadena vacía
        if (this.m_estaciones.longitud() == 0)
            return Retorno.ok("");

        // Primero recolectar todos los barrios únicos y ordenarlos
        ILista<String> barriosUnicos = new ListaSE<>();
        for (int i = 0; i < this.m_estaciones.longitud(); i++) {
            try {
                String barrio = this.m_estaciones.obtener(i).getBarrio();
                boolean existe = false;
                for (int j = 0; j < barriosUnicos.longitud(); j++) {
                    if (barriosUnicos.obtener(j).equals(barrio)) {
                        existe = true;
                        break;
                    }
                }
                if (!existe) {
                    barriosUnicos.adicionarOrdenado(barrio, String::compareTo);
                }
            } catch (Exception e) {}
        }

        // Ahora procesar cada barrio
        String resultado = "";
        for (int i = 0; i < barriosUnicos.longitud(); i++) {
            try {
                String barrioActual = barriosUnicos.obtener(i);
                int totalBicis = 0;
                int totalCapacidad = 0;

                // Sumar las bicis y capacidades de todas las estaciones del barrio
                for (int j = 0; j < this.m_estaciones.longitud(); j++) {
                    Estacion est = this.m_estaciones.obtener(j);
                    if (est.getBarrio().equals(barrioActual)) {
                        totalBicis += est.cantidadBicicletas();
                        totalCapacidad += est.getCapacidad();
                    }
                }

                // Calcular porcentaje
                int porcentaje = totalCapacidad > 0 ? Math.round((totalBicis * 100) / totalCapacidad) : 0;
                
                // Agregar al resultado
                if (i > 0) {
                    resultado += "|";
                }
                resultado += barrioActual + "#" + porcentaje;
            } catch (Exception e) {}
        }

        return Retorno.ok(resultado);
    }

    @Override
    public Retorno rankingTiposPorUso() {
        String[] tiposBici = Validadores.BicicletaV.getTiposValidos();
        int[] conteoPorTipo = new int[tiposBici.length];

        // contar alquileres
        PilaSE<String> copiaHistorial = this.m_historialAlquileres.copiarPila();
        while (!copiaHistorial.estaVacia()) {
            String registro = copiaHistorial.desapilar();
            String[] partes = registro.split("#");
            if (partes.length >= 2) {
                Bicicleta bici = this.buscarBicicleta(partes[1]);
                if (bici != null) {
                    String tipo = bici.getTipo();
                    for (int i = 0; i < tiposBici.length; i++) {
                        if (tiposBici[i].equals(tipo)) {
                            conteoPorTipo[i]++;
                            break;
                        }
                    }
                }
            }
        }

        // ordenar por cantidad desc, luego tipo asc
        for (int i = 0; i < tiposBici.length - 1; i++) {
            for (int j = 0; j < tiposBici.length - i - 1; j++) {
                boolean intercambiar = false;
                if (conteoPorTipo[j] < conteoPorTipo[j + 1]) {
                    intercambiar = true;
                } else if (conteoPorTipo[j] == conteoPorTipo[j + 1] &&
                           tiposBici[j].compareTo(tiposBici[j + 1]) > 0) {
                    intercambiar = true;
                }
                if (intercambiar) {
                    int tmpCant = conteoPorTipo[j];
                    conteoPorTipo[j] = conteoPorTipo[j + 1];
                    conteoPorTipo[j + 1] = tmpCant;

                    String tmpTipo = tiposBici[j];
                    tiposBici[j] = tiposBici[j + 1];
                    tiposBici[j + 1] = tmpTipo;
                }
            }
        }

        // generar salida
        String resultado = "";
        for (int i = 0; i < tiposBici.length; i++) {
            if (i > 0) resultado += "|";
            resultado += (tiposBici[i]) + "#" + conteoPorTipo[i];
        }

        return Retorno.ok(resultado);
    }

    @Override
    public Retorno usuariosEnEspera(String nombreEstacion) {
        if (Validadores.esStringVacio(nombreEstacion)) return Retorno.error1();

        Estacion estacion = this.buscarEstacion(nombreEstacion);
        if (estacion == null) return Retorno.error2();

        String resultado = "";
        ColaSE<Usuario> cola = estacion.getColaEsperaUsuarios().copiarCola();

        ILista<String> cedulasEnEspera = new ListaSE<>();

        while (!cola.estaVacia()) {
            Usuario u = cola.desencolar();
            cedulasEnEspera.adicionar(u.getCedula());
        }

        // Formatear resultado
        for (int i = 0; i < cedulasEnEspera.longitud(); i++) {
            try {
                resultado += (i > 0 ? "|" : "") + cedulasEnEspera.obtener(i);
            } catch (Exception e) {}
        }

        return Retorno.ok(resultado);
    }

    @Override
    public Retorno usuarioMayor() {
        if (this.m_usuarios.longitud() == 0) return Retorno.ok("");

        String cedulaMayor = "";
        int maxAlquileres = -1;

        for (int i = 0; i < this.m_usuarios.longitud(); i++) {
            try {
                Usuario usuario = this.m_usuarios.obtener(i);
                int alquileres = usuario.getCantidadAlquileres();

                if (
                    alquileres > maxAlquileres ||
                    (alquileres == maxAlquileres &&
                        (cedulaMayor.isEmpty() ||
                            usuario.getCedula().compareTo(cedulaMayor) < 0))
                ) {
                    maxAlquileres = alquileres;
                    cedulaMayor = usuario.getCedula();
                }
            } catch (Exception e) {}
        }

        return Retorno.ok(cedulaMayor);
    }

    private boolean sacarBicicletaDeDeposito(Bicicleta bici) {
        ILista<Bicicleta> bicis = this.m_deposito.getBicicletas();
        for (int i = 0; i < bicis.longitud(); i++) {
            try {
                if (bicis.obtener(i) == bici) {
                    bicis.eliminar(i);
                    return true;
                }
            } catch (Exception e) {}
        }
        return false;
    }

    private boolean sacarBicicletaDeEstacion(Bicicleta bici) {
        for (int i = 0; i < this.m_estaciones.longitud(); i++) {
            try {
                Estacion estacion = this.m_estaciones.obtener(i);
                ILista<Bicicleta> bicis = estacion.getBicicletas();
                for (int j = 0; j < bicis.longitud(); j++) {
                    if (bicis.obtener(j) == bici) {
                        bicis.eliminar(j);
                        return true;
                    }
                }
            } catch (Exception e) {}
        }
        return false;
    }
}
