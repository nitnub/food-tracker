export default (reactionId: number) => {
  console.log('DELETE ACTION', reactionId);

  return `
    UPDATE reaction
    SET deleted_on = now()
    WHERE id = ${reactionId};
  `;
};
