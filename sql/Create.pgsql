-- -- https://stackoverflow.com/questions/39575075/postgresql-trigger-if-one-of-the-columns-have-changed
--   CREATE TABLE IF NOT EXISTS reaction (
--     id SERIAL         PRIMARY KEY, 
--     "user"            INTEGER,
--     food              INTEGER,
--     reaction_type     INTEGER,
--     severity          INTEGER,
--     active            BOOLEAN DEFAULT true,
--     subsided_on       TIMESTAMP WITH TIME ZONE,
--     last_modified_on  TIMESTAMP WITH TIME ZONE DEFAULT now(),
--     identified_on     TIMESTAMP WITH TIME ZONE DEFAULT now(),
--     deleted_on        TIMESTAMP WITH TIME ZONE,
--     unique("user", food, reaction_type)
--   );



  CREATE TABLE IF NOT EXISTS app_user (
    id SERIAL         PRIMARY KEY, 
    global_user_id    TEXT,
    email             TEXT,
    admin             BOOLEAN DEFAULT false,
    avatar            TEXT,
    active            BOOLEAN DEFAULT true,
    created_on        TIMESTAMP WITH TIME ZONE DEFAULT now(),
    last_modified_on  TIMESTAMP WITH TIME ZONE DEFAULT now(),
    last_modified_by  TEXT,
    deleted_on        TIMESTAMP WITH TIME ZONE,
    unique(email)
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

-- Create trigger to apply function 
CREATE OR REPLACE TRIGGER add_log
  BEFORE UPDATE
  ON
      app_user
  FOR EACH ROW
EXECUTE PROCEDURE add_updated_timestamp();