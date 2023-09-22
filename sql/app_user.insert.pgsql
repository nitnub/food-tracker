INSERT INTO app_user(
  global_user_id,
  email,
  -- admin, -- this is a default field
  avatar
  -- active, -- this is a default field
  -- created_on, -- this is a default field
  -- last_modified_on -- this is a default field
)
VALUES(
  'just-a-test-guid',
  'test@gmail.com',
  -- admin, -- this is a default field
  'www.my-icon-url.com/avatar/123'
  -- active, -- this is a default field
  -- created_on, -- this is a default field
  -- last_modified_on -- this is a default field
);

SELECT * FROM app_user ORDER BY id ASC;