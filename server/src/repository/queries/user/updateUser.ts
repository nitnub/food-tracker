export default (id: string, userUpdates: UserDbUpdateEntry) => {
  const { modifiedBy, email, admin, avatar, active } = userUpdates;

  const resp = `
    UPDATE app_user SET 
    last_modified_by = '${modifiedBy}'
    ${email ? `, email  = '${email}'` : ''}
    ${avatar ? `, avatar = '${avatar}'` : ''}
    ${typeof admin === 'boolean' ? `, admin  = ${admin}` : ''}
    ${typeof active === 'boolean' ? `, active = ${active}` : ''}
    WHERE id = '${id}';  
    SELECT
      id,
      global_user_id as "globalUserId",
      email,
      admin,
      avatar,
      active,
      created_on as "createdOn",
      last_modified_on as "lastModifiedOn",
      deleted_on as "deletedOn",
      last_modified_by as "lastModifiedBy" 
    FROM app_user WHERE id = '${id}';
  `;

  return resp;
};
