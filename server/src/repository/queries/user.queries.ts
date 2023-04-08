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

export const selectUserByEmail = (email: string) => {
  return `
    SELECT * FROM app_user WHERE lower(email) = '${email.toLowerCase()}';
  `;
};

export const insertUser = (user: UserDbEntry) => {
  return `
    INSERT INTO app_user(
      global_user_id
      , email
      ${user.admin ? ', admin' : ''}
      ${user.avatar ? ', avatar' : ''}
      ${user.active ? ', active' : ''}
    

      -- , created_on -- this is a default field
      -- , last_modified_on -- this is a default field
      -- , deleted_on -- this is a default field
    )
    VALUES(
      '${user.globalUserId}'
      , '${user.email}'
      ${user.admin ? `, ${user.admin}` : ''}
      ${user.avatar ? `, '${user.avatar}'` : ''}
      ${user.active ? `, ${user.active}` : ''}
      
      -- , created_on -- this is a default field
      -- , last_modified_on -- this is a default field
      -- , deleted_on -- this is a default field
    );
  `;
};

export const updateUserByGlobalId = (
  globalUserId: string,
  userUpdates: UserDbUpdateEntry
) => {
  const { modifiedBy, email, admin, avatar, active } = userUpdates;
  console.log('userUpdates:', userUpdates);
  const resp = `
    UPDATE app_user SET 
    last_modified_by = '${modifiedBy}'
    ${email ? `, email  = '${email}'` : ''}
    ${avatar ? `, avatar = '${avatar}'` : ''}
    ${typeof admin === 'boolean' ? `, admin  = ${admin}` : ''}
    ${typeof active === 'boolean' ? `, active = ${active}` : ''}
    WHERE global_user_id = '${globalUserId}';  

    SELECT * FROM app_user WHERE global_user_id = '${globalUserId}';
  `;
  console.log(resp);
  return resp;
};
// Update to modify date; not remove entirely(?)
export const deleteUser = (userId: number) => {
  return `
    DELETE FROM app_user where id = ${userId};
  `;
};
