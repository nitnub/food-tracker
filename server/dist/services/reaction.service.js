"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const reaction_repository_1 = __importDefault(require("@repository/reaction.repository"));
const appError_1 = __importDefault(require("../utils/appError"));
class ReactionService {
    constructor() {
        this.getReactionOptions = () => __awaiter(this, void 0, void 0, function* () {
            const { categories, types, severities } = yield this.reactionRepository.getReactionOptions();
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
            // console.log(categories)
            return { severities, categories };
        });
        this.addReaction = (reaction) => __awaiter(this, void 0, void 0, function* () {
            return yield this.reactionRepository.addReaction(reaction);
        });
        this.addReactions = (reactions) => __awaiter(this, void 0, void 0, function* () {
            return yield this.reactionRepository.addReactions(reactions);
        });
        this.getUserReactions = (userId) => __awaiter(this, void 0, void 0, function* () {
            // const formattedReaction = []
            const response = yield this.reactionRepository.getUserReactions(userId);
            // if (Array.isArray(reactions) && reactions.length === 0) {
            //   throw AppError('Unable to find any reactions for userId') // two scenarios: 1) the user exists with no rows 2) user does not exist. Necessary to check here or just to return zero rows in both instance?
            // }
            const reactions = response.map((reaction) => {
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
                            categoryId: reaction.fodCategory,
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
                        id: reaction.id,
                        category: reaction.reactionCategory,
                        typeName: reaction.reactionTypeName,
                        typeId: reaction.reactionTypeId,
                        severityName: reaction.severityName,
                        severityId: reaction.severityId,
                        foodGroupingId: reaction.foodGroupingId
                    },
                };
                return myObj;
            });
            const reactiveFoods = [];
            response.forEach((reaction) => {
                // const myObj = {
                //   reactionId: reaction.id,
                //   active: reaction.active,
                //   subsidedOn: reaction.subsidedOn,
                //   modifiedOn: reaction.modifiedOn,
                //   identifiedOn: reaction.identifiedOn,
                //   deletedOn: reaction.deletedOn,
                //   food: {
                //     id: reaction.foodId,
                //     reactionScope: reaction.reactionScope,
                //     name: reaction.foodName,
                //     vegetarian: reaction.vegetarian,
                //     vegan: reaction.vegan,
                //     glutenFree: reaction.glutenFree,
                //     fodMap: {
                //       id: reaction.fodId,
                //       category: reaction.fodCategory,
                //       categoryId: reaction.fodCategory,
                //       name: reaction.fodName,
                //       freeUse: reaction.fodFreeUse,
                //       oligos: reaction.fodOligos,
                //       fructose: reaction.fodFructose,
                //       polyols: reaction.fodPolyols,
                //       lactose: reaction.fodLactose,
                //       color: reaction.fodColor,
                //       maxIntake: reaction.maxIntake,
                //     },
                //   },
                //   reaction: {
                //     id: reaction.id,
                //     category: reaction.reactionCategory,
                //     typeName: reaction.reactionTypeName,
                //     typeId: reaction.reactionTypeId,
                //     severityName: reaction.severityName,
                //     severityId: reaction.severityId,
                //     foodGroupingId: reaction.foodGroupingId
                //   },
                // };
                if (!reactiveFoods.includes(reaction.foodId)) {
                    reactiveFoods.push(reaction.foodId);
                }
            });
            return {
                userId: response[0].userId,
                reactiveFoods,
                resultCount: reactions.length,
                reactions,
            };
        });
        this.deleteReaction = (reactionId) => __awaiter(this, void 0, void 0, function* () {
            const result = yield this.reactionRepository.deleteReaction(reactionId);
            if (Array.isArray(result) && result.length === 0) {
                throw new appError_1.default(`Unable to find any results for Reaction ID ${reactionId}.`, 401);
            }
        });
        this.formatReactionForDb = (reaction) => {
            // const formattedReaction: ReactionDbEntry = {
            return {
                userId: reaction.user.id,
                foodId: reaction.food.id,
                reactionTypeId: reaction.reactionType.id,
                severityId: reaction.severity.id,
                active: reaction.active,
            };
        };
        this.reactionRepository = new reaction_repository_1.default();
    }
}
exports.default = ReactionService;
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
