import { FoodDBObject } from '../../../types/food.types';

export default (foodItem: FoodDBObject) => {
  const { name, fodmapId, vegetarian, vegan, glutenFree } = foodItem;

  const correctedName = name.replace("'", "''");

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
