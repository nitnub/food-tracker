"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.default = (globalUserId, userUpdates) => {
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
    return resp;
};
