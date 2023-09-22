
  CREATE TABLE IF NOT EXISTS event (
    id                SERIAL PRIMARY KEY, 
    event_type        INTEGER,
    time              TIMESTAMP,
    event_map         INTEGER, -- fk to event_map table
    app_user          INTEGER, -- fk to app_user table

    created_on        TIMESTAMP WITH TIME ZONE DEFAULT now(),
    last_modified_on  TIMESTAMP WITH TIME ZONE DEFAULT now(),
    last_modified_by  TEXT,
    deleted_on        TIMESTAMP WITH TIME ZONE,
    CONSTRAINT FK_event_event_type
      FOREIGN KEY(event_type)
          REFERENCES event_type(id),
    CONSTRAINT FK_event_event_map
      FOREIGN KEY(event_map)
          REFERENCES event_map(id),
    CONSTRAINT FK_event_app_user
      FOREIGN KEY(app_user)
          REFERENCES app_user(id)
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
      event
  FOR EACH ROW
EXECUTE PROCEDURE add_updated_timestamp();