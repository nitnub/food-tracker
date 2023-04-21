"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const express_1 = require("express");
const fodmap_controller_1 = __importDefault(require("@controllers/fodmap.controller"));
const fodmapRouter = (0, express_1.Router)();
fodmapRouter.get('/', fodmap_controller_1.default.getAll);
exports.default = fodmapRouter;
