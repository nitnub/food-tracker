import { Request, Response } from 'express';
import ReactionService from '@services/reaction.service';
import catchAsync from '../utils/catchAsync';
import AppError from '../utils/appError';

class ReactionController {
  private reactionService;

  constructor() {
    this.reactionService = new ReactionService();
  }
  add = catchAsync(async (req: Request, res: Response) => {
    // console.log(`request body:`, req.body);
    // console.log('Reaction controller!');
    // call service to handle update requst

    // {
    //   "user": {"displayName": "argoUser", "id": 2},
    //   "food": {"displayName": "argoFood", "id": 2},
    //   "reactionType": {"displayName": "argoReactionType", "id": 2},
    //   "severity": {"displayName": "argoSeverity", "id": 2},
    //   "active": true
    // }

    if (!req.body.reactions) {
      throw new AppError('Request must be a list of formatted reactions', 400);
    }

    const { reactions } = req.body;

    // const result = await this.reactionService.addReaction(req.body);
    const result = await this.reactionService.addReactions(reactions);
    // const status = result.length > 0 ? 'success' : 'fail';
    console.log('result is')
    console.log(result)
    res.status(200).json({
      status: 'success',
      result,
    });
  });

  getAllReactions = catchAsync(async (req: Request, res: Response) => {
    const userId = req.params.id;
    const results = await this.reactionService.getAllReactions(userId);

    res.status(200).json({
      status: 'success',
      results,
    });
  });
}
// export const add = (req: Request, res: Response) => {
//   console.log(`request body:`, req.body);

//   // call service to handle update requst

//   this.reactionService.addReaction(req.body)

//   res.status(200).json({
//     status: 'success',
//   });
// };

export default new ReactionController();
