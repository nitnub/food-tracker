import express, { Request, NextFunction, Response } from 'express';
import cors from 'cors';
import foodRouter from '@routes/food.route';
import reactionRouter from '@routes/reaction.route';
import globalErrorHandler from './controllers/error.controller';
import AppError from './utils/appError';
import userRouter from '@routes/user.route';
import fodmapRouter from '@routes/fodmap.route';

const app = express();

app.use(express.json());
app.use(cors());
app.use('/api/v1/food', foodRouter);
app.use('/api/v1/reaction', reactionRouter);
app.use('/api/v1/user', userRouter);
app.use('/api/v1/fodmap', fodmapRouter);

app.all('*', (req: Request, res: Response, next: NextFunction) => {
  next(new AppError(`Can't find ${req.originalUrl} on this server!`, 404));
});

app.use(globalErrorHandler);

export default app;
