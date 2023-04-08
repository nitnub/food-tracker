"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const appError_1 = __importDefault(require("../utils/appError"));
const sendErrorDev = (err, res) => {
    res.status(err.statusCode).json({
        status: err.status,
        message: err.message,
        error: err,
        stack: err.stack,
    });
};
const sendErrorProd = (err, res) => {
    // Operational, trusted error: send message to client
    if (err.isOperational) {
        res.status(err.statusCode).json({
            status: err.status,
            message: err.message,
        });
        // Programming or other unknown error: don't want to leak details tto the client
    }
    else {
        // 1) Log error
        console.error('ERROR!', err);
        // 2) Send generic message
        res.status(500).json({
            status: 'error',
            message: 'Something went very wrong!',
        });
    }
};
const handleDuplicatePGKeyError = (err) => {
    var _a;
    const respArr = ((_a = err.detail) === null || _a === void 0 ? void 0 : _a.split(')=(')) || [];
    const key = respArr[0].replace('Key (', '');
    const value = respArr[1].replace(') already exists.', '');
    return new appError_1.default(`${key} value '${value}' already exists and must be unique.`, 401);
};
// type GenError = AppError | Error | DatabaseError
const globalErrorHandler = (err, req, res, next) => {
    err.statusCode = err.statusCode || 500;
    err.status = err.status || 'error';
    if (process.env.NODE_ENV === 'development') {
        sendErrorDev(err, res);
    }
    else if (process.env.NODE_ENV === 'production') {
        let error = Object.assign({}, err);
        // console.log('err')
        // console.log(err)
        // note that I had to do err.name in the comparison instead of error.name (the latter did work in the original). Appears there has been a change in mongo..
        if (err.code === '23505')
            error = handleDuplicatePGKeyError(error);
        // if (err.name === 'CastError') error = handleCastErrorDB(err);
        // if (err.code === 11000) error = handleDuplicateFieldsDB(error);
        // if (err.code === 11000) error = handleDuplicateFieldsDB(error);
        // if (err.name === 'ValidationError') error = handleValidationErrorDB(error);
        // if (err.name === 'JsonWebTokenError') error = handleJWTError();
        // if (err.name === 'TokenExpiredError') error = handleJWTExpiredError();
        // console.log('err end')
        sendErrorProd(error, res);
    }
};
exports.default = globalErrorHandler;
