import ReactionRepository from '@repository/reaction.repository';
import { Reaction, ReactionDbEntry } from '../types/reaction.types';

class ReactionService {
  private reactionRepository;
  constructor() {
    this.reactionRepository = new ReactionRepository();
  }

  addReactions = async (reactions: ReactionDbEntry[]) => {

    // const formattedReaction = this.formatReactionForDb(reaction)

    return await this.reactionRepository.addReactions(reactions);
  };
  // addReaction = (reaction: Reaction) => {

  //   // const formattedReaction = this.formatReactionForDb(reaction)

  //   return this.reactionRepository.addReaction(reaction);
  // };

  getAllReactions = async (userId: number) => {
    return await this.reactionRepository.getAllReactions(userId);
  };

  formatReactionForDb = (reaction: Reaction) => {
    // const formattedReaction: ReactionDbEntry = {
    return {
      userId: reaction.user.id,
      foodId: reaction.food.id,
      reactionTypeId: reaction.reactionType.id,
      severityId: reaction.severity.id,
      active: reaction.active,
    };
  };
}





export default ReactionService;


// const formatReactionForDb = (reaction: Reaction) => {
//   // const formattedReaction: ReactionDbEntry = {
//   return {
//     userId: reaction.user.id,
//     foodId: reaction.food.id,
//     reactionTypeId: reaction.reactionType.id,
//     severityId: reaction.severity.id,
//     active: reaction.active,
//   };
// };
