import { ReactionEntry } from '../types/dbTypes';

export default function getReactionListByFood(
  userId: number,
  foodId: number,
  reactionList: any[]
) {
  const outputList: any = [];
  const rawReactions: any = [];

  reactionList.forEach((reaction: any) => {
    if (reaction.food.id === foodId) {
      rawReactions.push(reaction);
    }
  });
  rawReactions.forEach((entry: any) => {
    const { reaction } = entry;

    const formattedReaction: ReactionEntry = {
      userId,
      elementId: foodId,
      reactionId: reaction.id,
      foodGroupingId: reaction.foodGroupingId,
      reactionType: reaction.typeId,
      severity: reaction.severityId,
      active: reaction.active,
    };
    outputList.push(formattedReaction);
  });

  return outputList;
}
