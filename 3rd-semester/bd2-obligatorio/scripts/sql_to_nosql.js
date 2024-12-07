#!/usr/bin/env node

// script experimental para traducir instrucciones SQL a instrucciones de MongoDB
// usado para evitar sindrome del tunel carpiano al escribir inserts manualmente (de nuevo!) en MongoDB

const path = require("path");
const fs = require("fs");

const HERE = __dirname;

const OUTPUT_FILE_PATH = path.join(HERE, "..", "nosql", "mongo_inserts.js");

const DML_FILE_PATH = path.join(HERE, "..", "sql", "DML_CatHotel.sql");
const DML_FILE_CONTENTS = fs.readFileSync(DML_FILE_PATH, "utf8").toString();

const DDL_FILE_PATH = path.join(HERE, "..", "sql", "DDL_CatHotel.sql");
const DDL_FILE_CONTENTS = fs.readFileSync(DDL_FILE_PATH, "utf8").toString();

const ignoreLinesStartingWith = ["--", "IF", "DELETE", "USE", "GO"];
const lines = DML_FILE_CONTENTS.split("VALUES").join("\n").split("\n");
let currentTable = "";
let currentValues = [];
let sql_inserts = [];
for (let line of lines) {
  if (ignoreLinesStartingWith.some((prefix) => line.startsWith(prefix))) {
    continue;
  }
  if (!line) {
    continue;
  }

  if (line?.startsWith("INSERT INTO")) {
    if (currentTable) {
      sql_inserts.push({
        table: currentTable,
        values: currentValues,
      });
    }
    currentTable = line.split(" ")[2];
    currentValues = [];
  } else {
    // ('Pepe', 'Siames', 5, 6.2, '12345678')
    const _values = line.match(/\((.*)\)/)[1];
    // ['Pepe', 'Siames', 5, 6.2, '12345678']
    currentValues.push(_values);
  }
}

const tablesAndColumns = DDL_FILE_CONTENTS.split("\n").reduce((acc, line) => {
  const ignore = ["CONSTRAINT", "PRIMARY"];
  const ignoreColumsWith = ["IDENTITY"]; // mongodb creara sus propios IDs, "_id"
  const createTableMatch = line.match(/CREATE TABLE (\w+)/);
  if (createTableMatch) {
    const table = createTableMatch[1];
    acc[table] = [];
  } else {
    const columnMatch = line.match(/^\s+(\w+)/);
    if (columnMatch && !ignore.includes(columnMatch[1])) {
      if (!ignoreColumsWith.some((icw) => line.includes(icw))) {
        acc[Object.keys(acc).slice(-1)[0]].push(columnMatch[1]);
      }
    }
  }
  return acc;
}, {});

// Merge occurrences under the same table key - Para casos en los cuales los inserts a una misma tabla se hacen por separado
const mergedData = new Map();

sql_inserts.forEach(({ table, values }) => {
  if (mergedData.has(table)) {
    mergedData.get(table).values.push(...values);
  } else {
    mergedData.set(table, { table, values: [...values] });
  }
});

const dataToWrite = Array.from(mergedData.values()).map(({ table, values }) => {
  const columns = tablesAndColumns[table];
  return {
    table,
    columns,
    values,
  };
});

let nosql_inserts = [];

const toCamelCase = (str) => {
  return str
    .split("_")
    .map(
      (word, index) =>
        index === 0
          ? word.charAt(0).toLowerCase() + word.slice(1) // primera letra de primera palabra minuscula
          : word.charAt(0).toUpperCase() + word.slice(1) // primera letra de las demas palabras mayuscula
    )
    .join("");
};

for (let i = 0; i < dataToWrite.length; i++) {
  let { table, columns, values } = dataToWrite[i];
  table = toCamelCase(table);
  const documents = values.map((value) => {
    const rowValues = value
      .split(",")
      .map((v) => v.trim().replace(/^'|'$/g, ""));
    let document = {};
    columns.forEach((col, index) => {
      document[col] = isNaN(rowValues[index])
        ? rowValues[index]
        : Number(rowValues[index]);
    });
    return document;
  });

  const insert = `const ${table}InsertsResult = db.${table}.insertMany(${JSON.stringify(
    documents,
    null,
    2
  )});`;
  nosql_inserts.push(insert);
}

const content = nosql_inserts.join("\n");

fs.writeFileSync(OUTPUT_FILE_PATH, content);
