"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const express_1 = require("express");
const user_controller_1 = __importDefault(require("@controllers/user.controller"));
const userRouter = (0, express_1.Router)();
userRouter
    .route('/:id')
    .get(user_controller_1.default.getUser)
    .patch(user_controller_1.default.updateUser)
    .delete(user_controller_1.default.deleteUser);
userRouter
    .route('/')
    .get(user_controller_1.default.getAllUsers)
    .post(user_controller_1.default.addUser);
exports.default = userRouter;
