import { Router } from 'express';
import c from '@controllers/reaction.controller';

const reactionRouter = Router();

reactionRouter
  .route('/')
  .get(c.getReactionOptions)
  .post(c.adminAdd);

reactionRouter
  .route('/:id')
  .get(c.getUserReactions)
  .post(c.addReaction)
  .delete(c.deleteReaction);

export default reactionRouter;
