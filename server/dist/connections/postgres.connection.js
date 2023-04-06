"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const pg_pool_1 = __importDefault(require("pg-pool"));
const postgres_config_1 = __importDefault(require("@configs/postgres.config"));
exports.default = new pg_pool_1.default(postgres_config_1.default);
