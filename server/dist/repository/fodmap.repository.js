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
const fodmap_queries_1 = require("./queries/fodmap.queries");
class FodMapRepository {
    constructor() {
        this.selectAll = () => __awaiter(this, void 0, void 0, function* () {
            const formatted = [];
            const resp = yield this.runQuery((0, fodmap_queries_1.selectAllAsObj)());
            // console.log(resp);
            resp.rows.forEach((jbo) => {
                // console.log(jbo);
                const formattedAlias = [];
                // return {...jbo, aliasList: jbo.aliasList.split("&%&")}
                const aliasList = jbo.json_build_object.aliasList
                    ? jbo.json_build_object.aliasList.split('&%&')
                    : null;
                // formatted.push({ ...jbo.json_build_object, aliasList: jbo.json_build_object.aliasList.split('&%&') });
                formatted.push(Object.assign(Object.assign({}, jbo.json_build_object), { aliasList }));
            });
            // return resp.rows;
            // console.log(formatted);
            return formatted;
        });
        this.runQuery = (queryString) => __awaiter(this, void 0, void 0, function* () {
            return yield this.pool.query(queryString);
        });
        this.pool = postgres_connection_1.default;
    }
}
exports.default = FodMapRepository;
