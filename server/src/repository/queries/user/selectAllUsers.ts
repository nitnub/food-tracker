export default () => {
  return `
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
    FROM app_user
    ORDER BY id ASC;
  `;
};
