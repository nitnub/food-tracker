import { Request, Response, NextFunction } from 'express';
import AppError from '../utils/appError';
import { DatabaseError } from 'pg';

const sendErrorDev = (err: AppError, res: Response) => {
  res.status(err.statusCode).json({
    status: err.status,
    message: err.message,
    error: err,
    stack: err.stack,
  }).end();
};

const sendErrorProd = (err: AppError, res: Response) => {
  // Operational, trusted error: send message to client
  if (err.isOperational) {
    res.status(err.statusCode).json({
      status: err.status,
      message: err.message,
    }).end();
    // Programming or other unknown error: don't want to leak details to the client
  } else {
    // 1) Log error
    console.error('ERROR!', err);
    // 2) Send generic message
    res.status(500).json({
      status: 'error',
      message: 'Something went wrong!',
    });
  }
};

const handleDuplicatePGKeyError = (err: DatabaseError) => {
  const respArr: string[] = err.detail?.split(')=(') || [];
  const key = respArr[0].replace('Key (', '');
  const value = respArr[1].replace(') already exists.', '');

  return new AppError(
    `${key} value '${value}' already exists and must be unique.`,
    401
  );
};

// type GenError = AppError | Error | DatabaseError
const globalErrorHandler = (
  err: AppError,
  req: Request,
  res: Response ,
  next: NextFunction
) => {
  err.statusCode = err.statusCode || 500;
  err.status = err.status || 'error';

  if (process.env.NODE_ENV === 'development') {
    sendErrorDev(err, res);
  } else if (process.env.NODE_ENV === 'production') {
    let error = { ...err };
    // console.log('err')
    // console.log(err)
    // note that I had to do err.name in the comparison instead of error.name (the latter did work in the original). Appears there has been a change in mongo..
    if (err.code === '23505') error = handleDuplicatePGKeyError(error as any);
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

export default globalErrorHandler;
