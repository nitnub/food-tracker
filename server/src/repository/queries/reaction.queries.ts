import { ReactionDbEntry } from "../../types/reaction.types";

export const insertTest = (
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

export const insertReaction = (
 reaction: ReactionDbEntry
) => {
  const query = `
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
        return query
};

export const selectUserReactions = (userId: number) => {
  return `
    SELECT * FROM reaction
    WHERE "user" = ${userId} 
    AND deleted_on IS null
    ORDER BY id ASC;
  `;
};

export const selectActiveUserReactions = (userId: number) => {
  return `
    SELECT * FROM reaction 
    WHERE "user" = ${userId}
    AND active is true 
    AND deleted_on IS null;
  `;
};

export const selectReactionSeverities = () => {
  return `
    SELECT * FROM reaction_severity;
  `;
};

export const selectReactionTypes = () => {
  return `
    SELECT * FROM reaction_type;
  `;
};

export const deleteReaction = (reactionId: number) => {
  //   SET last_modified_on = NOW()
  return `
    UPDATE reaction
    SET deleted_on = now()
  
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