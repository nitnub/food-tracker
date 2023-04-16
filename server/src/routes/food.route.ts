import { Router } from 'express';
import foodController from '@controllers/food.controller';

const foodRouter = Router();

foodRouter
  .route('/')
  .post(foodController.addFoods)
  .get(foodController.getAllFoods);

export default foodRouter;
