export const selectAllAsObj = () => {
  return `
    SELECT
    json_build_object(
      'id', fm.id,
      'category', fodmap_category.name,
      'name', fm.name,
      'freeUse', fm.free_use,
      'oligos', fm.oligos,
      'fructose', fm.fructose,
      'polyols', fm.polyols,
      'lactose', fm.lactose,
      'color', fodmap_color.name,
      'aliasPrimary', (SELECT "alias" from "fodmap_alias" where is_primary = true AND fodmap_main_id = fm.id),
      'aliasList', (SELECT array_agg(alias) FROM "fodmap_alias" WHERE fodmap_main_id = 38),
      'maxIntake', (
          SELECT
            CASE
              WHEN fm.max_use_value = 9999 THEN null
              WHEN fm.max_use_value = 0 THEN concat(fm.max_use_value, unit.short_name_plural) 
              WHEN fm.max_use_value = 1 THEN concat(fm.max_use_value, unit.short_name) 
              WHEN fm.max_use_value > 1 THEN concat(fm.max_use_value, unit.short_name_plural) 
              ELSE null
            END 
          FROM fodmap_main 
          WHERE fodmap_main.id = fm.id
        ) 
      )
    FROM fodmap_main fm
    JOIN fodmap_color on fm.color = fodmap_color.id
    JOIN unit ON fm.max_use_unit = unit.id
    JOIN fodmap_category ON fm.category = fodmap_category.id;
  `;
};

