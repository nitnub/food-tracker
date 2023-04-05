"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.updateReactionActive = exports.updateReactionSeverity = exports.updateReactionType = exports.updateReactionTypeAndSeverity = exports.deleteReaction = exports.selectReactionTypes = exports.selectReactionSeverities = exports.selectActiveUserReactions = exports.selectUserReactions = exports.insertReaction = exports.insertTest = void 0;
const insertTest = (porkInt, porkBool, porkText) => {
    return `
    INSERT INTO test_table(pork_int, pork_bool, pork_text) 
    VALUES (
      ${porkInt}
      , ${porkBool}
      , '${porkText}'
    )
  `;
};
exports.insertTest = insertTest;
const insertReaction = (reaction) => {
    const query = `
    INSERT 
      INTO reaction(
        "user"
        , food
        , reaction_type
        ${reaction.severityId && ', severity'}
        ${typeof reaction.active === 'boolean' && ', active'}
        ${(reaction.subsidedOn || reaction.subsidedOn === null) ? `, subsided_on` : ''}
        ${(reaction.deletedOn || reaction.deletedOn === null) ? `, deleted_on` : ''}
      ) 
      VALUES (
        ${reaction.userId}
        , ${reaction.foodId}
        , ${reaction.reactionTypeId}
        ${reaction.severityId && `, ${reaction.severityId}`}
        ${typeof reaction.active === 'boolean' && `, ${reaction.active}`}
        ${(reaction.subsidedOn || reaction.subsidedOn === null) ? `, '${reaction.subsidedOn}'` : ''}
        ${(reaction.deletedOn || reaction.deletedOn === null) ? `, '${reaction.deletedOn}'` : ''}
      )
    ON CONFLICT ("user", food, reaction_type) 
    DO 
      UPDATE SET 
        severity=EXCLUDED.severity
        , active=EXCLUDED.active
        , subsided_on=EXCLUDED.subsided_on
        , deleted_on=EXCLUDED.deleted_on
    ;
  `;
    // console.log(query)
    return query;
};
exports.insertReaction = insertReaction;
const selectUserReactions = (userId) => {
    return `
    SELECT * FROM reaction
    WHERE "user" = ${userId} 
    AND deleted_on IS null
    ORDER BY id ASC;
  `;
};
exports.selectUserReactions = selectUserReactions;
const selectActiveUserReactions = (userId) => {
    return `
    SELECT * FROM reaction 
    WHERE "user" = ${userId}
    AND active is true 
    AND deleted_on IS null;
  `;
};
exports.selectActiveUserReactions = selectActiveUserReactions;
const selectReactionSeverities = () => {
    return `
    SELECT * FROM reaction_severity;
  `;
};
exports.selectReactionSeverities = selectReactionSeverities;
const selectReactionTypes = () => {
    return `
    SELECT * FROM reaction_type;
  `;
};
exports.selectReactionTypes = selectReactionTypes;
const deleteReaction = (reactionId) => {
    //   SET last_modified_on = NOW()
    return `
    UPDATE reaction
    SET deleted_on = now()
  
    WHERE id = ${reactionId};
  `;
};
exports.deleteReaction = deleteReaction;
// export const getReaction = () => {};
// export const updateReaction = () => {}; // what fields to update? active/inactive/change severity / change dates (perhaps onset)?
// export const getReactionByName = () => {};
// export const getReactionByUserId = () => {};
// export const getReactionIfActive = () => {};
const updateReactionTypeAndSeverity = (reactionId, type, severity) => {
    return `
    UPDATE reaction
    SET reaction_type = ${type},
    SET severity = ${severity},
    SET last_modified_on = NOW()
    WHERE id = ${reactionId};
  `;
};
exports.updateReactionTypeAndSeverity = updateReactionTypeAndSeverity;
const updateReactionType = (reactionId, type) => {
    return `
    UPDATE reaction
    SET reaction_type = ${type},
    SET last_modified_on = NOW()
    WHERE id = ${reactionId};
  `;
};
exports.updateReactionType = updateReactionType;
const updateReactionSeverity = (reactionId, severity) => {
    return `
    UPDATE reaction
    SET severity = ${severity},
    SET last_modified_on = NOW()
    WHERE id = ${reactionId};
  `;
};
exports.updateReactionSeverity = updateReactionSeverity;
const updateReactionActive = (reactionId, active) => {
    return `
    UPDATE reaction
    SET active = ${active},
    SET last_modified_on = NOW()
    WHERE id = ${reactionId};
  `;
};
exports.updateReactionActive = updateReactionActive;
