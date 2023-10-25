export default (userId: number) => {
  return `
    SELECT * FROM reaction 
    WHERE user_id = ${userId}
    AND active is true 
    AND deleted_on IS null;
  `;
};
