export default (foodId: number) => {
  return `
    DELETE FROM food
    WHERE id = ${foodId};
  `;
};
