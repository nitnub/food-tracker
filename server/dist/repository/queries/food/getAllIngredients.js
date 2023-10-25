"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.default = (foodId) => {
    return `
    WITH RECURSIVE subordinates AS (
      SELECT
        parent_id,
        ingredient_food_id
      FROM
        ingredient
      WHERE
        parent_id = ${foodId}
      UNION
        SELECT
          i.parent_id,
          i.ingredient_food_id
        FROM
          ingredient i
        INNER JOIN subordinates s ON s.ingredient_food_id = i.parent_id
    ) SELECT
      *
    FROM subordinates;
  `;
};
