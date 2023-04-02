import { FoodDBObject } from '../../types/food.types';

export const getAllFoods = () => {
  return `
    SELECT * FROM food;
  `;
};

export const getAllMatchingFoods = (name: string) => {
  return `
    SELECT * FROM food WHERE LOWER(name) LIKE '%${name}%';
  `;
};

export const addFood = (foodItem: FoodDBObject) => {
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

export const deleteFood = (foodId: number) => {
  return `
      DELETE FROM food
      WHERE id = ${foodId};
    `;
};
