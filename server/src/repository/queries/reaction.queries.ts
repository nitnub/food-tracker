import { ReactionDbEntry } from '../../types/reaction.types';

export const insertTest = (
  porkInt: number,
  porkBool: boolean,
  porkText: string
) => {
  return `
    INSERT INTO test_table(pork_int, pork_bool, pork_text) 
    VALUES (
      ${porkInt}
      , ${porkBool}
      , '${porkText}'
    )
  `;
};

export const insertReaction = (reaction: ReactionDbEntry) => {
  const query = `
    INSERT 
      INTO reaction(
        user_id
        , element_id
        , food_grouping_id
        , reaction_type
        ${reaction.severityId && ', severity'}
        ${typeof reaction.active === 'boolean' ? ', active' : ''}
        ${
          reaction.subsidedOn || reaction.subsidedOn === null
            ? `, subsided_on`
            : ''
        }
        ${
          reaction.deletedOn || reaction.deletedOn === null
            ? `, deleted_on`
            : ''
        }
      ) 
      VALUES (
        ${reaction.userId}
        , ${reaction.elementId}
        , ${reaction.foodGroupingId ? `, ${reaction.foodGroupingId}` : 1}
        , ${reaction.reactionTypeId}
        ${reaction.severityId && `, ${reaction.severityId}`}
        ${typeof reaction.active === 'boolean' ? `, ${reaction.active}` : ''}
        ${
          reaction.subsidedOn || reaction.subsidedOn === null
            ? `, '${reaction.subsidedOn}'`
            : ''
        }
        ${
          reaction.deletedOn || reaction.deletedOn === null
            ? `, '${reaction.deletedOn}'`
            : ''
        }
      )
    ON CONFLICT (user_id, element_id, food_grouping_id, reaction_type) 
    DO 
      UPDATE SET 
        severity=EXCLUDED.severity
        , active=EXCLUDED.active
        , subsided_on=EXCLUDED.subsided_on
        , deleted_on=EXCLUDED.deleted_on
    RETURNING 
   --   id
   --   , user_id AS "userId"
   --   , element_id AS "elementId"
   --   , food_grouping_id AS "foodGroupingId"
   --   , reaction_type AS "reactionType"
   --   , severity AS ""
   --   , active AS ""
   --   , subsided_on AS ""
   --   , last_modified_on AS ""
   --   , identified_on AS ""
   --   , deleted_on AS "",     
   -- user_id AS "userId";
  `;
  console.log(query)
  return query;
};
export const insertReactionWithFormattedReturn = (reaction: ReactionDbEntry) => {
  const query = `
  
    INSERT 
      INTO reaction(
        user_id
        , element_id
        , food_grouping_id
        , reaction_type
        ${reaction.severityId && ', severity'}
        ${typeof reaction.active === 'boolean' ? ', active' : ''}
        ${
          reaction.subsidedOn || reaction.subsidedOn === null
            ? `, subsided_on`
            : ''
        }
        ${
          reaction.deletedOn || reaction.deletedOn === null
            ? `, deleted_on`
            : ''
        }
      ) 
      VALUES (
        ${reaction.userId}
        , ${reaction.elementId}
        , ${reaction.foodGroupingId ? `, ${reaction.foodGroupingId}` : 1}
        , ${reaction.reactionTypeId}
        ${reaction.severityId && `, ${reaction.severityId}`}
        ${typeof reaction.active === 'boolean' ? `, ${reaction.active}` : ''}
        ${
          reaction.subsidedOn || reaction.subsidedOn === null
            ? `, '${reaction.subsidedOn}'`
            : ''
        }
        ${
          reaction.deletedOn || reaction.deletedOn === null
            ? `, '${reaction.deletedOn}'`
            : ''
        }
      )
      ON CONFLICT (user_id, element_id, food_grouping_id, reaction_type)
    DO
      UPDATE SET
        severity=EXCLUDED.severity
        , active=EXCLUDED.active
        , subsided_on=EXCLUDED.subsided_on
        , deleted_on=EXCLUDED.deleted_on
 ;
        
 SELECT 
  reaction.id
  , reaction.user_id as "userId"
  , reaction.active 
  , reaction.subsided_on AS "subsidedOn"
  , reaction.last_modified_on AS "modifiedOn"
  , reaction.identified_on AS "identifiedOn"
  , reaction.deleted_on AS "deletedOn"
  , reaction_type.name AS "reactionType" 
  , reaction_category.name AS "reactionCategory"
  , element_group.name AS "reactionScope"
  , food.id AS "foodId"
  , food.name AS "foodName"
  , food.vegetarian
  , food.vegan
  , food.gluten_free AS "glutenFree"
  , reaction_severity.name AS "reactionSeverity"
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

    WHERE user_id = ${reaction.userId}
    AND element_id = ${reaction.elementId}

    
    AND food_grouping_id = ${reaction.foodGroupingId ? `, ${reaction.foodGroupingId}` : 1}
    AND reaction_type = ${reaction.reactionTypeId}
    ${reaction.severityId && `AND severity = ${reaction.severityId}`}
    
  --  ${typeof reaction.active === 'boolean' ? `AND active = ${reaction.active}` : ''}
    
;






  `;
  // console.log(query)
  return query;
};

export const selectUserReactions = (userId: number) => {
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
      , reaction_type.name AS "reactionType" 
      , reaction_category.name AS "reactionCategory"
      , element_group.name AS "reactionScope"
      , food.id AS "foodId"
      , food.name AS "foodName"
      , food.vegetarian
      , food.vegan
      , food.gluten_free AS "glutenFree"
      , reaction_severity.name AS "reactionSeverity"
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

export const selectActiveUserReactions = (userId: number) => {
  return `
    SELECT * FROM reaction 
    WHERE user_id = ${userId}
    AND active is true 
    AND deleted_on IS null;
  `;
};

export const selectReactionSeverities = () => {
  return `
    SELECT * FROM reaction_severity;
  `;
};

export const selectReactionTypes = () => {
  return `
 
    SELECT 
      rt.id
      , rt.name
      , rc.id AS "categoryId"
      , rc.name AS "categoryName"
    FROM reaction_type rt
    JOIN reaction_category rc ON rt.reaction_category = rc.id;
  `;
};
export const selectReactionSeveritiesAndTypes = () => {
  return `
    SELECT * FROM reaction_severity;
    SELECT id, "name" FROM reaction_category;
    SELECT id, "name", reaction_category AS "reactionCategory" FROM reaction_type;
  `;
};

export const selectReactionCategories = () => {
  return `
    SELECT * FROM reaction_category;
  `;
};

export const deleteReaction = (reactionId: number) => {
  //   SET last_modified_on = NOW()
  return `
    UPDATE reaction
    SET deleted_on = now()
  
    WHERE id = ${reactionId};
  `;
};

// export const getReaction = () => {};
// export const updateReaction = () => {}; // what fields to update? active/inactive/change severity / change dates (perhaps onset)?

// export const getReactionByName = () => {};
// export const getReactionByUserId = () => {};
// export const getReactionIfActive = () => {};

export const updateReactionTypeAndSeverity = (
  reactionId: number,
  type: number,
  severity: number
) => {
  return `
    UPDATE reaction
    SET reaction_type = ${type},
    SET severity = ${severity},
    SET last_modified_on = NOW()
    WHERE id = ${reactionId};
  `;
};

export const updateReactionType = (reactionId: number, type: number) => {
  return `
    UPDATE reaction
    SET reaction_type = ${type},
    SET last_modified_on = NOW()
    WHERE id = ${reactionId};
  `;
};

export const updateReactionSeverity = (
  reactionId: number,
  severity: number
) => {
  return `
    UPDATE reaction
    SET severity = ${severity},
    SET last_modified_on = NOW()
    WHERE id = ${reactionId};
  `;
};

export const updateReactionActive = (reactionId: number, active: boolean) => {
  return `
    UPDATE reaction
    SET active = ${active},
    SET last_modified_on = NOW()
    WHERE id = ${reactionId};
  `;
};
