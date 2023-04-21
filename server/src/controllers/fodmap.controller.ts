import { NextFunction, Request, Response } from 'express';
import FodMapService from '../services/fodmap.service';

class FodMapController {
  private fodMapService: FodMapService;

  constructor() {
    this.fodMapService = new FodMapService();
  }

  getAll = async (req: Request, res: Response, next: NextFunction) => {
    const data = await this.fodMapService.getAll();
    res.status(200).json({
      status: 'success',
      data,
    });
  };
}

export default new FodMapController();
