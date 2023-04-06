import { Router } from 'express';
import userController from '@controllers/user.controller';
const userRouter = Router();

userRouter
  .route('/:id')
  .get(userController.getUser)
  .delete(userController.deleteUser);

userRouter
  .route('/')
  .get(userController.getAllUsers)
  .post(userController.addUser);

export default userRouter;
