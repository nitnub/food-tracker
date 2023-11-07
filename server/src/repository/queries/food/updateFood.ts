import { FoodUpdateObject } from '../../../types/food.types';

export default (id: number, foodUpdate: FoodUpdateObject) => {
  const { name, fodmapId, vegetarian, vegan, glutenFree } = foodUpdate;
  let correctedName = '';

  if (name) {
    correctedName = name.replace("'", "''");
  }

  let setStatement = `${correctedName ? `"name"  = '${correctedName}'` : ''}
    ${typeof fodmapId === 'number' ? `, fodmap_id = ${fodmapId}` : ''}
    ${typeof vegetarian === 'boolean' ? `, vegetarian  = ${vegetarian}` : ''}
    ${typeof vegan === 'boolean' ? `, vegan = ${vegan}` : ''}
    ${
      typeof glutenFree === 'boolean' ? `, gluten_free = ${glutenFree}` : ''
    }`.trim();

  if (setStatement.charAt(0) === ',') {
    setStatement = setStatement.slice(1);
  }

  const resp = `
    UPDATE food 
    SET ${setStatement}
    WHERE id = '${id}';  
    SELECT
      id
      , "name"
      , vegetarian
      , fodmap_id as "fodmapId"
      , vegan
      , gluten_free as "glutenFree"
    FROM food 
   WHERE id = '${id}';
  `;

  return resp;
};
