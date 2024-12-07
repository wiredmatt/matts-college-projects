#!/usr/bin/env node
const fs = require("fs");
const path = require("path");

const here = __dirname;
const GOOGLE_DOC_ID = process.env.GOOGLE_DOC_ID;
const keyFilePath = path.join(here, "key.json");
const IGNORE_FILES_IN = ["bin", "obj"];

// Base directory for the C# projects
const BASE_DIR = path.join(here, "..", "..");
const PACKAGES = [
  "LibreriaWeb",
  "Compartido",
  "LogicaNegocio",
  "LogicaAplicacion",
  "LogicaAccesoDatos",
];
const BASE_INDEX = 5;

function formatFileContent(filePath) {
  const fileContent = fs.readFileSync(filePath, "utf-8");
  const relPath = path.relative(BASE_DIR, filePath);
  // Add file path as the first line as a comment
  return `// ${relPath}\n${fileContent}`;
}

function generateGoogleDocContent(baseDir, packages) {
  let docContent = "";

  packages.forEach((packageName, packageIndex) => {
    const packagePath = path.join(baseDir, packageName);
    docContent += ` ${BASE_INDEX}.${packageIndex + 1}. ${packageName}\n`; // Package header

    const files = getAllFiles(packagePath, ".cs");
    files.forEach((filePath, fileIndex) => {
      const fileName = path.basename(filePath);
      // prettier-ignore
      docContent += `  ${BASE_INDEX}.${packageIndex + 1}.${fileIndex + 1} ${fileName}\n`;
      docContent += formatFileContent(filePath) + "\n\n";
    });
  });

  return docContent;
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
function main() {
  const docContent = generateGoogleDocContent(BASE_DIR, PACKAGES);

  // Save to file or use Google Docs API
  fs.writeFileSync("output.txt", docContent, "utf-8");
}

main();
