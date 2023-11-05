export default (reactionId: number) => {
  return `
    UPDATE reaction
    SET deleted_on = now()
    WHERE id = ${reactionId};
  `;
};
