"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.default = (user) => {
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
