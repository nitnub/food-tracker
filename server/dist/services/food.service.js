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
const food_repository_1 = __importDefault(require("@repository/food.repository"));
const appError_1 = __importDefault(require("../utils/appError"));
class FoodService {
    constructor() {
        this.getAllFoods = () => __awaiter(this, void 0, void 0, function* () {
            const data = yield this.foodRepository.getAllFoods();
            return data;
        });
        this.addFoods = (foods) => __awaiter(this, void 0, void 0, function* () {
            // TODO: account for blank strings
            const data = yield this.foodRepository.addFoods(foods);
            return data;
        });
        this.deleteFood = (foodId) => __awaiter(this, void 0, void 0, function* () {
            const result = yield this.foodRepository.deleteFood(foodId);
            if (result !== 1) {
                console.log(result);
                throw new appError_1.default(`Unable to find a food with ID ${foodId}.`, 401);
            }
        });
        this.foodRepository = new food_repository_1.default();
    }
}
exports.default = FoodService;
