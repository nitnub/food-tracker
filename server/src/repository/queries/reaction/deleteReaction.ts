export default (reactionId: number) => {
  //   SET last_modified_on = NOW()
  return `
    UPDATE reaction
    SET deleted_on = now()
    WHERE id = ${reactionId};
  `;
};
