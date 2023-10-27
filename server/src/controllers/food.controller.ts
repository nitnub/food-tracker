import { Request, Response } from 'express';
import FoodService from '@services/food.service';
import catchAsync from '../utils/catchAsync';

class FoodController {
  private foodService;
  constructor() {
    this.foodService = new FoodService();
  }

  getAllFoods = catchAsync(async (req: Request, res: Response) => {
    const data = await this.foodService.getAllFoods();

    res.status(200).json({
      status: 'success',
      data,
    });
  });

  addFoods = catchAsync(async (req: Request, res: Response) => {
    let data;
    if (req.body.data) {
      data = await this.foodService.addFoods(req.body.data);
    } else {
      data = await this.foodService.addFoods([req.body]);
    }

    res.status(200).json({
      status: 'success',
      data,
    });
  });

  deleteFood = catchAsync(async (req: Request, res: Response) => {
    await this.foodService.deleteFood(req.params.id);

    res.status(200).json({
      status: 'success',
    });
  });
}

export default new FoodController();
