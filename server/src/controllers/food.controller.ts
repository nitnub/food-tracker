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

  addFood = catchAsync(async (req: Request, res: Response) => {
    let response;

    if (req.body.data.length === 1) {
      response = await this.foodService.addFood(req.body.data[0]);
    } else {
      response = await this.foodService.addFoods(req.body.data);
    }
    res.status(200).json(response);
  });

  updateFood = catchAsync(async (req: Request, res: Response) => {
    const data = await this.foodService.updateFood(req.params.id, req.body);
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
