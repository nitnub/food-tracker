import { ReactionDbEntry } from "../../types/reaction.types";

export const addTest = (
  porkInt: number,
  porkBool: boolean,
  porkText: string
) => {
  return `
    INSERT INTO test_table(pork_int, pork_bool, pork_text) 
    VALUES (
      ${porkInt}
      , ${porkBool}
      , '${porkText}'
    )
  `;
};

// export const addReaction = (
//   userId: number,
//   foodId: number,
//   reactionTypeId: number,
//   severityId: number,
//   active: boolean = true
// ) => {
export const addReaction = (
 reaction: ReactionDbEntry
) => {

  const hasDelete = (reaction.deletedOn || reaction.deletedOn === null)
  const hasSubside = (reaction.subsidedOn || reaction.subsidedOn === null)
// console.log(`
// INSERT 
//   INTO reaction_two(
//     "user"
//     , food
//     , reaction_type
//     ${reaction.severityId && ', severity'}
//     ${typeof reaction.active === 'boolean'  && ', active'}
//     ${(reaction.subsidedOn || reaction.subsidedOn === null) ? `, subsided_on` : ''}
//     ${(reaction.deletedOn || reaction.deletedOn === null) ? `, deleted_on` : ''}
//     ) 
//   VALUES (
//     ${reaction.userId}
//     , ${reaction.foodId}
//     , ${reaction.reactionTypeId}
//     ${reaction.severityId && `, ${reaction.severityId}`}
//     ${typeof reaction.active === 'boolean'  && `, ${reaction.active}`}
//     ${ hasDelete && `, ${reaction.subsidedOn}`}
//     ${ hasSubside && `, ${reaction.deletedOn}`}
//   )
// ON CONFLICT ("user", food, reaction_type) 
// DO 
//   UPDATE SET 
//     severity=EXCLUDED.severity
//     , active=EXCLUDED.active
//     , subsided_on=EXCLUDED.subsided_on
//     , deleted_on=EXCLUDED.deleted_on
// `)



    // let updateSet = 
    
    // const getUpdateSet = (reaction: ReactionDbEntry) => {

    //   const severityStatement = `severity=EXCLUDED.severity,`
    //   const activeStatement = ` active=EXCLUDED.active,`
    //   const subsidedOnStatement = ` subsided_on=EXCLUDED.subsided_on,`
    //   const deletedOnStatement = ` deleted_on=EXCLUDED.deleted_on,`
    // }

  return`
    INSERT 
      INTO reaction(
        "user"
        , food
        , reaction_type
        ${reaction.severityId && ', severity'}
        ${typeof reaction.active === 'boolean'  && ', active'}
        ${(reaction.subsidedOn || reaction.subsidedOn === null) ? `, subsided_on` : ''}
        ${(reaction.deletedOn || reaction.deletedOn === null) ? `, deleted_on` : ''}
      ) 
      VALUES (
        ${reaction.userId}
        , ${reaction.foodId}
        , ${reaction.reactionTypeId}
        ${reaction.severityId && `, ${reaction.severityId}`}
        ${typeof reaction.active === 'boolean'  && `, ${reaction.active}`}
        ${(reaction.subsidedOn || reaction.subsidedOn === null) ? `, ${reaction.subsidedOn}` : ''}
        ${(reaction.deletedOn || reaction.deletedOn === null) ? `, ${reaction.deletedOn}` : ''}
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


//   INSERT 
//   INTO reaction_two(
//     "user"
//     , food
//     , reaction_type
//     , severity
//     , active
//     , subsided_on
//     , deleted_on) 
//   VALUES (
//     ${userId}
//     , ${foodId}
//     , ${reactionTypeId}
//     , ${severityId}
//     , ${active}
//     , null
//     , null
//   )
// ON CONFLICT ("user", food, reaction_type) 
// DO 
//   UPDATE SET 
//     severity=EXCLUDED.severity
//     , active=EXCLUDED.active
//     , subsided_on=EXCLUDED.subsided_on
//     , deleted_on=EXCLUDED.deleted_on
// `;



  // `
  //   INSERT INTO reaction("user", food, reaction_type, severity, active) 
  //   VALUES (
  //     ${userId}
  //     , ${foodId}
  //     , ${reactionTypeId}
  //     , ${severityId}
  //     , ${active}
  //   );
  // `;
};

export const getAllUserReactions = (userId: number) => {
  return `
    SELECT * FROM reaction
    WHERE "user" = ${userId} 
    AND deleted_on IS null;
  `;
};

export const getActiveUserReactions = (userId: number) => {
  return `
    SELECT * FROM reaction 
    WHERE "user" = ${userId}
    AND active is true 
    AND deleted_on IS null;
  `;
};

export const getReactionSeverities = () => {
  return `
    SELECT * FROM reaction_severity;
  `;
};

export const getReactionTypes = () => {
  return `
    SELECT * FROM reaction_type;
  `;
};

export const deleteReaction = (reactionId: number) => {
  return `
    UPDATE reaction
    SET deleted_on = now(),
    SET last_modified_on = NOW()
    WHERE id = ${reactionId};
  `;
};

// export const getReaction = () => {};
// export const updateReaction = () => {}; // what fields to update? active/inactive/change severity / change dates (perhaps onset)?

// export const getReactionByName = () => {};
// export const getReactionByUserId = () => {};
// export const getReactionIfActive = () => {};

export const updateReactionTypeAndSeverity = (reactionId: number, type: number, severity: number) => {
  return `
    UPDATE reaction
    SET reaction_type = ${type},
    SET severity = ${severity},
    SET last_modified_on = NOW()
    WHERE id = ${reactionId};
  `;
}; 

export const updateReactionType = (reactionId: number, type: number) => {
  return `
    UPDATE reaction
    SET reaction_type = ${type},
    SET last_modified_on = NOW()
    WHERE id = ${reactionId};
  `;
}; 

export const updateReactionSeverity = (reactionId: number, severity: number) => {
  return `
    UPDATE reaction
    SET severity = ${severity},
    SET last_modified_on = NOW()
    WHERE id = ${reactionId};
  `;
}; 

export const updateReactionActive = (reactionId: number, active: boolean) => {
  return `
    UPDATE reaction
    SET active = ${active},
    SET last_modified_on = NOW()
    WHERE id = ${reactionId};
  `;
}; 