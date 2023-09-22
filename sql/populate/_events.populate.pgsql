INSERT INTO activity_type (name)
VALUES
  ('hike'),
  ('walk'),
  ('cycling'),
  ('mountain biking'),
  ('yoga')
;

INSERT INTO intensity (name)
VALUES
  ('Mild'),
  ('Intermediate'),
  ('Intense')
;

INSERT INTO event_type (name)
VALUES
  ('Meal'),
  ('Activity')
;

INSERT INTO event_map (id)
VALUES
  (DEFAULT),
  (DEFAULT),
  (DEFAULT),
  (DEFAULT)
;






INSERT INTO event (event_type, time, event_map, app_user)
VALUES
  (1, NULL, 1, 88 ),
  (1, NULL, 2, 88 ),
  (2, NULL, 3, 88 ),
  (2, NULL, 4, 77 )
;


INSERT INTO meal_item (food, volume, volume_units, event_map)
VALUES
  (4, 1, 12, 1),
  (10, 1, 12, 1),
  (11, 1, 12, 1),
  (5, 1, 12, 1),
  (6, 1, 12, 1),
  (7, 1, 12, 1),
  (9, 1, 12, 1),
  (10, 1, 12, 1),
  (2, 1, 12, 2)
;

INSERT INTO activity (activity_type, duration, duration_units, 
 intensity, location)
VALUES
  (1, 2, 15, 2, 'Mt. Sai' ),
  (5, 30, 14, 1, 'Olympic Yoga')
;




SELECT * FROM intensity;