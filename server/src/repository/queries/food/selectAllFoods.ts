export default () => {
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
