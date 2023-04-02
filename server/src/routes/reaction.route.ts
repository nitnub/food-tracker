import { Router } from "express"; 
import reactionController from '@controllers/reaction.controller'

const reactionRouter = Router()

reactionRouter.route('/').post(reactionController.add)
reactionRouter.route('/:id').get(reactionController.getAllReactions)

export default reactionRouter;