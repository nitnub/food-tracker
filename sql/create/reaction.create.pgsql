-- https://stackoverflow.com/questions/39575075/postgresql-trigger-if-one-of-the-columns-have-changed
  CREATE TABLE IF NOT EXISTS reaction (
    id SERIAL         PRIMARY KEY, 
    user_id           INTEGER,
    element_id      INTEGER,
    food_grouping_id          INTEGER,
    reaction_type     INTEGER,
    severity          INTEGER,
    active            BOOLEAN DEFAULT true,
    subsided_on       TIMESTAMP WITH TIME ZONE,
    last_modified_on  TIMESTAMP WITH TIME ZONE DEFAULT now(),
    identified_on     TIMESTAMP WITH TIME ZONE DEFAULT now(),
    deleted_on        TIMESTAMP WITH TIME ZONE,
    unique(user_id, element_id, food_grouping_id, reaction_type)
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
      reaction
  FOR EACH ROW
EXECUTE PROCEDURE add_updated_timestamp();

