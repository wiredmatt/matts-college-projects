#!/usr/bin/env node
const fs = require("fs");
const path = require("path");

const here = __dirname;
const IGNORE_FILES_IN = ["bin", "obj"];

const ROOT = path.join(here, "..");

const SOLUTIONS = {
    backend: "backend",
    frontend: "frontend",
}
const PACKAGES = {
  backend: [
    "WebApi",
    "Compartido",
    "LogicaNegocio",
    "LogicaAplicacion",
    "LogicaAccesoDatos",
  ],
  frontend: ["WebApp"],
};
const BASE_INDEX = {
  backend: 6,
  frontend: 7,
};

function formatFileContent(filePath) {
  const fileContent = fs.readFileSync(filePath, "utf-8");
  const relPath = path.relative(ROOT, filePath);
  // Add file path as the first line as a comment
  const content = `\n// ${relPath}\n\n${fileContent}`;

  const md = `\`\`\`c#\n${content}\n\`\`\``;

  return md;
}

function generateMdContent(solution) {
    let docContent = "";
    let packages = PACKAGES[solution];
  
    // margin: 0 tab
    docContent += `# ${BASE_INDEX[solution]}. Código Fuente - ${solution}\n\n\n\n`;
  
    packages.forEach((packageName, packageIndex) => {
      const packagePath = path.join(ROOT, solution, packageName);
      const packageLevel = `${BASE_INDEX[solution]}.${packageIndex + 1}`;
      // margin: 1 tab
      docContent += `## ${packageLevel}. ${packageName}\n\n\n\n`;
  
      const files = getAllFiles(packagePath, ".cs");
      files.forEach((filePath, fileIndex) => {
        const fileName = path.basename(filePath);
        const fileLevel = `${packageLevel}.${fileIndex + 1}`;
        // margin: 2 tab
        docContent += `### ${fileLevel}. ${fileName}\n\n\n\n`;
        // margin: 0 tab
        docContent += formatFileContent(filePath) + "\n\n" ;
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
let docContent = ``;

    Object.keys(SOLUTIONS).forEach((solution) => {
        docContent += generateMdContent(solution);
    });

    docContent += "\n";

    fs.writeFileSync("output.md", docContent, "utf-8");
}

main();
