"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.deleteFood = exports.addFood = exports.getAllMatchingFoods = exports.getAllFoods = void 0;
const getAllFoods = () => {
    return `
    SELECT * FROM food;
  `;
};
exports.getAllFoods = getAllFoods;
const getAllMatchingFoods = (name) => {
    return `
    SELECT * FROM food WHERE LOWER(name) LIKE '%${name}%';
  `;
};
exports.getAllMatchingFoods = getAllMatchingFoods;
const addFood = (foodItem) => {
    const { name, fodmapId, vegetarian, vegan, glutenFree } = foodItem;
    return `
    INSERT INTO food(name, fodmap_id, vegetarian, vegan, gluten_free) 
    VALUES (
      '${name}'
      , ${fodmapId}
      , ${vegetarian}
      , ${vegan}
      , ${glutenFree}      
    );
  `;
};
exports.addFood = addFood;
const deleteFood = (foodId) => {
    return `
      DELETE FROM food
      WHERE id = ${foodId};
    `;
};
exports.deleteFood = deleteFood;
