export default (userId: number) => {
  return `
    DELETE FROM app_user where id = ${userId};
  `;
};
