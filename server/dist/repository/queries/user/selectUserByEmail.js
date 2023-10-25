"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.default = (email) => {
    return `
    SELECT * FROM app_user WHERE lower(email) = '${email.toLowerCase()}';
  `;
};
