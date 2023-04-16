"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const express_1 = require("express");
const reaction_controller_1 = __importDefault(require("@controllers/reaction.controller"));
const reactionRouter = (0, express_1.Router)();
reactionRouter
    .route('/')
    .get(reaction_controller_1.default.getReactionOptions)
    .post(reaction_controller_1.default.adminAdd);
reactionRouter
    .route('/:id')
    .get(reaction_controller_1.default.getUserReactions)
    .post(reaction_controller_1.default.addReaction)
    .delete(reaction_controller_1.default.deleteReaction);
exports.default = reactionRouter;
