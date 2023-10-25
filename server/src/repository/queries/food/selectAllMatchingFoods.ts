export default (name: string) => {
  return `
    SELECT * FROM food WHERE LOWER(name) LIKE '%${name}%';
  `;
};
