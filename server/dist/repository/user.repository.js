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
const postgres_connection_1 = __importDefault(require("@connections/postgres.connection"));
const appError_1 = __importDefault(require("../utils/appError"));
const user_queries_1 = require("./queries/user.queries");
class UserRepository {
    constructor() {
        this.userExists = (userId) => __awaiter(this, void 0, void 0, function* () {
            const user = yield this.runQuery((0, user_queries_1.selectUser)(userId));
            if (Array.isArray(user) && user.length > 0) {
                // TODO: can check for deleted.
                return true;
            }
            return false;
        });
        this.getUser = (userId) => __awaiter(this, void 0, void 0, function* () {
            const resp = yield this.runQuery((0, user_queries_1.selectUser)(userId));
            return resp.rows;
        });
        this.getAllUsers = () => __awaiter(this, void 0, void 0, function* () {
            const resp = yield this.runQuery((0, user_queries_1.selectAllUsers)());
            return resp.rows;
        });
        this.addUser = (userArray) => __awaiter(this, void 0, void 0, function* () {
            let queryString = '';
            for (let user of userArray) {
                queryString += (0, user_queries_1.insertUser)(user);
            }
            // add select statement at end of query
            if (userArray.length === 1) {
                queryString += (0, user_queries_1.selectUserByEmail)(userArray[0].email);
            }
            else {
                queryString += (0, user_queries_1.selectAllUsers)();
            }
            const resp = yield this.runQuery(queryString);
            if (!Array.isArray(resp)) {
                return resp;
            }
            return resp[userArray.length].rows;
        });
        this.updateUser = (globalUserId, userUpdates) => __awaiter(this, void 0, void 0, function* () {
            const resp = yield this.runQuery((0, user_queries_1.updateUserByGlobalId)(globalUserId, userUpdates));
            if (!Array.isArray(resp)) {
                throw new appError_1.default(`Unable to update user ${globalUserId}`, 400);
            }
            if (resp[1].rows.length === 0) {
                throw new appError_1.default(`Unable to update user ${globalUserId}; user not found.`, 400);
            }
            return resp[1].rows;
        });
        this.deleteUser = (userId) => __awaiter(this, void 0, void 0, function* () {
            const resp = yield this.runQuery((0, user_queries_1.deleteUser)(userId));
            return resp.rowCount;
        });
        this.runQuery = (queryString) => __awaiter(this, void 0, void 0, function* () {
            return yield this.pool.query(queryString);
        });
        this.pool = postgres_connection_1.default;
    }
}
exports.default = UserRepository;
