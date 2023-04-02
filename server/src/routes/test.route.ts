import { Router } from 'express';

const testRouter = Router();

testRouter
  .route('/')
  .post()
  .delete();
// testRouter.route('/').delete();

export default testRouter;
