"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.deleteUser = exports.insertUser = exports.selectUser = exports.selectAllUsers = void 0;
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
const deleteUser = (userId) => {
    return `
    DELETE FROM app_user where id = ${userId};
  `;
};
exports.deleteUser = deleteUser;
