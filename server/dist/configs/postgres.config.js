"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
if (typeof process.env.DATABASE_PORT !== 'string' &&
    process.env.NODE_ENV === 'development') {
    throw Error('Error identifying DB Port!');
}
exports.default = {
    user: process.env.DATABASE_USER,
    password: process.env.DATABASE_PASSWORD,
    host: process.env.DATABASE_HOST_NAME,
    port: process.env.DATABASE_PORT,
    database: process.env.DATABASE_NAME,
    ssl: process.env.DATABASE_SSL_SETTING === 'true',
};
