import { getMockReq, getMockRes } from '@jest-mock/express';
import AppError from '../../utils/appError';
import globalErrorHandler from '../error.controller';

const req = getMockReq();
const { res, next } = getMockRes();

const originalEnv = process.env;

afterEach(() => {
  jest.clearAllMocks();
  process.env = originalEnv;
});

describe('Error Controller test suite', () => {
  describe('development errors', () => {
    beforeEach(() => {
      process.env.NODE_ENV = 'development';
    });

    it('sends general dev error', () => {
      const err = new AppError('Test error', 400);

      globalErrorHandler(err, req, res, next);

      expect(res.json).toHaveBeenCalledWith({
        status: err.status,
        message: err.message,
        error: err,
        stack: err.stack,
      });
    });
  });

  describe('production errors', () => {
    beforeEach(() => {
      process.env.NODE_ENV = 'production';
    });

    it('sends general prod (400)', () => {
      const err = new AppError('Test error', 400);
      globalErrorHandler(err, req, res, next);

      expect(res.json).toHaveBeenCalledWith({
        status: 'fail',
        message: err.message,
      });
    });
    it('sends general prod (500)', () => {
      const err = new AppError('Test error', 500);
      err.isOperational = false;
      globalErrorHandler(err, req, res, next);
      expect(res.json).toHaveBeenCalledWith({
        status: 'error',
        message: 'Something went wrong!',
      });
    });

  });
});
