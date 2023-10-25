export default () => {
  return `
    SELECT * FROM reaction_severity;
    SELECT id, "name" FROM reaction_category;
    SELECT id, "name", reaction_category AS "reactionCategory" FROM reaction_type;
  `;
};
