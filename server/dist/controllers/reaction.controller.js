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
            if (!req.body.reactions) {
                throw new appError_1.default('Request must be a list of formatted reactions', 400);
            }
            const result = yield this.reactionService.addReactions(req.body.reactions);
            res.status(200).json({
                status: 'success',
                result,
            });
        }));
        this.getUserReactions = (0, catchAsync_1.default)((req, res) => __awaiter(this, void 0, void 0, function* () {
            if (!req.params.id) {
                throw new appError_1.default('Request must contain user ID', 400);
            }
            // const userId = req.params.id;
            const results = yield this.reactionService.getUserReactions(req.params.id);
            res.status(200).json({
                status: 'success',
                results,
            });
        }));
        this.deleteReaction = (0, catchAsync_1.default)((req, res) => __awaiter(this, void 0, void 0, function* () {
            const response = this.reactionService.deleteReaction(req.params.id);
            res.status(204).json({
                status: 'success'
            });
        }));
        this.reactionService = new reaction_service_1.default();
    }
}
exports.default = new ReactionController();
