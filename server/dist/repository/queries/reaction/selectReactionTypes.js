"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.default = () => {
    return `
    SELECT 
      rt.id
      , rt.name
      , rc.id AS "categoryId"
      , rc.name AS "categoryName"
    FROM reaction_type rt
    JOIN reaction_category rc ON rt.reaction_category = rc.id;
  `;
};
