"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const express_1 = require("express");
const food_controller_1 = require("@controllers/food.controller");
const foodRouter = (0, express_1.Router)();
foodRouter.route('/').post(food_controller_1.add);
exports.default = foodRouter;
