import { Request, Response } from 'express';
import ReactionService from '@services/reaction.service';
import catchAsync from '../utils/catchAsync';
import AppError from '../utils/appError';

class ReactionController {
  private reactionService;

  constructor() {
    this.reactionService = new ReactionService();
  }
  
  getReactionOptions = catchAsync(async (req: Request, res: Response) => {
    const data = await this.reactionService.getReactionOptions();
    res.status(200).json({
      status: 'success',
      data,
    });
  });

  adminAdd = catchAsync(async (req: Request, res: Response) => {
    if (!req.body.reactions) {
      throw new AppError('Request must be a list of formatted reactions', 400);
    }
    const result = await this.reactionService.addReactions(req.body.reactions);
    res.status(200).json({
      status: 'success',
      result,
    });
  });

  addReaction = catchAsync(async (req: Request, res: Response) => {
    // temp sanitization; pre-middleware
    const sanitizedRequest = { ...req.body, userId: Number(req.params.id) };

    const result = await this.reactionService.addReaction(sanitizedRequest);
    res.status(200).json({
      status: 'success',
      result,
    });
  });

  getUserReactions = catchAsync(async (req: Request, res: Response) => {
    if (!req.params.id) {
      throw new AppError('Request must contain user ID', 400);
    }
    const data = await this.reactionService.getUserReactions(req.params.id);

    res.status(200).json({
      status: 'success',
      data,
    });
  });

  deleteReaction = catchAsync(async (req: Request, res: Response) => {
    await this.reactionService.deleteReaction(req.params.id);

    res.status(202).json({
      status: 'success',
    });
  });
}

export default new ReactionController();
