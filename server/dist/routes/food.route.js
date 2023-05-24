"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const express_1 = require("express");
const food_controller_1 = __importDefault(require("@controllers/food.controller"));
const foodRouter = (0, express_1.Router)();
foodRouter
    .route('/')
    .get(food_controller_1.default.getAllFoods)
    .post(food_controller_1.default.addFoods);
foodRouter
    .route('/:id')
    // .patch(foodController.updateFood)  // need to implement
    .delete(food_controller_1.default.deleteFood);
exports.default = foodRouter;
