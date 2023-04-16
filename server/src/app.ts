import 'module-alias/register';
import dotenv from 'dotenv';
// configure dotenv before module imports
dotenv.config({ path: `.env.${process.env.NODE_ENV || 'development'}` });

import express, { Request, NextFunction, Response } from 'express';
import cors from 'cors';
import testRouter from '@routes/test.route';
import foodRouter from '@routes/food.route';
import reactionRouter from '@routes/reaction.route';
import globalErrorHandler from './controllers/error.controller';
import AppError from './utils/appError';
import userRouter from '@routes/user.route';

// import cat from '@repository/test'

const app = express();
// console.log(process.env.NODE_ENV)
// console.log(app.get('env'))
// // console.log(cat)
// console.log(`app= ${process.env.ENV_MESSAGE}`)
const PORT = process.env.PORT;

app.use(express.json());
app.use(cors())
app.use('/api/v1/food', foodRouter);
app.use('/api/v1/reaction', reactionRouter);
app.use('/api/v1/user', userRouter);

app.all('*', (req: Request, res: Response, next: NextFunction) => {
  next(new AppError(`Can't find ${req.originalUrl} on this server!`, 404));
});

app.use(globalErrorHandler);

app.listen(PORT, () => {
  console.log(`Listening on port ${PORT} in ${process.env.NODE_ENV} mode...`);
});
