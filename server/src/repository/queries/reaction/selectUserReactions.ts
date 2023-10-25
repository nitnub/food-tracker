export default (userId: number) => {
  // return `
  //   SELECT * FROM reaction
  //   WHERE user_id = ${userId}
  //   AND deleted_on IS null
  //   ORDER BY id ASC;
  // `;
  return `
    SELECT 
      reaction.id
      , reaction.user_id as "userId"
      , reaction.active 
      , reaction.subsided_on AS "subsidedOn"
      , reaction.last_modified_on AS "modifiedOn"
      , reaction.identified_on AS "identifiedOn"
      , reaction.deleted_on AS "deletedOn"
      , reaction_type.name AS "reactionTypeName" 
      , reaction_type.id AS "reactionTypeId" 
      , reaction_category.name AS "reactionCategory"
      , element_group.name AS "reactionScope"
      , element_group.id AS "foodGroupingId"
      , food.id AS "foodId"
      , food.name AS "foodName"
      , food.vegetarian
      , food.vegan
      , food.gluten_free AS "glutenFree"
      , reaction_severity.name AS "severityName"
      , reaction_severity.id AS "severityId"
      , fod.id AS "fodId"
      , fod.category AS "fodCategory"
      , fod.name AS "fodName"
      , fod.free_use AS "fodFreeUse"
      , fod.oligos AS "fodOligos"
      , fod.fructose AS "fodFructose"
      , fod.polyols AS "fodPolyols"
      , fod.lactose AS "fodLactose"
      , fod.color AS "fodColor"
      , fod."maxIntake"
      , fod."maxIntakeTest"
    FROM reaction
      JOIN reaction_type ON reaction.reaction_type = reaction_type.id
      JOIN reaction_category ON reaction_type.reaction_category = reaction_category.id
      JOIN reaction_severity ON reaction.severity = reaction_severity.id
      JOIN food ON reaction.element_id = food.id
      JOIN element_group ON reaction.food_grouping_id = element_group.id
      LEFT JOIN (
        SELECT 
          fm.id
          , fodmap_category.name AS category
          , fm.name
          , fm.free_use
          , fm.oligos
          , fm.fructose
          , fm.polyols
          , fm.lactose
          , fodmap_color.name AS color
          , concat(fm.max_use_value, unit.short_name ) AS "maxIntake" 
          , (
              SELECT
                CASE
                  WHEN fm.max_use_value = 9999 THEN null
                  WHEN fm.max_use_value = 1 THEN concat(fm.max_use_value, unit.short_name)
                  ELSE concat(fm.max_use_value, unit.short_name_plural) 
                END 
              FROM fodmap_main 
              WHERE fodmap_main.id = 16
          ) AS "maxIntakeTest" 
        FROM fodmap_main fm
        JOIN fodmap_color on fm.color = fodmap_color.id
        JOIN unit ON fm.max_use_unit = unit.id
        JOIN fodmap_category ON fm.category = fodmap_category.id
      ) AS fod on food.fodmap_id = fod.id

    WHERE reaction.user_id = ${userId}
      ;

  `;
};

const aa = {
  reactionId: 13,
  active: true,
  subsidedOn: null,
  modifiedOn: '2023-04-17T02:37:01.147433+00:00',
  identifiedOn: '2023-04-08T02:50:32.525914+00:00',
  deletedOn: null,
  food: {
    id: 1,
    reactionScope: 'food',
    name: 'Apple',
    vegetarian: true,
    vegan: true,
    glutenFree: true,
    fodMap: {
      id: 16,
      category: 'Fresh Fruit',
      categoryId: 'Fresh Fruit',
      name: 'apple',
      freeUse: false,
      oligos: false,
      fructose: true,
      polyols: true,
      lactose: false,
      color: 'Red',
      maxIntake: '0g',
    },
  },
  reaction: {
    id: 13,
    category: 'Stomach',
    typeName: 'Vomiting',
    typeId: 2,
    severityName: 'Mild',
    severityId: 2,
    foodGroupingId: 1,
  },
};
