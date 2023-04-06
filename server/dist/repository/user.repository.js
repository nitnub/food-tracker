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
            queryString += (0, user_queries_1.selectAllUsers)();
            const resp = yield this.runQuery(queryString);
            return resp.rows;
        });
        this.updateUser = (user) => __awaiter(this, void 0, void 0, function* () { });
        this.deleteUser = (userId) => __awaiter(this, void 0, void 0, function* () {
            const resp = yield this.runQuery((0, user_queries_1.deleteUser)(userId));
            return resp.rowCount;
        });
        this.runQuery = (queryString) => __awaiter(this, void 0, void 0, function* () {
            return yield this.pool.query(queryString);
            // .catch((resp) 
            // => {
            // console.log(resp)
            // throw new AppError(`${resp.message}.${resp.detail ? ' ' + resp.detail : ''}`, 400);
            // }
            // );
        });
        this.pool = postgres_connection_1.default;
    }
}
exports.default = UserRepository;
