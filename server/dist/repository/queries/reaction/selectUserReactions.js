"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.default = (userId) => {
    return `

    SELECT json_build_object(
      'reactionId', r.id,
      'active', r.active,
      'subsidedOn', r.subsided_on,
      'modifiedOn', r.last_modified_on,
      'identifiedOn', r.identified_on,
      'deletedOn', r.deleted_on,
      'food', (
        SELECT json_build_object(
          'id', f.id,
          'reactionScope', eg.name, 
          'name', f.name,
          'vegetarian', f.vegetarian,
          'vegan', f.vegan,
          'glutenFree', f.gluten_free,
          'fodMap', (
            SELECT json_build_object(
              'id', fm.id,
              'category', fmc.name,
              'categoryId', fmc.name,
              'name', fm.name,
              'freeUse', fm.free_use,
              'oligos', fm.oligos,
              'fructose', fm.fructose,
              'polyols', fm.polyols,
              'lactose', fm.lactose,
              'color', color.name,
              'maxIntake', (
                SELECT
                  CASE
                    WHEN fm.max_use_value = 9999 THEN null
                    WHEN fm.max_use_value = 1 THEN concat(fm.max_use_value, u.short_name)
                    ELSE concat(fm.max_use_value, u.short_name_plural) 
                  END 
                FROM fodmap_main fm
                JOIN unit u ON fm.max_use_unit = u.id
                WHERE f.fodmap_id = fm.id
              )
            )
            FROM fodmap_main fm
            JOIN fodmap_category fmc ON fm.category = fmc.id
            JOIN fodmap_color color ON fm.color = color.id
            WHERE f.fodmap_id = fm.id
          )
        )
        from food f
        JOIN element_group eg ON r.food_grouping_id = eg.id
        WHERE r.element_id = f.id
      ),
      'reaction', (
        SELECT json_build_object(
          'id',r.id,
          'category',rc.name,
          'typeName',rt.name,
          'typeId',rt.id,
          'severityName',rs.name,
          'severityId',rs.id,
          'foodGroupingId', eg.id
        )
      )
    )
  FROM reaction r
  JOIN reaction_type rt ON r.reaction_type = rt.id
  JOIN reaction_category rc ON rt.reaction_category = rc.id
  JOIN reaction_severity rs ON r.severity = rs.id
  JOIN element_group eg ON r.food_grouping_id = eg.id
  WHERE r.user_id = ${userId}
  AND r.deleted_on IS null;
  `;
};
