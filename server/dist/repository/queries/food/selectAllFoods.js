"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.default = () => {
    return `
    SELECT 
    id 
    , name
    , fodmap_id AS "fodmapId"
    , vegetarian
    , vegan
    , gluten_free AS "glutenFree"
    FROM food;
  `;
};
