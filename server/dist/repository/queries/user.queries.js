"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.deleteUser = exports.updateUserByGlobalId = exports.insertUser = exports.selectUserByEmail = exports.selectUser = exports.selectAllUsers = void 0;
const selectAllUsers = () => {
    return `
    SELECT * FROM app_user
    ORDER BY id ASC;
  `;
};
exports.selectAllUsers = selectAllUsers;
const selectUser = (userId) => {
    return `
    SELECT * FROM app_user WHERE id = ${userId};
  `;
};
exports.selectUser = selectUser;
const selectUserByEmail = (email) => {
    return `
    SELECT * FROM app_user WHERE lower(email) = '${email.toLowerCase()}';
  `;
};
exports.selectUserByEmail = selectUserByEmail;
const insertUser = (user) => {
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
exports.insertUser = insertUser;
const updateUserByGlobalId = (globalUserId, userUpdates) => {
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
exports.updateUserByGlobalId = updateUserByGlobalId;
// Update to modify date; not remove entirely(?)
const deleteUser = (userId) => {
    return `
    DELETE FROM app_user where id = ${userId};
  `;
};
exports.deleteUser = deleteUser;
