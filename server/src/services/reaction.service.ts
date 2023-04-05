import ReactionRepository from '@repository/reaction.repository';
import { Reaction, ReactionDbEntry } from '../types/reaction.types';
import AppError from '../utils/appError';

class ReactionService {
  private reactionRepository;
  constructor() {
    this.reactionRepository = new ReactionRepository();
  }

  addReactions = async (reactions: ReactionDbEntry[]) => {
    return await this.reactionRepository.addReactions(reactions);
  };

  getUserReactions = async (userId: number) => {

    const reactions = await this.reactionRepository.getUserReactions(userId);
    // if (Array.isArray(reactions) && reactions.length === 0) {
    //   throw AppError('Unable to find any reactions for userId') // two scenarios: 1) the user exists with no rows 2) user does not exist. Necessary to check here or just to return zero rows in both instance?
    // }
    return reactions;
  };


  deleteReaction = async (userId: number) => {
    return await this.reactionRepository.deleteReaction(userId);
  }

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
