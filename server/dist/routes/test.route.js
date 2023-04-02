"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const express_1 = require("express");
const testRouter = (0, express_1.Router)();
testRouter
    .route('/')
    .post()
    .delete();
// testRouter.route('/').delete();
exports.default = testRouter;
