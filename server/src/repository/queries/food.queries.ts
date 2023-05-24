import { FoodDBObject } from '../../types/food.types';

export const selectAllFoods = () => {
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

export const selectAllMatchingFoods = (name: string) => {
  return `
    SELECT * FROM food WHERE LOWER(name) LIKE '%${name}%';
  `;
};

export const insertFood = (foodItem: FoodDBObject) => {
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
  `
};

export const deleteFood = (foodId: number) => {
  return `
    DELETE FROM food
    WHERE id = ${foodId};
  `;
};

export const getAllIngredients = (foodId: number) => {
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
