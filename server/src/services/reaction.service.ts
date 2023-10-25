import ReactionRepository from '@repository/reaction.repository';
import {
  Reaction,
  ReactionComplete,
  ReactionDbEntry,
  ReactionDbResponse,
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
    console.log('reactions');
    // console.log(reactions);
    // console.log(Object.keys(response));
    // console.log(response[0]);
    // console.log(response[0]['json_build_object']);

    // response.forEach(reaction: ReactionD)
    // const reactions = response.map((reaction) => reaction['json_build_object']); //['json_build_object'];
    const reactiveFoods: number[] = [];
    reactions.forEach((el) => {
      console.log(el);
      if (
        // !reactiveFoods.includes(reaction.foodId) &&
        !reactiveFoods.includes(el.food.id) &&
        el.reaction.severityId !== 1
        // el.reaction. .severityId !== 1
      ) {
        // reactiveFoods.push(reaction.foodId);
        reactiveFoods.push(el.food.id);
      }
    });

    console.log(reactions);
    return {
      userId: reactions[0].userId,
      reactiveFoods,
      resultCount: reactions.length,
      reactions,
    };
  };

  deleteReaction = async (reactionId: number) => {
    const result = await this.reactionRepository.deleteReaction(reactionId);

    if (Array.isArray(result) && result.length === 0) {
      throw new AppError(
        `Unable to find any results for Reaction ID ${reactionId}.`,
        401
      );
    }
  };

  formatReactionForDb = (reaction: Reaction) => {
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
