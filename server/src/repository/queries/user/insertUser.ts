export default (user: UserDbEntry) => {
  return `
    INSERT INTO app_user(
      global_user_id
      , email
      ${user.admin ? ', admin' : ''}
      ${user.avatar ? ', avatar' : ''}
      ${user.active ? ', active' : ''}
    )
    VALUES(
      '${user.globalUserId}'
      , '${user.email}'
      ${user.admin ? `, ${user.admin}` : ''}
      ${user.avatar ? `, '${user.avatar}'` : ''}
      ${user.active ? `, ${user.active}` : ''}
    );
  `;
};
