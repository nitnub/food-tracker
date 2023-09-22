INSERT INTO food(name, fodmap_id, vegetarian, vegan, gluten_free)
SELECT 'Lettuce', null, true, true, true
WHERE
NOT EXISTS (
SELECT name FROM food WHERE lower(name) = 'lettuce'
);

select * from food