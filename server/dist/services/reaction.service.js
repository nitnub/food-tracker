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
            return { severities, categories };
        });
        this.addReaction = (reaction) => __awaiter(this, void 0, void 0, function* () {
            return yield this.reactionRepository.addReaction(reaction);
        });
        this.addReactions = (reactions) => __awaiter(this, void 0, void 0, function* () {
            return yield this.reactionRepository.addReactions(reactions);
        });
        this.getUserReactions = (userId) => __awaiter(this, void 0, void 0, function* () {
            const reactions = yield this.reactionRepository.getUserReactions(userId);
            console.log('reactions');
            // console.log(reactions);
            // console.log(Object.keys(response));
            // console.log(response[0]);
            // console.log(response[0]['json_build_object']);
            // response.forEach(reaction: ReactionD)
            // const reactions = response.map((reaction) => reaction['json_build_object']); //['json_build_object'];
            const reactiveFoods = [];
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
        });
        this.deleteReaction = (reactionId) => __awaiter(this, void 0, void 0, function* () {
            const result = yield this.reactionRepository.deleteReaction(reactionId);
            if (Array.isArray(result) && result.length === 0) {
                throw new appError_1.default(`Unable to find any results for Reaction ID ${reactionId}.`, 401);
            }
        });
        this.formatReactionForDb = (reaction) => {
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
