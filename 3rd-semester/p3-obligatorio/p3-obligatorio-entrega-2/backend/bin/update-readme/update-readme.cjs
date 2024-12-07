#!/usr/bin/env node

// NOTE(matt): This script is used to update the README file with the generated Mermaid diagram.

const fs = require("fs");
const path = require("path");

const here = __dirname;

const erMmdPath = path.join(here, "..", "..", "_generated", "er.mmd");
const readmePath = path.join(here, "..", "..", "README.md");

// Read the Mermaid file
const mermaidContent = fs.readFileSync(erMmdPath).toString();

// Read the README file
let readmeContent = fs.readFileSync(readmePath).toString();

// Define the regex to match the existing mermaid block (or placeholder if it exists)
const mermaidRegex = /```mermaid[\s\S]*?```/g;

// Create the new mermaid block
const newMermaidBlock = `\`\`\`mermaid\n${mermaidContent}\n\`\`\``;

// Replace the existing Mermaid block with the new one
if (mermaidRegex.test(readmeContent)) {
  readmeContent = readmeContent.replace(mermaidRegex, newMermaidBlock);
} else {
  // If no mermaid block exists, append it to the "## ER Diagram" section
  readmeContent = readmeContent.replace(
    `## ER Diagram`,
    `## ER Diagram\n\n${newMermaidBlock}`
  );
}

// Write the updated README file
fs.writeFileSync(readmePath, readmeContent);

console.log("README updated with Mermaid Diagram");
