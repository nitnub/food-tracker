"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
require("module-alias/register");
const dotenv_1 = __importDefault(require("dotenv"));
// configure dotenv before module imports
dotenv_1.default.config({ path: `.env.${process.env.NODE_ENV || 'development'}` });
const express_1 = __importDefault(require("express"));
const cors_1 = __importDefault(require("cors"));
const food_route_1 = __importDefault(require("@routes/food.route"));
const reaction_route_1 = __importDefault(require("@routes/reaction.route"));
const error_controller_1 = __importDefault(require("./controllers/error.controller"));
const appError_1 = __importDefault(require("./utils/appError"));
const user_route_1 = __importDefault(require("@routes/user.route"));
// import cat from '@repository/test'
const app = (0, express_1.default)();
// console.log(process.env.NODE_ENV)
// console.log(app.get('env'))
// // console.log(cat)
// console.log(`app= ${process.env.ENV_MESSAGE}`)
const PORT = process.env.PORT;
app.use(express_1.default.json());
app.use((0, cors_1.default)());
app.use('/api/v1/food', food_route_1.default);
app.use('/api/v1/reaction', reaction_route_1.default);
app.use('/api/v1/user', user_route_1.default);
app.all('*', (req, res, next) => {
    next(new appError_1.default(`Can't find ${req.originalUrl} on this server!`, 404));
});
app.use(error_controller_1.default);
app.listen(PORT, () => {
    console.log(`Listening on port ${PORT} in ${process.env.NODE_ENV} mode...`);
});
