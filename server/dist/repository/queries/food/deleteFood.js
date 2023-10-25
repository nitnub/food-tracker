"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.default = (foodId) => {
    return `
    DELETE FROM food
    WHERE id = ${foodId};
  `;
};
