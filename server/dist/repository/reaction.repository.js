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
const postgres_connection_1 = __importDefault(require("@connections/postgres.connection"));
const appError_1 = __importDefault(require("../utils/appError"));
const reaction_queries_1 = require("./queries/reaction.queries");
class ReactionRepository {
    constructor() {
        this.addReactions = (reactionsArray) => __awaiter(this, void 0, void 0, function* () {
            const selectQuery = reactionsArray.length;
            let queryString = this.createReactionArrayQuery(reactionsArray);
            const resp = yield this.runQuery(queryString);
            if (!Array.isArray(resp)) {
                return resp;
            }
            console.log(resp[selectQuery]);
            return resp[selectQuery].rows;
        });
        this.getUserReactions = (userId) => __awaiter(this, void 0, void 0, function* () {
            const resp = yield this.runQuery((0, reaction_queries_1.selectUserReactions)(userId));
            return resp.rows;
        });
        this.deleteReaction = (reactionId) => __awaiter(this, void 0, void 0, function* () {
            const resp = yield this.runQuery((0, reaction_queries_1.deleteReaction)(reactionId));
            return resp.rows;
        });
        this.runQuery = (queryString) => __awaiter(this, void 0, void 0, function* () {
            return yield this.pool
                .query(queryString)
                .catch((resp) => {
                throw new appError_1.default(resp.message, 400);
            });
        });
        this.createReactionArrayQuery = (reactionsArray) => {
            let queryString = '';
            for (let reaction of reactionsArray) {
                queryString += (0, reaction_queries_1.insertReaction)(reaction);
            }
            queryString += (0, reaction_queries_1.selectUserReactions)(reactionsArray[0].userId);
            return queryString;
        };
        this.pool = postgres_connection_1.default;
    }
}
exports.default = ReactionRepository;
