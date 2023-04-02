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
const reaction_service_1 = __importDefault(require("@services/reaction.service"));
const catchAsync_1 = __importDefault(require("../utils/catchAsync"));
const appError_1 = __importDefault(require("../utils/appError"));
class ReactionController {
    constructor() {
        this.add = (0, catchAsync_1.default)((req, res) => __awaiter(this, void 0, void 0, function* () {
            // console.log(`request body:`, req.body);
            // console.log('Reaction controller!');
            // call service to handle update requst
            // {
            //   "user": {"displayName": "argoUser", "id": 2},
            //   "food": {"displayName": "argoFood", "id": 2},
            //   "reactionType": {"displayName": "argoReactionType", "id": 2},
            //   "severity": {"displayName": "argoSeverity", "id": 2},
            //   "active": true
            // }
            if (!req.body.reactions) {
                throw new appError_1.default('Request must be a list of formatted reactions', 400);
            }
            const { reactions } = req.body;
            // const result = await this.reactionService.addReaction(req.body);
            const result = yield this.reactionService.addReactions(reactions);
            // const status = result.length > 0 ? 'success' : 'fail';
            console.log('result is');
            console.log(result);
            res.status(200).json({
                status: 'success',
                result,
            });
        }));
        this.getAllReactions = (0, catchAsync_1.default)((req, res) => __awaiter(this, void 0, void 0, function* () {
            const userId = req.params.id;
            const results = yield this.reactionService.getAllReactions(userId);
            res.status(200).json({
                status: 'success',
                results,
            });
        }));
        this.reactionService = new reaction_service_1.default();
    }
}
// export const add = (req: Request, res: Response) => {
//   console.log(`request body:`, req.body);
//   // call service to handle update requst
//   this.reactionService.addReaction(req.body)
//   res.status(200).json({
//     status: 'success',
//   });
// };
exports.default = new ReactionController();
