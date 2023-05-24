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

  return `
    INSERT INTO food(name, fodmap_id, vegetarian, vegan, gluten_free) 
    VALUES (
      '${correctedName}'
      , ${fodmapId}
      , ${vegetarian}
      , ${vegan}
      , ${glutenFree}      
    );
  `;
};

export const deleteFood = (foodId: number) => {
  return `
      DELETE FROM food
      WHERE id = ${foodId};
    `;
};
