"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.default = (name) => {
    return `
    SELECT * FROM food WHERE LOWER(name) LIKE '%${name}%';
  `;
};
