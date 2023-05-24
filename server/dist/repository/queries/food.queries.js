"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.getAllIngredients = exports.deleteFood = exports.insertFood = exports.selectAllMatchingFoods = exports.selectAllFoods = void 0;
const selectAllFoods = () => {
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
exports.selectAllFoods = selectAllFoods;
const selectAllMatchingFoods = (name) => {
    return `
    SELECT * FROM food WHERE LOWER(name) LIKE '%${name}%';
  `;
};
exports.selectAllMatchingFoods = selectAllMatchingFoods;
const insertFood = (foodItem) => {
    const { name, fodmapId, vegetarian, vegan, glutenFree } = foodItem;
    const correctedName = name.replace("'", "''");
    // return `
    //   INSERT INTO food(name, fodmap_id, vegetarian, vegan, gluten_free) 
    //   VALUES (
    //     '${correctedName}'
    //     , ${fodmapId}
    //     , ${vegetarian}
    //     , ${vegan}
    //     , ${glutenFree}      
    //   );
    // `;
    return `
    INSERT INTO food(name, fodmap_id, vegetarian, vegan, gluten_free)
    SELECT '${correctedName}'
      , ${fodmapId}
      , ${vegetarian}
      , ${vegan}
      , ${glutenFree} 
    WHERE NOT EXISTS (
      SELECT name FROM food WHERE lower(name) = '${correctedName.toLowerCase()}'
    );
  `;
};
exports.insertFood = insertFood;
const deleteFood = (foodId) => {
    return `
    DELETE FROM food
    WHERE id = ${foodId};
  `;
};
exports.deleteFood = deleteFood;
const getAllIngredients = (foodId) => {
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
exports.getAllIngredients = getAllIngredients;
