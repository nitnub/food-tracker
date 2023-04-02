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
const pg_pool_1 = __importDefault(require("pg-pool"));
const appError_1 = __importDefault(require("../utils/appError"));
const reaction_queries_1 = require("./queries/reaction.queries");
// const Pool = require('pg-pool');
// const url = require('url')
// const params = url.parse(process.env.DATABASE_URL);
// const auth = params.auth.split(':');
// const config = {
//   user: auth[0],
//   password: auth[1],
//   host: params.hostname,
//   port: params.port,
//   database: params.pathname.split('/')[1],
//   ssl: true
// };
// console.log(process.env.NODE_ENV)
// // console.log(process.env)
// console.log(process.env.PORT)
// console.log(process.env.DATABASE_PORT)
if (typeof process.env.DATABASE_PORT !== 'string' &&
    process.env.NODE_ENV === 'development') {
    throw Error('Error identifying DB Port!');
}
// console.log(`ssl= ${process.env.DATABASE_SSL_SETTING}`)
// console.log(process.env.DATABASE_PASSWORD)
// const port = process.env.DATABASE_PORT as unknown as number
const config = {
    user: process.env.DATABASE_USER,
    password: process.env.DATABASE_PASSWORD,
    host: process.env.DATABASE_HOST_NAME,
    port: process.env.DATABASE_PORT,
    database: process.env.DATABASE_NAME,
    ssl: process.env.DATABASE_SSL_SETTING === 'true',
};
// const pool = new Pool(config);
class ReactionRepository {
    constructor() {
        this.addReactions = (reactionsArray) => __awaiter(this, void 0, void 0, function* () {
            const selectQuery = reactionsArray.length;
            let queryString = this.createReactionArrayQuery(reactionsArray);
            const resp = yield this.runQuery(queryString);
            if (!Array.isArray(resp)) {
                return resp;
            }
            // console.log(resp[selectQuery])
            return resp[selectQuery].rows;
        });
        this.getAllReactions = (userId) => __awaiter(this, void 0, void 0, function* () {
            const resp = yield this.pool
                .query((0, reaction_queries_1.getAllUserReactions)(userId))
                .catch((resp) => {
                throw new appError_1.default(resp.message, 400);
            });
            return resp.rows;
        });
        this.runQuery = (queryString) => __awaiter(this, void 0, void 0, function* () {
            return yield this.pool
                .query(queryString)
                .catch((resp) => {
                // console.log(resp)
                throw new appError_1.default(resp.message, 400);
            });
        });
        this.createReactionArrayQuery = (reactionsArray) => {
            let queryString = '';
            for (let reaction of reactionsArray) {
                // const { userId, foodId, reactionTypeId, severityId, active } = reaction;
                // queryString += addReaction(userId, foodId, reactionTypeId, severityId, active);      
                queryString += (0, reaction_queries_1.addReaction)(reaction);
            }
            queryString += (0, reaction_queries_1.getAllUserReactions)(reactionsArray[0].userId);
            console.log((0, reaction_queries_1.getAllUserReactions)(reactionsArray[0].userId));
            return queryString;
        };
        this.pool = new pg_pool_1.default(config);
    }
}
exports.default = ReactionRepository;
// const addReactionQuery = (reaction: ReactionDbEntry) => {
//   const { userId, foodId, reactionTypeId, severityId, active } = reaction;
//   return addReaction(userId, foodId, reactionTypeId, severityId, active);
// };
