  CREATE TABLE IF NOT EXISTS unit (
    id SERIAL         PRIMARY KEY, 
    short_name        TEXT,
    short_name_plural TEXT,
    long_name         TEXT,
    long_name_plural  TEXT,
    notes             TEXT,
    -- created_on        TIMESTAMP WITH TIME ZONE DEFAULT now(),
    -- last_modified_on  TIMESTAMP WITH TIME ZONE DEFAULT now(),
    -- last_modified_by  TEXT,
    -- deleted_on        TIMESTAMP WITH TIME ZONE,
    unique(short_name, short_name_plural, long_name, long_name_plural)
  );




--   -- FUNCTIONS --

-- CREATE OR REPLACE FUNCTION add_updated_timestamp() 
-- RETURNS TRIGGER AS $$
-- BEGIN
-- --  <> vs != vs distinct  -> https://stackoverflow.com/questions/39575075/postgresql-trigger-if-one-of-the-columns-have-changed
-- --  IF OLD!=NEW THEN
-- IF NEW IS DISTINCT FROM OLD THEN
--   NEW.last_modified_on = now();

--   END IF;
--   RETURN NEW;
-- END;
-- $$ language 'plpgsql';

-- -- TRIGGERS --

-- -- Create trigger to apply function 
-- CREATE OR REPLACE TRIGGER add_log
--   BEFORE UPDATE
--   ON
--       unit
--   FOR EACH ROW
-- EXECUTE PROCEDURE add_updated_timestamp();