import { ReactionDbEntry } from '../../../types/reaction.types';

export default (reaction: ReactionDbEntry) => {
  const query = `
    INSERT 
      INTO reaction(
        user_id
        , element_id
        , food_grouping_id
        , reaction_type
        ${reaction.severityId && ', severity'}
        ${typeof reaction.active === 'boolean' ? ', active' : ''}
        ${
          reaction.subsidedOn || reaction.subsidedOn === null
            ? `, subsided_on`
            : ''
        }
        ${
          reaction.deletedOn || reaction.deletedOn === null
            ? `, deleted_on`
            : ''
        }
      ) 
      VALUES (
        ${reaction.userId}
        , ${reaction.elementId}
        , ${reaction.foodGroupingId ? `, ${reaction.foodGroupingId}` : 1}
        , ${reaction.reactionTypeId}
        ${reaction.severityId && `, ${reaction.severityId}`}
        ${typeof reaction.active === 'boolean' ? `, ${reaction.active}` : ''}
        ${
          reaction.subsidedOn || reaction.subsidedOn === null
            ? `, '${reaction.subsidedOn}'`
            : ''
        }
        ${
          reaction.deletedOn || reaction.deletedOn === null
            ? `, '${reaction.deletedOn}'`
            : ''
        }
      )
    ON CONFLICT (user_id, element_id, food_grouping_id, reaction_type) 
    DO 
      UPDATE SET 
        severity=EXCLUDED.severity
        , active=EXCLUDED.active
        , subsided_on=EXCLUDED.subsided_on
        , deleted_on=EXCLUDED.deleted_on
    RETURNING 
   --   id
   --   , user_id AS "userId"
   --   , element_id AS "elementId"
   --   , food_grouping_id AS "foodGroupingId"
   --   , reaction_type AS "reactionType"
   --   , severity AS ""
   --   , active AS ""
   --   , subsided_on AS ""
   --   , last_modified_on AS ""
   --   , identified_on AS ""
   --   , deleted_on AS "",     
   -- user_id AS "userId";
  `;
  console.log(query);
  return query;
};
