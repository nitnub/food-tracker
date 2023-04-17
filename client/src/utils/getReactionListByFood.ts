import { ReactionEntry } from "../types/dbTypes";

export default  function getReactionListByFood(
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
    // console.log('RR', reaction);
    // console.log('RR', entry);
    const formattedReaction: ReactionEntry = {
      // formattedReaction.userId = userId; // Included in API_params
      userId,
      elementId: foodId,
      foodGroupingId: reaction.foodGroupingId,
      reactionType: reaction.typeId,
      severity: reaction.severityId,
      active: reaction.active,
    };
    outputList.push(formattedReaction);
  });

  // console.log('ITEMS:');
  // console.log(outputList);
  return outputList;
}