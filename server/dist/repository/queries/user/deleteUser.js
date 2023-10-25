"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.default = (userId) => {
    return `
    DELETE FROM app_user where id = ${userId};
  `;
};
