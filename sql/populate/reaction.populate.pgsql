-- INSERT INTO public.reaction_old(
-- 	id, user_id, food_id, category_id, food, reaction_type, severity, active, subsided_on, last_modified_on, identified_on, deleted_on)
-- 	VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?);


-- INSERT INTO reaction(
-- 	id, user_id, element_id, food_grouping_id, reaction_type, severity, active, subsided_on, last_modified_on, identified_on, deleted_on)
-- 	VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?);

INSERT INTO reaction(
	user_id, element_id, food_grouping_id, reaction_type, severity)
	VALUES 
    (202, 2, 1, ?, ?, ?, ?, ?, ?, ?, ?),
    (333, 1, 1, ?, ?, ?, ?, ?, ?, ?, ?),

;