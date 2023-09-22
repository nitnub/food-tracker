SELECT 
  fm.id
  , fodmap_category.name AS category
  , fm.name
  , fm.free_use AS "freeUse"
  , fm.oligos
  , fm.fructose
  , fm.polyols
  , fm.lactose
  , fodmap_color.name AS color
  -- , concat(fm.max_use_value, unit.short_name ) AS "maxIntake" 
  , (
      SELECT
        CASE
          WHEN fm.max_use_value = 9999 THEN null
          WHEN fm.max_use_value = 0 THEN concat(fm.max_use_value, unit.short_name_plural) 
          WHEN fm.max_use_value = 1 THEN concat(fm.max_use_value, unit.short_name) 
          WHEN fm.max_use_value > 1 THEN concat(fm.max_use_value, unit.short_name_plural) 
          ELSE null
        END 

      FROM fodmap_main 
      -- WHERE 
      --   max_use_value < 9999 
      -- AND fodmap_main.id = fm.id 
      -- WHERE fodmap_main.id = fm.id 
      WHERE fodmap_main.id = 16
    ) AS "maxIntake" 
FROM fodmap_main fm
JOIN fodmap_color on fm.color = fodmap_color.id
JOIN unit ON fm.max_use_unit = unit.id
JOIN fodmap_category ON fm.category = fodmap_category.id