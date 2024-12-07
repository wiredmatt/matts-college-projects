// reference: https://www.mongodb.com/docs/mongodb-shell/write-scripts/#connect-to-a-local-mongodb-instance

// conn = Mongo("mongodb://<username>:<password>@localhost:27017/<authDB>");
conn = Mongo("mongodb://root:example@localhost/admin");
db = conn.getDB("CatHotel");

db.propietarios.insertMany([
  {
    propietarioDocumento: "12345678",
    propietarioNombre: "JuanCho",
    propietarioTelefono: "099123456",
    propietarioEmail: "juancho@gmail.com",
  },
  {
    propietarioDocumento: "87654321",
    propietarioNombre: "Pedro Gomez",
    propietarioTelefono: "099654321",
    propietarioEmail: "pedrogomez@gmail.com",
  },
  {
    propietarioDocumento: "91979735",
    propietarioNombre: "Mateo C",
    propietarioTelefono: "092831273",
    propietarioEmail: "mateoc@gmail.com",
  },
  {
    propietarioDocumento: "42445789",
    propietarioNombre: "Andres F",
    propietarioTelefono: "094567667",
    propietarioEmail: "andres_546@gmail.com",
  },
  {
    propietarioDocumento: "45768751",
    propietarioNombre: "Robert I",
    propietarioTelefono: "092887699",
    propietarioEmail: "robert22@gmail.com",
  },
  {
    propietarioDocumento: "37659347",
    propietarioNombre: "Carlos I",
    propietarioTelefono: "095787990",
    propietarioEmail: "Carlos3445@gmail.com",
  },
  {
    propietarioDocumento: "24536475",
    propietarioNombre: "Jose G",
    propietarioTelefono: "095768776",
    propietarioEmail: "Jose_34@gmail.com",
  },
  {
    propietarioDocumento: "42758599",
    propietarioNombre: "Clara V",
    propietarioTelefono: "094776432",
    propietarioEmail: "ClaraV_43@gmail.com",
  },
  {
    propietarioDocumento: "34568873",
    propietarioNombre: "Micaela V",
    propietarioTelefono: "096554263",
    propietarioEmail: "micaela234@gmail.com",
  },
  {
    propietarioDocumento: "45462253",
    propietarioNombre: "Ana B",
    propietarioTelefono: "093546567",
    propietarioEmail: "Ana_B_24@gmail.com",
  },
]);
db.propietarios.createIndex({ propietarioDocumento: 1 }, { unique: true });

db.gatos.insertMany([
  {
    gatoNombre: "Pepe",
    gatoRaza: "Siames",
    gatoEdad: 5,
    gatoPeso: 6.2,
    propietarioDocumento: "12345678",
  },
  {
    gatoNombre: "Pepito",
    gatoRaza: "Birman",
    gatoEdad: 4,
    gatoPeso: 7.1,
    propietarioDocumento: "12345678",
  },
  {
    gatoNombre: "Pipo JR",
    gatoRaza: "Persa",
    gatoEdad: 13,
    gatoPeso: 3.2,
    propietarioDocumento: "87654321",
  },
  {
    gatoNombre: "Pity",
    gatoRaza: "Persa",
    gatoEdad: 13,
    gatoPeso: 3.2,
    propietarioDocumento: "87654321",
  },
  {
    gatoNombre: "Ramon",
    gatoRaza: "Persa",
    gatoEdad: 14,
    gatoPeso: 4.1,
    propietarioDocumento: "87654321",
  },
  {
    gatoNombre: "Marco Aurelio",
    gatoRaza: "Birman",
    gatoEdad: 11,
    gatoPeso: 4.4,
    propietarioDocumento: "91979735",
  },
  {
    gatoNombre: "Luna",
    gatoRaza: "Bengali",
    gatoEdad: 3,
    gatoPeso: 4.5,
    propietarioDocumento: "91979735",
  },
  {
    gatoNombre: "Milo",
    gatoRaza: "Sphynx",
    gatoEdad: 4,
    gatoPeso: 5,
    propietarioDocumento: "45768751",
  },
  {
    gatoNombre: "Nina",
    gatoRaza: "Maine Coon",
    gatoEdad: 2,
    gatoPeso: 6.8,
    propietarioDocumento: "45768751",
  },
  {
    gatoNombre: "Simba",
    gatoRaza: "Sphynx ",
    gatoEdad: 5,
    gatoPeso: 4.2,
    propietarioDocumento: "42758599",
  },
  {
    gatoNombre: "Olivia",
    gatoRaza: "Siames",
    gatoEdad: 1,
    gatoPeso: 3.9,
    propietarioDocumento: "45462253",
  },
  {
    gatoNombre: "Leo",
    gatoRaza: "Ragdoll",
    gatoEdad: 4,
    gatoPeso: 5.3,
    propietarioDocumento: "34568873",
  },
  {
    gatoNombre: "Nina",
    gatoRaza: "Sphynx",
    gatoEdad: 2,
    gatoPeso: 3.8,
    propietarioDocumento: "24536475",
  },
  {
    gatoNombre: "Charlie",
    gatoRaza: "Ragdoll",
    gatoEdad: 5,
    gatoPeso: 7.2,
    propietarioDocumento: "34568873",
  },
  {
    gatoNombre: "Loki",
    gatoRaza: "Abyssinian",
    gatoEdad: 3,
    gatoPeso: 4.1,
    propietarioDocumento: "24536475",
  },
  {
    gatoNombre: "Malvabizco",
    gatoRaza: "Siames",
    gatoEdad: 6,
    gatoPeso: 5.5,
    propietarioDocumento: "42758599",
  },
  {
    gatoNombre: "Oscar",
    gatoRaza: "Persa",
    gatoEdad: 8,
    gatoPeso: 6,
    propietarioDocumento: "34568873",
  },
  {
    gatoNombre: "Alfonso",
    gatoRaza: "Ragdoll",
    gatoEdad: 2,
    gatoPeso: 8.5,
    propietarioDocumento: "24536475",
  },
  {
    gatoNombre: "Toby",
    gatoRaza: "Birman",
    gatoEdad: 4,
    gatoPeso: 4.9,
    propietarioDocumento: "45462253",
  },
  {
    gatoNombre: "Lily",
    gatoRaza: "Bengali",
    gatoEdad: 3,
    gatoPeso: 4.6,
    propietarioDocumento: "42758599",
  },
  {
    gatoNombre: "Nano",
    gatoRaza: "Sphynx",
    gatoEdad: 1,
    gatoPeso: 3.2,
    propietarioDocumento: "45768751",
  },
]);
db.gatos.createIndex({ propietarioDocumento: 1, gatoNombre: 1 });

db.servicios.insertMany([
  {
    servicioNombre: "PASEO",
    servicioPrecio: 200,
  },
  {
    servicioNombre: "PELUQUERIA",
    servicioPrecio: 100,
  },
  {
    servicioNombre: "CONTROL_PARASITOS",
    servicioPrecio: 300,
  },
  {
    servicioNombre: "REVISION_VETERINARIA",
    servicioPrecio: 500,
  },
  {
    servicioNombre: "JUEGO",
    servicioPrecio: 100,
  },
  {
    servicioNombre: "MASAJE",
    servicioPrecio: 150,
  },
  {
    servicioNombre: "ENTRENAMIENTO",
    servicioPrecio: 180,
  },
]);
db.servicios.createIndex({ servicioNombre: 1 }, { unique: true });

db.habitaciones.insertMany([
  {
    habitacionNombre: "Habitacion1",
    habitacionCapacidad: 5,
    habitacionPrecio: 20,
    habitacionEstado: "DISPONIBLE",
  },
  {
    habitacionNombre: "Habitacion2",
    habitacionCapacidad: 10,
    habitacionPrecio: 45,
    habitacionEstado: "DISPONIBLE",
  },
  {
    habitacionNombre: "Habitacion3",
    habitacionCapacidad: 7,
    habitacionPrecio: 35,
    habitacionEstado: "LIMPIANDO",
  },
  {
    habitacionNombre: "Habitacion4",
    habitacionCapacidad: 15,
    habitacionPrecio: 60,
    habitacionEstado: "DISPONIBLE",
  },
  {
    habitacionNombre: "Habitacion5",
    habitacionCapacidad: 20,
    habitacionPrecio: 120,
    habitacionEstado: "LIMPIANDO",
  },
  {
    habitacionNombre: "Habitacion6",
    habitacionCapacidad: 12,
    habitacionPrecio: 75,
    habitacionEstado: "DISPONIBLE",
  },
  {
    habitacionNombre: "Habitacion7",
    habitacionCapacidad: 6,
    habitacionPrecio: 40,
    habitacionEstado: "DISPONIBLE",
  },
  {
    habitacionNombre: "Habitacion8",
    habitacionCapacidad: 8,
    habitacionPrecio: 55,
    habitacionEstado: "LIMPIANDO",
  },
  {
    habitacionNombre: "Habitacion9",
    habitacionCapacidad: 18,
    habitacionPrecio: 90,
    habitacionEstado: "DISPONIBLE",
  },
  {
    habitacionNombre: "Habitacion10",
    habitacionCapacidad: 11,
    habitacionPrecio: 50,
    habitacionEstado: "LIMPIANDO",
  },
  {
    habitacionNombre: "Habitacion11",
    habitacionCapacidad: 1,
    habitacionPrecio: 50,
    habitacionEstado: "DISPONIBLE",
  },
  {
    habitacionNombre: "Suite1",
    habitacionCapacidad: 1,
    habitacionPrecio: 500,
    habitacionEstado: "DISPONIBLE",
  },
]);
db.habitaciones.createIndex({ habitacionNombre: 1 }, { unique: true });
db.habitaciones.createIndex({ habitacionEstado: 1 });

// A. Cree la(s) colecciones de documentos JSON para almacenar una reserva y que luego se pueda
// consultar de forma óptima todos los datos de una reserva (habitación, servicios adicionales,
// gato, dueño, etc.). Oriente su diseño a performance de lectura como objetivo principal por
// encima de los criterios de integridad de datos que nos tiene acostumbrados el modelo
// relacional
db.reservas.insertMany([
  {
    reservaFechaInicio: "2024-07-02",
    reservaFechaFin: "2024-07-09",
    habitacion: {
      habitacionNombre: "Habitacion1",
      habitacionPrecio: 20.0,
    },
    gato: {
      gatoNombre: "Pepito",
      gatoRaza: "Birman",
      gatoEdad: 4,
    },
    propietario: {
      propietarioDocumento: "12345678",
      propietarioNombre: "JuanCho",
    },
    servicios: [
      {
        servicioNombre: "PELUQUERIA",
        servicioPrecio: 100,
        servicioCantidad: 1,
      },
      {
        servicioNombre: "REVISION_VETERINARIA",
        servicioPrecio: 500,
        servicioCantidad: 1,
      },
    ],
  },
  {
    reservaFechaInicio: "2024-11-06",
    reservaFechaFin: "2024-11-09",
    habitacion: {
      habitacionNombre: "Habitacion1",
      habitacionPrecio: 20.0,
    },
    gato: {
      gatoNombre: "Pepe",
      gatoRaza: "Siames",
      gatoEdad: 5,
    },
    propietario: {
      propietarioDocumento: "12345678",
      propietarioNombre: "JuanCho",
    },
    servicios: [
      {
        servicioNombre: "CONTROL_PARASITOS",
        servicioPrecio: 300,
        servicioCantidad: 1,
      },
      {
        servicioNombre: "REVISION_VETERINARIA",
        servicioPrecio: 500,
        servicioCantidad: 1,
      },
    ],
  },
  {
    reservaFechaInicio: "2024-07-06",
    reservaFechaFin: "2024-07-23",
    habitacion: {
      habitacionNombre: "Habitacion2",
      habitacionPrecio: 45.0,
    },
    gato: {
      gatoNombre: "Pipo JR",
      gatoRaza: "Persa",
      gatoEdad: 13,
    },
    propietario: {
      propietarioDocumento: "87654321",
      propietarioNombre: "Pedro Gomez",
    },
    servicios: [
      {
        servicioNombre: "PASEO",
        servicioPrecio: 200,
        servicioCantidad: 2,
      },
      {
        servicioNombre: "PELUQUERIA",
        servicioPrecio: 100,
        servicioCantidad: 1,
      },
    ],
  },
  {
    reservaFechaInicio: "2024-07-06",
    reservaFechaFin: "2024-07-23",
    habitacion: {
      habitacionNombre: "Habitacion2",
      habitacionPrecio: 45.0,
    },
    gato: {
      gatoNombre: "Pity",
      gatoRaza: "Persa",
      gatoEdad: 13,
    },
    propietario: {
      propietarioDocumento: "87654321",
      propietarioNombre: "Pedro Gomez",
    },
    servicios: [
      {
        servicioNombre: "PASEO",
        servicioPrecio: 200,
        servicioCantidad: 2,
      },
      {
        servicioNombre: "PELUQUERIA",
        servicioPrecio: 100,
        servicioCantidad: 1,
      },
    ],
  },
]);
db.reservas.createIndex({ reservaFechaInicio: 1, reservaFechaFin: 1 });
db.reservas.createIndex({ "habitacion.habitacionNombre": 1 });
db.reservas.createIndex({ "gato.gatoNombre": 1 });
db.reservas.createIndex({ "propietario.propietarioDocumento": 1 });

print(db.getCollectionNames());
