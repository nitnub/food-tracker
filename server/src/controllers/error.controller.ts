import { Request, Response, NextFunction } from 'express';
import AppError from '../utils/appError';
import { DatabaseError } from 'pg';

const sendErrorDev = (err: AppError, res: Response) => {
  res
    .status(err.statusCode)
    .json({
      status: err.status,
      message: err.message,
      error: err,
      stack: err.stack,
    })
    .end();
};

const sendErrorProd = (err: AppError, res: Response) => {
  // Operational, trusted error: send message to client
  if (err.isOperational) {
    res
      .status(err.statusCode)
      .json({
        status: err.status,
        message: err.message,
      })
      .end();
  } else {
    res.status(500).json({
      status: 'error',
      message: 'Something went wrong!',
    });
  }
};

const pgRangeError = (err: DatabaseError) => {
  const mArray = err.message.split('"');
  const value = mArray[1];
  return new AppError(`'${value}' is not a valid value.`, 401);
};

const globalErrorHandler = (
  err: AppError,
  req: Request,
  res: Response,
  next: NextFunction
) => {
  err.statusCode = err.statusCode || 500;
  err.status = err.status || 'error';

  if (process.env.NODE_ENV === 'development') {
    sendErrorDev(err, res);
  } else if (process.env.NODE_ENV === 'production') {
    if (err.code === '42703') err = pgRangeError(err as any);
    sendErrorProd(err, res);
  }
};

export default globalErrorHandler;
