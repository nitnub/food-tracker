import { Router } from 'express';
import c from '@controllers/user.controller';

const userRouter = Router();

userRouter
  .route('/')
  .get(c.getAllUsers)
  .post(c.addUser);

userRouter
  .route('/:id')
  .get(c.getUser)
  .patch(c.updateUser)
  .delete(c.deleteUser);

export default userRouter;
