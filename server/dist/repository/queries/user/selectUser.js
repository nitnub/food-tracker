"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.default = (userId) => {
    return `
    SELECT * FROM app_user WHERE id = ${userId};
  `;
};
