-- select * from fodmap_alias order by fodmap_main_id asc;
-- select  distinct original from fodmap_alias;

select * from fodmap_main;

select * from food;


-- -- GET FULL DEPTH INGREDIENT REACTIONS - RAW (NOT FORMATTED)
-- SELECT * 
-- FROM food 
-- JOIN reaction ON reaction.element_id = food.id 
-- WHERE reaction.user_id = 202 
-- AND food.id IN (
--   WITH RECURSIVE full_depth_ingredients AS (
--     SELECT
--       ingredient_food_id
--     FROM
--       ingredient
--     WHERE
--       parent_id = 83
--     UNION
--       SELECT
--         i.ingredient_food_id
--       FROM
--         ingredient i
--       INNER JOIN full_depth_ingredients fdi ON fdi.ingredient_food_id = i.parent_id
--   ) SELECT
--     *
--   FROM full_depth_ingredients
-- );






-- -- GET FULL DEPTH INGREDIENT IDS - RAW (NOT FORMATTED)
-- WITH RECURSIVE subordinates AS (
-- 	SELECT
--     parent_id,
--     ingredient_food_id
-- 	FROM
--     ingredient
-- 	WHERE
--     parent_id = 83
-- 	UNION
-- 		SELECT
--       i.parent_id,
--       i.ingredient_food_id
--     FROM
--       ingredient i
-- 		INNER JOIN subordinates s ON s.ingredient_food_id = i.parent_id
-- ) SELECT
-- 	*
-- FROM subordinates;




-- SELECT * FROM reaction;
-- SELECT * FROM reaction
-- WHERE user_id = 202;

-- SELECT * FROM element_group;

-- WITH RECURSIVE subordinates AS (
-- 	SELECT
-- 		-- employee_id,
-- 		-- manager_id,
-- 		-- full_name
--     id,
--     name
-- 	FROM
-- 		-- employees
--     food f
-- 	WHERE
-- 		-- employee_id = 2
--     id = 83
-- 	UNION
-- 		SELECT
-- 			-- e.employee_id,
-- 			-- e.manager_id,
-- 			-- e.full_name
--       -- i.parent_id,
--       -- i.ingredient_food_id
--       f.id,
--       f.name
--     FROM
-- 			-- employees e
--       food f,
--       ingredient

--       -- INNER JOIN ingredient i on f.id = i.parent_id
      
--       -- ingredient i
-- 		-- INNER JOIN subordinates s ON s.employee_id = e.manager_id
-- 		-- INNER JOIN subordinates s ON s.id = i.ingredient_food_id
-- 		-- INNER JOIN subordinates s ON s.id = f.id
-- 		INNER JOIN subordinates s ON s.id = ingredient.parent_id
-- ) SELECT
-- 	*
-- FROM subordinates;



-- WITH RECURSIVE subordinates AS (
-- 	SELECT
-- 		-- employee_id,
-- 		-- manager_id,
-- 		-- full_name
--     -- id,
--     -- name
--     parent_id,
--     ingredient_food_id
-- 	FROM
-- 		-- employees
--     -- food f
--     ingredient
-- 	WHERE
-- 		-- employee_id = 2
--     -- id = 83
--     parent_id = 83
-- 	UNION
-- 		SELECT
-- 			-- e.employee_id,
-- 			-- e.manager_id,
-- 			-- e.full_name
--       -- i.parent_id,
--       -- i.ingredient_food_id
--       -- f.id,
--       -- f.name
--       i.parent_id,
--       i.ingredient_food_id
--     FROM
-- 			-- employees e
--       -- food f,
--       ingredient i

--       -- INNER JOIN ingredient i on f.id = i.parent_id
      
--       -- ingredient i
-- 		-- INNER JOIN subordinates s ON s.employee_id = e.manager_id
-- 		-- INNER JOIN subordinates s ON s.id = i.ingredient_food_id
-- 		-- INNER JOIN subordinates s ON s.id = f.id
-- 		INNER JOIN subordinates s ON s.ingredient_food_id = i.parent_id
-- ) SELECT
-- 	*
-- FROM subordinates;