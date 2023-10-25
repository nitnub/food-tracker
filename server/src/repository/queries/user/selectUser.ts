export default (userId: number) => {
  return `
    SELECT * FROM app_user WHERE id = ${userId};
  `;
};
