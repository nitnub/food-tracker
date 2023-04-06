export const selectAllUsers = () => {
  return `
    SELECT * FROM app_user
    ORDER BY id ASC;
  `;
};

export const selectUser = (userId: number) => {
  return `
    SELECT * FROM app_user WHERE id = ${userId};
  `;
};

export const insertUser = (user: UserDbEntry) => {
  return `
    INSERT INTO app_user(
      global_user_id
      , email
      ${user.admin ? ', admin' : ''}
      ${user.avatar ? ', avatar': ''}
      ${user.active ? ', active': ''}
    

      -- , created_on -- this is a default field
      -- , last_modified_on -- this is a default field
      -- , deleted_on -- this is a default field
    )
    VALUES(
      '${user.globalUserId}'
      , '${user.email}'
      ${user.admin ? `, ${user.admin}`: ''}
      ${user.avatar ? `, '${user.avatar}'`: ''}
      ${user.active ? `, ${user.active}`: ''}
      
      -- , created_on -- this is a default field
      -- , last_modified_on -- this is a default field
      -- , deleted_on -- this is a default field
    );
  `;
};

export const deleteUser = (userId: number) => {
  return `
    DELETE FROM app_user where id = ${userId};
  `
}