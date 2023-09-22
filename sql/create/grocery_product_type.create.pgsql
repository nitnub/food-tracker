
  CREATE TABLE IF NOT EXISTS product_type (
    id SERIAL         PRIMARY KEY, 
    name              TEXT,
    refrigerate       BOOLEAN DEFAULT false,
    non_grocery       BOOLEAN DEFAULT false,
    section           INTEGER,
    -- global_user_id    TEXT,
    -- email             TEXT,
    -- admin             BOOLEAN DEFAULT false,
    -- avatar            TEXT,
    -- active            BOOLEAN DEFAULT true,
    created_on        TIMESTAMP WITH TIME ZONE DEFAULT now(),
    last_modified_on  TIMESTAMP WITH TIME ZONE DEFAULT now(),
    last_modified_by  TEXT,
    deleted_on        TIMESTAMP WITH TIME ZONE,
    unique(name)
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
      product_type
  FOR EACH ROW
EXECUTE PROCEDURE add_updated_timestamp();




