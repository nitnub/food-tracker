import { Router } from 'express';
import reactionController from '@controllers/reaction.controller';

const reactionRouter = Router();

reactionRouter
  .route('/')
  .get(reactionController.getReactionOptions)
  .post(reactionController.adminAdd);

reactionRouter
  .route('/:id')
  .get(reactionController.getUserReactions)
  .post(reactionController.addReaction)
  .delete(reactionController.deleteReaction);

export default reactionRouter;
