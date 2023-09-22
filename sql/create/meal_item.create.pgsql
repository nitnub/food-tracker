  CREATE TABLE IF NOT EXISTS meal_item (
    id SERIAL         PRIMARY KEY, 
    food              INTEGER, -- fk to food table
    volume            INTEGER,
    volume_units      INTEGER, --fk to units
    event_map         INTEGER, --fk to event_map
   
    created_on        TIMESTAMP WITH TIME ZONE DEFAULT now(),
    last_modified_on  TIMESTAMP WITH TIME ZONE DEFAULT now(),
    last_modified_by  TEXT,
    deleted_on        TIMESTAMP WITH TIME ZONE,
    CONSTRAINT FK_meal_item_food
      FOREIGN KEY(food)
          REFERENCES food(id),
    CONSTRAINT FK_meal_item_unit
      FOREIGN KEY(volume_units)
          REFERENCES unit(id),
    CONSTRAINT FK_meal_item_event_map
      FOREIGN KEY(event_map)
          REFERENCES event_map(id)
  );


  
-- Create Function

CREATE OR REPLACE FUNCTION add_updated_timestamp() 
RETURNS TRIGGER AS $$
BEGIN
--  <> vs != vs distinct  -> https://stackoverflow.com/questions/39575075/postgresql-trigger-if-one-of-the-columns-have-changed
--  IF OLD!=NEW THEN
IF NEW IS DISTINCT FROM OLD THEN
  NEW.last_modified_on = now();

  END IF;
  RETURN NEW;
END;
$$ language 'plpgsql';


-- TRIGGERS --

-- Create trigger to apply function 
CREATE OR REPLACE TRIGGER add_log
  BEFORE UPDATE
  ON
      meal_item
  FOR EACH ROW
EXECUTE PROCEDURE add_updated_timestamp();