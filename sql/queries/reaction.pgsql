SELECT 
reaction.id
, reaction.user_id as "userId"
, reaction.active 
, reaction.subsided_on AS "subsidedOn"
-- , reaction.last_modified_by -- does not exist
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
JOIN food ON reaction.element_id = food.id --WHERE reaction.food_grouping_id = 1 -- Food type for grouping 1
JOIN element_group ON reaction.food_grouping_id = element_group.id
-- LEFT JOIN fodmap_main ON food.fodmap_id = -- fodmap_main.id
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
      -- SELECT  concat(fm.max_use_value, unit.short_name  ) 
      -- FROM fodmap_main 
      -- WHERE 
      --   max_use_value < 9999 
      -- AND fodmap_main.id = fm.id 
      -- SELECT  concat(fm.max_use_value, unit.short_name  ) 
      -- SELECT  concat(fm.max_use_value, unit.short_name  ) ,
      SELECT
        CASE
          WHEN fm.max_use_value = 9999 THEN null
          WHEN fm.max_use_value = 1 THEN concat(fm.max_use_value, unit.short_name)
          -- WHEN fm.max_use_value = 0 THEN concat(fm.max_use_value, unit.short_name_plural) 
          -- WHEN fm.max_use_value > 1 THEN concat(fm.max_use_value, unit.short_name_plural) 
          -- ELSE null
          ELSE concat(fm.max_use_value, unit.short_name_plural) 
      END 
      -- AS "max"
      FROM fodmap_main 
      -- WHERE 
      --   max_use_value < 9999 
      -- AND fodmap_main.id = fm.id 
      -- WHERE fodmap_main.id = fm.id 
      WHERE fodmap_main.id = 16
    ) AS "maxIntakeTest" 
FROM fodmap_main fm
JOIN fodmap_color on fm.color = fodmap_color.id
JOIN unit ON fm.max_use_unit = unit.id
JOIN fodmap_category ON fm.category = fodmap_category.id
-- where fodmap_color.name = 'Red'
-- WHERE fm.id = food.fodmap_id
-- ORDER BY fodmap_category.name
) AS fod on food.fodmap_id = fod.id

WHERE reaction.user_id = 202
;




-- select * from fodmap_main











-- select * from app_user;
-- (
-- SELECT 
--   fm.id
--   , fodmap_category.name AS category
--   , fm.name
--   -- , fm.color
--   , fm.free_use AS "freeUse"
--   -- , fm.max_use_value
--   -- , fm.max_use_unit
--   -- , fm.category
--   , fm.oligos
--   , fm.fructose
--   , fm.polyols
--   , fm.lactose
--   , fodmap_color.name AS color
--   -- , concat(fm.max_use_value, unit.short_name ) AS "maxIntake" 
--   , (
--       SELECT  concat(fm.max_use_value, unit.short_name  ) 
--       FROM fodmap_main 
--       WHERE 
--         max_use_value < 9999 
--       AND fodmap_main.id = fm.id 
--     ) AS "maxIntakeTest" 
-- FROM fodmap_main fm
-- JOIN fodmap_color on fm.color = fodmap_color.id
-- JOIN unit ON fm.max_use_unit = unit.id
-- JOIN fodmap_category ON fm.category = fodmap_category.id
-- -- where fodmap_color.name = 'Red'
-- WHERE fm.id = food.fodmap_id
-- -- ORDER BY fodmap_category.name
-- )
-- -- select * from test_table;
