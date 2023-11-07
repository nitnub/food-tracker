import { Router } from 'express';
import userController from '@controllers/user.controller';
const userRouter = Router();

userRouter
  .route('/')
  .get(userController.getAllUsers)
  .post(userController.addUser);

userRouter
  .route('/:id')
  .get(userController.getUser)
  .patch(userController.updateUser)
  .delete(userController.deleteUser);

export default userRouter;
