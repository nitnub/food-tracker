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
    const {categories, types, severities} = await this.reactionRepository.getReactionOptions();

    for (let category of categories) {
      category.reactionTypes = [];
    }

    for (let type of types) {
      for (let category of categories) {
        if (type.reactionCategory === category.id) {
          category.reactionTypes.push({id: type.id, name: type.name});
        }
      }
    }
    // console.log(categories)
    return {severities, categories};
  };

  addReaction = async (reaction: ReactionDbEntry) => {
    return await this.reactionRepository.addReaction(reaction);
  };

  addReactions = async (reactions: ReactionDbEntry[]) => {
    return await this.reactionRepository.addReactions(reactions);
  };

  getUserReactions = async (userId: number) => {
    // const formattedReaction = []
    const response = await this.reactionRepository.getUserReactions(userId);

    // if (Array.isArray(reactions) && reactions.length === 0) {
    //   throw AppError('Unable to find any reactions for userId') // two scenarios: 1) the user exists with no rows 2) user does not exist. Necessary to check here or just to return zero rows in both instance?
    // }

    const reactions: ReactionComplete[] = response.map(
      (reaction: ReactionDbResponse) => {
        const myObj = {
          reactionId: reaction.id,
          active: reaction.active,
          subsidedOn: reaction.subsidedOn,
          modifiedOn: reaction.modifiedOn,
          identifiedOn: reaction.identifiedOn,
          deletedOn: reaction.deletedOn,
          food: {
            id: reaction.foodId,
            reactionScope: reaction.reactionScope,
            name: reaction.foodName,
            vegetarian: reaction.vegetarian,
            vegan: reaction.vegan,
            glutenFree: reaction.glutenFree,
            fodMap: {
              id: reaction.fodId,
              category: reaction.fodCategory,
              name: reaction.fodName,
              freeUse: reaction.fodFreeUse,
              oligos: reaction.fodOligos,
              fructose: reaction.fodFructose,
              polyols: reaction.fodPolyols,
              lactose: reaction.fodLactose,
              color: reaction.fodColor,
              maxIntake: reaction.maxIntake,
            },
          },
          reaction: {
            category: reaction.reactionCategory,
            type: reaction.reactionType,
            severity: reaction.reactionSeverity,
          },
        };
        return myObj;
      }
    );

    return {
      userId: response[0].userId,
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
