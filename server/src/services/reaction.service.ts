import ReactionRepository from '@repository/reaction.repository';
import {
  Reaction,
  ReactionComplete,
  ReactionDbEntry,
} from '../types/reaction.types';
import AppError from '../utils/appError';

class ReactionService {
  private reactionRepository;
  constructor() {
    this.reactionRepository = new ReactionRepository();
  }

  getReactionOptions = async () => {
    const { categories, types, severities } =
      await this.reactionRepository.getReactionOptions();

    for (let category of categories) {
      category.reactionTypes = [];
    }

    for (let type of types) {
      for (let category of categories) {
        if (type.reactionCategory === category.id) {
          category.reactionTypes.push({ id: type.id, name: type.name });
        }
      }
    }
    return { severities, categories };
  };

  addReaction = async (reaction: ReactionDbEntry) => {
    return await this.reactionRepository.addReaction(reaction);
  };

  addReactions = async (reactions: ReactionDbEntry[]) => {
    return await this.reactionRepository.addReactions(reactions);
  };

  getUserReactions = async (userId: number) => {
    const reactions = await this.reactionRepository.getUserReactions(userId);

    const reactiveFoods: number[] = [];
    reactions.forEach((el: ReactionComplete) => {
      if (!reactiveFoods.includes(el.food.id) && el.reaction.severityId !== 1) {
        reactiveFoods.push(el.food.id);
      }
    });

    return {
      id: reactions[0].id,
      reactiveFoods,
      resultCount: reactions.length,
      reactions,
    };
  };

  deleteReaction = async (reactionId: number) => {
    const result = await this.reactionRepository.deleteReaction(reactionId);

    if (result === 0) {
      throw new AppError(
        `Unable to find any results for Reaction ID ${reactionId}.`,
        401
      );
    }
  };

  formatReactionForDb = (reaction: Reaction) => {
    return {
      id: reaction.user.id,
      foodId: reaction.food.id,
      reactionTypeId: reaction.reactionType.id,
      severityId: reaction.severity.id,
      active: reaction.active,
    };
  };
}

export default ReactionService;
