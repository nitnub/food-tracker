"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.default = (userId) => {
    return `
    SELECT * FROM reaction 
    WHERE user_id = ${userId}
    AND active is true 
    AND deleted_on IS null;
  `;
};
