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
const user_repository_1 = __importDefault(require("@repository/user.repository"));
const appError_1 = __importDefault(require("../utils/appError"));
class UserService {
    constructor() {
        this.userExists = (userId) => __awaiter(this, void 0, void 0, function* () {
            return yield this.userRepository.userExists(userId);
        });
        this.getAllUsers = () => __awaiter(this, void 0, void 0, function* () {
            const users = yield this.userRepository.getAllUsers();
            return users;
        });
        this.getUser = (userId) => __awaiter(this, void 0, void 0, function* () {
            const user = yield this.userRepository.getUser(userId);
            if (!Array.isArray(user) || user.length === 0) {
                throw new appError_1.default(`Unable to find user ${userId}`, 401);
            }
            return user;
        });
        this.addUser = (userArray) => __awaiter(this, void 0, void 0, function* () {
            // check for admin to allow more than one entry
            return yield this.userRepository.addUser(userArray);
        });
        this.updateUser = (globalUserId, userUpdates) => __awaiter(this, void 0, void 0, function* () {
            return yield this.userRepository.updateUser(globalUserId, userUpdates);
        });
        this.deleteUser = (userId) => __awaiter(this, void 0, void 0, function* () {
            const result = yield this.userRepository.deleteUser(userId);
            if (result === 0) {
                throw new appError_1.default(`Unable to find any results for User ID ${userId}.`, 401);
            }
        });
        this.userRepository = new user_repository_1.default();
    }
}
exports.default = UserService;
