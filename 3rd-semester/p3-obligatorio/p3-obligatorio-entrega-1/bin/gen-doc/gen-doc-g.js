#!/usr/bin/env node
const fs = require("fs");
const path = require("path");
const { google } = require("googleapis");
require("dotenv").config({
  path: path.join(__dirname, ".env"),
});

const here = __dirname;
const BASE_DIR = path.join(here, "..", "..");
const IGNORE_FILES_IN = ["bin", "obj"];
const PACKAGES = [
  "LibreriaWeb",
  "Compartido",
  "LogicaNegocio",
  "LogicaAplicacion",
  "LogicaAccesoDatos",
];
const BASE_INDEX = 5;
const CODIGO_FUENTE_SECTION = `${BASE_INDEX}. Código Fuente`;

// Load service account credentials from the JSON key file
const serviceAccountKeyFilePath = path.join(here, "key.json");
const __DOC_ID__ = process.env.GOOGLE_DOC_ID;

/**
 * @type {import('googleapis').docs_v1.Docs}
 */
let DOCS = null;

/**
 * @type {import('googleapis').docs_v1.Schema$Document}
 */
let __DOC__ = null;

function authenticate() {
  const auth = new google.auth.GoogleAuth({
    keyFile: serviceAccountKeyFilePath,
    scopes: ["https://www.googleapis.com/auth/documents"],
  });
  return auth.getClient();
}

/**
 *
 * @param {import('googleapis').docs_v1.Docs} docs
 * @returns
 */
async function findCodigoFuenteIndex() {
  const content = __DOC__.body.content;

  for (let i = 0; i < content.length; i++) {
    const element = content[i];
    if (element.paragraph && element.paragraph.elements) {
      const textRun = element.paragraph.elements[0].textRun;
      if (textRun && textRun.content.trim() === CODIGO_FUENTE_SECTION) {
        return element.endIndex;
      }
    }
  }
  throw new Error(
    `"${CODIGO_FUENTE_SECTION}" section not found in the document.`
  );
}

async function deleteContentAfter(startIndex) {
  const documentLength =
    __DOC__.body.content[__DOC__.body.content.length - 1].endIndex;
  const endIndex = Math.max(startIndex + 1, documentLength - 1);

  await DOCS.documents.batchUpdate({
    documentId: __DOC_ID__,
    requestBody: {
      requests: [
        {
          deleteContentRange: {
            range: {
              startIndex,
              endIndex,
            },
          },
        },
      ],
    },
  });
}

async function updateGoogleDoc() {
  const codigoFuenteIndex = await findCodigoFuenteIndex(__DOC_ID__);
  const documentLengthB4Delete = await getDocLength();

  if (codigoFuenteIndex == documentLengthB4Delete + 1) {
    await deleteContentAfter(codigoFuenteIndex);
  }

  const documentLengthAfterDelete = await getDocLength();

  await pushUpdatesToGDoc(documentLengthAfterDelete);

  console.log(
    `Document updated: https://docs.google.com/document/d/${__DOC_ID__}/edit`
  );
}

async function pushUpdatesToGDoc(initialInsertionIndex) {
  const batchSize = 3;
  let requests = [];
  let insertionIndex = initialInsertionIndex;

  for (let packageIndex = 0; packageIndex < PACKAGES.length; packageIndex++) {
    const packageName = PACKAGES[packageIndex];
    // prettier-ignore
    const pkgHeader = insertTextRequest(`\t${BASE_INDEX}.${packageIndex + 1}. ${packageName}`, insertionIndex);
    requests.push(pkgHeader, formatTextRequest("HEADING_2", insertionIndex));
    insertionIndex += pkgHeader.insertText.text.length;

    const files = getAllFiles(path.join(BASE_DIR, packageName), ".cs");
    for (let fileIndex = 0; fileIndex < files.length; fileIndex++) {
      const filePath = files[fileIndex];
      // prettier-ignore
      const fileName = insertTextRequest(`\t\t${BASE_INDEX}.${packageIndex + 1}.${fileIndex + 1} ${path.basename(filePath)}`, insertionIndex);
      requests.push(fileName, formatTextRequest("HEADING_3", insertionIndex));
      insertionIndex += fileName.insertText.text.length;

      const fContent = insertTextRequest(format(filePath), insertionIndex);
      requests.push(fContent, formatTextRequest("NORMAL_TEXT", insertionIndex));
      insertionIndex += fContent.insertText.text.length;

      if (requests.length >= batchSize) {
        await executeBatch(requests);
        insertionIndex = await getDocLength();
        requests = [];
      }
    }
  }

  if (requests.length > 0) {
    await executeBatch(requests);
  }
}

async function executeBatch(requests) {
  console.log(requests.length);
  await DOCS.documents.batchUpdate({
    documentId: __DOC_ID__,
    requestBody: { requests },
  });
}

async function getDocLength() {
  const doc = await getDoc();
  return doc.body.content[doc.body.content.length - 1].endIndex - 1;
}

function insertTextRequest(text, index) {
  return {
    insertText: {
      location: { index },
      text: text + "\n\n",
    },
  };
}

function formatTextRequest(style, index) {
  return {
    updateParagraphStyle: {
      range: {
        startIndex: index,
        endIndex: index + 1,
      },
      paragraphStyle: {
        namedStyleType: style,
      },
      fields: "namedStyleType",
    },
  };
}

function format(filePath) {
  const fileContent = fs.readFileSync(filePath, "utf-8");
  const relPath = path.relative(BASE_DIR, filePath);
  return `// ${relPath}\n${fileContent}`;
}

function getAllFiles(dir, extension) {
  let results = [];
  fs.readdirSync(dir).forEach((file) => {
    const filePath = path.join(dir, file);
    const stat = fs.statSync(filePath);

    if (stat && stat.isDirectory()) {
      if (!IGNORE_FILES_IN.includes(file)) {
        results = results.concat(getAllFiles(filePath, extension));
      }
    } else if (filePath.endsWith(extension)) {
      results.push(filePath);
    }
  });
  return results;
}

const getDoc = async () => {
  __DOC__ = (await DOCS.documents.get({ documentId: __DOC_ID__ })).data;
  return __DOC__;
};

async function main() {
  const auth = await authenticate();
  DOCS = google.docs({ version: "v1", auth });
  await getDoc();
  await updateGoogleDoc(__DOC_ID__);
}

main();
