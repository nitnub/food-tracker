  CREATE TABLE IF NOT EXISTS activity (
    id                SERIAL PRIMARY KEY, 
    activity_type     INTEGER, -- fk for activity_type
    duration          INTEGER,
    duration_units    INTEGER, -- fk for unit
    intensity         INTEGER, -- fk for intensity
    location          TEXT,
    event_map         INTEGER, -- fk for event_map
 
    created_on        TIMESTAMP WITH TIME ZONE DEFAULT now(),
    last_modified_on  TIMESTAMP WITH TIME ZONE DEFAULT now(),
    last_modified_by  TEXT,
    deleted_on        TIMESTAMP WITH TIME ZONE,
    CONSTRAINT FK_activity_activity_type
      FOREIGN KEY(activity_type)
        REFERENCES activity_type(id),
    CONSTRAINT FK_activity_unit
      FOREIGN KEY(duration_units)
        REFERENCES unit(id),
    CONSTRAINT FK_activity_intensity
      FOREIGN KEY(intensity)
        REFERENCES intensity(id),
    CONSTRAINT FK_activity_event_map
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
      activity
  FOR EACH ROW
EXECUTE PROCEDURE add_updated_timestamp();